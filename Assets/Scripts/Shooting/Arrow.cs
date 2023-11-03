using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Arrow : MonoBehaviour
{
    private Rigidbody2D _rigidBody2D;
    private bool _isHitted;

    private void Awake()
    {
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isHitted)
        {
            if (_rigidBody2D.velocity != Vector2.zero)
            {
                float angle = Mathf.Atan2(_rigidBody2D.velocity.y, _rigidBody2D.velocity.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isHitted = true;
        _rigidBody2D.velocity = Vector2.zero;
        _rigidBody2D.isKinematic = true;
        _rigidBody2D.freezeRotation = true;
    }
}
