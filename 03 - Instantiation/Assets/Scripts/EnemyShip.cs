using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyShip : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabExplosion;

    private Rigidbody2D rb;
    private const float speed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.down * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Missile"))
        {
            Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
