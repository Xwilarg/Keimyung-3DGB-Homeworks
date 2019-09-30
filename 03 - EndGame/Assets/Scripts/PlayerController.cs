using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private const float speed = 10f;

    private const float refTimerShoot = 1f;
    private float timerShoot;

    private bool isGun1Loaded; // From where the missile shoot

    [SerializeField]
    private Transform gun1, gun2;
    [SerializeField]
    private GameObject missilePrefab;
    [SerializeField]
    private GameObject[] life;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timerShoot = 0f;
        isGun1Loaded = true;
    }

    private void FixedUpdate()
    {
        timerShoot -= Time.deltaTime;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(missilePrefab, (isGun1Loaded ? gun1 : gun2).position, Quaternion.identity);
            isGun1Loaded = !isGun1Loaded;
            timerShoot = refTimerShoot;
        }
    }
}
