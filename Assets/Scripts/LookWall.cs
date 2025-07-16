using UnityEngine;

public class LookWall : MonoBehaviour
{
    public float checkDistance = 0.5f;
    public LayerMask wallLayer;
    public MoveForward moveScript;

    void Update()
    {
        Vector2 direction = moveScript.moveLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, checkDistance, wallLayer);

        if (hit.collider != null)
        {
            moveScript.Flip();
        }
    }
}
