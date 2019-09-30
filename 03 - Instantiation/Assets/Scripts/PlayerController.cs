using UnityEngine;
using UnityEngine.UI;

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
    private GameObject missilePrefab, explosionPrefab;
    [SerializeField]
    private GameObject[] life;
    [SerializeField]
    private Text scoreUI, gameOverUI;

    private int score;
    private int lifeIndex;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timerShoot = 0f;
        isGun1Loaded = true;
        lifeIndex = life.Length - 1;
        score = 0;
    }

    private void FixedUpdate()
    {
        timerShoot -= Time.deltaTime;
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(missilePrefab, (isGun1Loaded ? gun1 : gun2).position, Quaternion.identity).GetComponent<Missile>().SetPlayerController(this);
            isGun1Loaded = !isGun1Loaded;
            timerShoot = refTimerShoot;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Instantiate(explosionPrefab, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            if (lifeIndex == -1)
            {
                Destroy(gameObject);
                gameOverUI.gameObject.SetActive(true);
                gameOverUI.text += score;
            }
            else
            {
                life[lifeIndex].SetActive(false);
                lifeIndex--;
                transform.position = Vector2.zero;
            }
        }
    }

    public void IncreaseScore()
    {
        score++;
        scoreUI.text = "Your Score: " + score;
    }
}
