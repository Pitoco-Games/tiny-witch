using UnityEngine;

namespace CoreGameplay.Controls.Movement
{
    public class MovementController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2D;
        [SerializeField] private float moveSpeed;

        public void Move(Vector2 direction)
        {
            rigidbody2D.linearVelocity = direction * moveSpeed * Time.deltaTime;
        }

        public void SetPosition(Vector2 position)
        {
            rigidbody2D.position = position;
        }
    }
}