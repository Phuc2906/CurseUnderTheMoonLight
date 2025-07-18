using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // phím A/D hoặc ← →
        float v = Input.GetAxis("Vertical");   // phím W/S hoặc ↑ ↓

        transform.position += new Vector3(h, v, 0) * speed * Time.deltaTime;
    }
}
