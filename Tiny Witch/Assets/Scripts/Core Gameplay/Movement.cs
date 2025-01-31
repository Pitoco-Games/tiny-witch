using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private float moveSpeed;
    
    public void Move(Vector2 direction)
    {
        rigidbody2D.linearVelocity = direction * moveSpeed * Time.deltaTime;
    }
}
