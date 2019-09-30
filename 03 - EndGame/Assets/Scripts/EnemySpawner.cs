using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabEnemyShip;

    private float xMin, xMax, yPos;

    private readonly Vector2 refTimerSpawn = new Vector2(1f, 3f);
    private float timerSpawn;

    private void Start()
    {
        xMin = GameObject.Find("Left collision").transform.position.x + 1f;
        xMax = GameObject.Find("Right collision").transform.position.x - 1f;
        yPos = GameObject.Find("Up collision").transform.position.y + 3f;
        ResetTimerSpawn();
    }

    private void Update()
    {
        timerSpawn -= Time.deltaTime;
        if (timerSpawn < 0f)
        {
            Instantiate(prefabEnemyShip, new Vector2(Random.Range(xMin, xMax), yPos), Quaternion.Euler(180f, 0f, 0f));
            ResetTimerSpawn();
        }
    }

    private void ResetTimerSpawn()
    {
        timerSpawn = Random.Range(refTimerSpawn.x, refTimerSpawn.y);
    }
}
