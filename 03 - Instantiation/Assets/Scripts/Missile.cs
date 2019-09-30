using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Missile : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 15f;

    private PlayerController pc;

    private void Start()
    {
        Destroy(gameObject, 5f);
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pc.IncreaseScore();
        Destroy(gameObject);
    }

    public void SetPlayerController(PlayerController newPc)
    {
        pc = newPc;
    }
}
