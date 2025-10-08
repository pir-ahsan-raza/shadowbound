using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colision : MonoBehaviour
{
    public int damage = 1;
    public float speed;

    public GameObject effect;
    private Animator camAnim;

    public GameObject popSoundObstacle;

    private void Start()
    {
        camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            camAnim.SetTrigger("cam-shake");
            Instantiate(popSoundObstacle, transform.position, Quaternion.identity);
            Instantiate(effect, transform.position, Quaternion.identity);
            other.GetComponent<hero>().health -= damage;
            Debug.Log(other.GetComponent<hero>().health);
            Destroy(gameObject);
        }
    }
}
