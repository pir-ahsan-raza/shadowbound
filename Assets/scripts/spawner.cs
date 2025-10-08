using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    //[Header("Obstacle Patterns")]
    public GameObject[] easyPatterns;
    public GameObject[] hardPatterns;

    //[Header("Spawn Timing")]
    private float timeBetweenSpawn;
    public float startTimeBetweenSpawn = 3f;
    public float decreaseRate = 0.05f;
    public float minSpawnTime = 1f;
    public float criticalSpawnTime = 0.3f; // when chaos starts

    //[Header("Game Timer")]
    private float gameTimer = 0f;
    public float difficultyTime = 10f;
    public float criticalTime = 12f; // after 2 min critical mode

    private int lastPatternIndex = -1;
    private int repeatCount = 0;
    public int maxRepeats = 3;

    public bool isCriticalMode { get; private set; }



    public bool isSpawning = true;

    /*void Update()
    {
        if (!isSpawning) return;

        // your existing spawn logic here
    }*/

    public void StopSpawning()
    {
        isSpawning = false;
    }




    void Start()
    {
        timeBetweenSpawn = startTimeBetweenSpawn;
    }

    //public bool isCriticalMode = false;
    public List<GameObject> colorChangeTargets; // assign in Inspector

    public void EnterCriticalMode()
    {
        isCriticalMode = true;
        foreach (GameObject obj in colorChangeTargets)
        {
            SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.color = Color.red;
        }
    }

    void Update()
    {
        if (!isSpawning) return;
        gameTimer += Time.deltaTime;

        if (timeBetweenSpawn <= 0)
        {
            GameObject[] currentSet = gameTimer >= difficultyTime ? hardPatterns : easyPatterns;

            int rand = Random.Range(0, currentSet.Length);
            if (rand == lastPatternIndex)
            {
                repeatCount++;
                if (repeatCount > maxRepeats)
                {
                    rand = (rand + 1) % currentSet.Length;
                    repeatCount = 0;
                }
            }
            else repeatCount = 0;

            lastPatternIndex = rand;

            Instantiate(currentSet[rand], transform.position, Quaternion.identity);
            timeBetweenSpawn = startTimeBetweenSpawn;

            // Gradually tighten spawn rate
            if (gameTimer < criticalTime && startTimeBetweenSpawn > minSpawnTime)
            {
                startTimeBetweenSpawn -= decreaseRate;
            }
            else if (gameTimer >= criticalTime && startTimeBetweenSpawn > criticalSpawnTime)
            {
                startTimeBetweenSpawn -= decreaseRate * 2; // faster reduction in critical mode
                isCriticalMode = true;
            }
        }
        else timeBetweenSpawn -= Time.deltaTime;
    }
}
