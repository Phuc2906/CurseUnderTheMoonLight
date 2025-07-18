using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 1f;               // tốc độ bơi
    public float moveDistance = 3f;        // khoảng cách bơi mỗi bên

    private Vector3 startPos;
    private bool movingLeft = true;
    private SpriteRenderer sr;

    void Start()
    {
        startPos = transform.position;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float leftLimit = startPos.x - moveDistance;
        float rightLimit = startPos.x + moveDistance;

        // Di chuyển
        if (movingLeft)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                movingLeft = false;
                sr.flipX = true; // Quay đầu qua phải
            }
        }
        else
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
            {
                movingLeft = true;
                sr.flipX = false; // Quay đầu qua trái
            }
        }
    }
}

