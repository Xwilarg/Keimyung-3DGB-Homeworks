using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Prefab for star UI")]
    private GameObject starUI;
    [SerializeField]
    [Tooltip("Parent UI for where star UI are placed")]
    private Transform parentUI;
    [SerializeField]
    private Sprite starUIEmpty, starUIFull;
    [SerializeField]
    private GameObject victoryUI;
    [SerializeField]
    private GameObject bluePlatform;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private const float speed = 300f; // Player speed
    private const float jumpDistance = 0.65f; // Max distance with the floor so the player can jump
    private const float jumpForce = 15f; // Force of the player jump
    private const int starCount = 5; // Max amount of star the player can get

    private int currStar; // Current amount of star the player have
    private Image[] allStarUI; // All Image of the UI of the star

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        allStarUI = new Image[starCount];
        for (int i = 0; i < starCount; i++)
        {
            GameObject ui = Instantiate(starUI, parentUI);
            ui.transform.Translate(new Vector2(i * 100f, 0f));
            allStarUI[i] = ui.GetComponent<Image>();
        }
        currStar = 0;
    }

    private void Update()
    {
        // Movements
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0f)
            sr.flipX = true;
        else if (horizontal > 0f)
            sr.flipX = false;

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            int layer = 1 << 8; // "Floor" layer
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -transform.up, jumpDistance, layer);
            if (hit.distance > 0f)
            {
                rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            }
        }

        // So the player don't return himself
        if (Mathf.Abs(transform.rotation.eulerAngles.z) > 80f)
        {
            transform.rotation = Quaternion.identity;
        }

        rb.velocity = new Vector2(horizontal * Time.deltaTime * speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Star"))
        {
            allStarUI[currStar].sprite = starUIFull;
            currStar++;
            if (currStar == starCount)
                victoryUI.SetActive(true);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("BlueKey"))
        {
            bluePlatform.GetComponent<TilemapCollider2D>().enabled = true;
            Tilemap tm = bluePlatform.GetComponent<Tilemap>();
            tm.color = new Color(tm.color.r, tm.color.g, tm.color.b, 1f);
            Destroy(collision.gameObject);
        }
    }
}
