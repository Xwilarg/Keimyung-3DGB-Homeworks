using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabEnemyShip;

    private float xMin, xMax, yPos;

    private Vector2 refTimerSpawn = new Vector2(1f, 2f);
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
            Destroy(Instantiate(prefabEnemyShip, new Vector2(Random.Range(xMin, xMax), yPos), Quaternion.Euler(180f, 0f, 0f)), 10f);
            ResetTimerSpawn();
        }
    }

    private void ResetTimerSpawn()
    {
        timerSpawn = Random.Range(refTimerSpawn.x, refTimerSpawn.y);
        if (refTimerSpawn.x > .1f)
            refTimerSpawn = new Vector2(refTimerSpawn.x - .05f, refTimerSpawn.y - .05f);
    }
}
