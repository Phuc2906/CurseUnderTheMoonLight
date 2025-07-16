using UnityEngine;

public class LookGap : MonoBehaviour
{
    public Transform groundCheckPoint;
    public float groundCheckDistance = 0.5f;
    public LayerMask groundLayer;
    public MoveForward moveScript;

    void Update()
    {
        Vector2 checkPos = groundCheckPoint.position;
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, groundCheckDistance, groundLayer);

        if (hit.collider == null)
        {
            moveScript.Flip();
        }
    }
}
