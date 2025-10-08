
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class hero : MonoBehaviour
{
    private Vector2 targetPos;
    public float hts = 100f; // hero transition speed

    public float limitleft = -8f;
    public float limitright = 8f;

    public int health = 3;

    public GameObject effect;
    public Animator camAnim;
    public Text Hlife;

    public GameObject gameUI;

    [Header("References")]
    public spawner spawner; // assign in inspector

    private bool isTeleporting = false;
    private float teleportSpeedMultiplier = 3f; // controls teleport travel speed
    private Vector2 teleportTarget;

    private void Update()
    {
        Hlife.text = health.ToString();

        if (health <= 0)
        {
            FindObjectOfType<gameManager>().LoseGame();
            if (effect != null)
                Instantiate(effect, transform.position, Quaternion.identity);
            Destroy(gameObject, 1f);
            return;
        }




        // Gradual horizontal movement toward current target
        transform.position = Vector2.MoveTowards(transform.position, targetPos, hts * Time.deltaTime);

        if (isTeleporting)
        {
            // smooth teleport slide motion
            transform.position = Vector2.MoveTowards(transform.position, teleportTarget, hts * teleportSpeedMultiplier * Time.deltaTime);

            // stop teleporting once reached
            if (Vector2.Distance(transform.position, teleportTarget) < 0.1f)
            {
                isTeleporting = false;
                targetPos = teleportTarget;
            }

            return; // skip rest while teleporting
        }

        if (!spawner.isCriticalMode)
        {
            // Normal mode: simple lane hopping
            if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.position.x > limitleft)
                Jump(-4);
            else if (Input.GetKeyDown(KeyCode.RightArrow) && transform.position.x < limitright)
                Jump(4);
        }
        else
        {
            // Critical mode: allow both lane movement AND teleporting
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (transform.position.x <= limitleft + 0.5f)
                    StartTeleport(limitright);
                else
                    Jump(-4);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (transform.position.x >= limitright - 0.5f)
                    StartTeleport(limitleft);
                else
                    Jump(4);
            }
        }
    }

    void Jump(float distance)
    {
        camAnim.SetTrigger("cam-shake");
        Instantiate(effect, transform.position, Quaternion.identity);
        targetPos = new Vector2(transform.position.x + distance, transform.position.y);
    }

    void StartTeleport(float targetX)
    {
        camAnim.SetTrigger("cam-shake");
        Instantiate(effect, transform.position, Quaternion.identity);

        // Start smooth teleport instead of instant swap
        teleportTarget = new Vector2(targetX, transform.position.y);
        isTeleporting = true;
    }
}
