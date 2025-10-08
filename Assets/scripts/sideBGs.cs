using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sideBGs : MonoBehaviour
{
    public float speed;

    private float height;

    private void Start()
    {
        // Get height of this sprite in world units
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        height = sr.bounds.size.y;
    }

    private void Update()
    {
        // Move down smoothly
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // If this background goes fully below the camera
        if (transform.position.y < -height)
        {
            // Snap it exactly above the other piece
            Vector2 pos = new Vector2(transform.position.x, transform.position.y + 2 * height);
            transform.position = pos;
        }
    }
}
