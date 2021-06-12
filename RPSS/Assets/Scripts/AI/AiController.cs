using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[Serializable]
public struct SpawnChances
{
    public float walkers;
    public float shielders;
    public float gunners;

    public SpawnChances(float w, float s, float g)
    {
        walkers = w;
        shielders = s;
        gunners = g;
    }

    public void Check()
    {
        return;

        float total = walkers + shielders + gunners;

        if (total % 1 != 0)
        {
            Debug.LogError("SPAWN CHANCE SET UP WITH TOO HIGH ACCURACY - CAN ONLY BE whole numbers, NO decimels");
            return;
        }

        if (total > 100)
        {
            while (walkers + shielders + gunners > 101)
            {
                walkers -= 1;
            }
            while (walkers + shielders + gunners < 101 && walkers + shielders + gunners > 100)
            {
                walkers -= 0.1f;
            }
            Check();
        }
        if (total < 100)
        {
            while (walkers + shielders + gunners < 99)
            {
                walkers += 1;
            }
            while (walkers + shielders + gunners > 99 && walkers + shielders + gunners < 100)
            {
                walkers += 0.1f;
            }
            Check();
        }
    }

}

public enum Enemies
{
    NULL = -1,
    WALK = 0,
    SHIELD = 1,
    GUN = 2
}

public class AiController : MonoBehaviour
{
    public int incrementPerWave;
    public int baseEnemycount;
    public int secondsForWaveSpawn;
    public int minTimeUntilNextWave;
    [Space]
    public List<GameObject> prefabs = new List<GameObject>();
    [Space]
    public List<SpawnChances> chances = new List<SpawnChances>();

    private int enemiesToSpawn = 0;
    private float timeBetweenSpawns = 0;
    private int totalEnemiesThisWave = 0;
    [SerializeField]
    private int currentWave = 0;
    [SerializeField]
    private float waveTimer = 0;
    [SerializeField]
    private float spawnTimer = 0;

    private List<AiUnit> enemies = new List<AiUnit>();
    private List<Transform> spawnPoints = new List<Transform>();

    // Start is called before the first frame update
    private void Start()
    {
        //Fetch spawnpoints
        foreach (var item in GameObject.FindGameObjectsWithTag("Spawnpoint"))
        {
            spawnPoints.Add(item.transform);

        }

        StartNewWave();
    }

    // Update is called once per frame
    private void Update()
    {
        if (enemies.Count == 0 && enemiesToSpawn == 0)
        {
            waveTimer = 2;
            enemiesToSpawn = -1;
        }

        if (spawnTimer > 0)
        {
            spawnTimer -= Time.deltaTime;
        }
        if (waveTimer > 0)
        {
            waveTimer -= Time.deltaTime;
        }

        if (spawnTimer <= 0 && enemiesToSpawn > 0)
        {
            SpawnEnemy();
            spawnTimer = timeBetweenSpawns;
        }

        if (waveTimer <= 0)
        {
            StartNewWave();
        }
    }

    private void StartNewWave()
    {
        currentWave++;

        // Calc wave specific values
        totalEnemiesThisWave = baseEnemycount + (currentWave * incrementPerWave);
        enemiesToSpawn = totalEnemiesThisWave;
        timeBetweenSpawns = Mathf.Clamp(secondsForWaveSpawn / totalEnemiesThisWave, 1, 10);


        // Reset Timers
        waveTimer = minTimeUntilNextWave;
        spawnTimer = timeBetweenSpawns;
    }

    private void SpawnEnemy(Enemies type = Enemies.NULL)
    {
        int chanceRef = currentWave;
        if (chanceRef >= chances.Count)
        {
            chanceRef = chances.Count - 1;
        }

        SpawnChances percentages = new SpawnChances(33.4f, 33.3f, 33.3f);

        switch (type)
        {
            case Enemies.NULL:
                percentages = chances[chanceRef];
                break;
            case Enemies.WALK:
                 percentages = new SpawnChances(100, 0, 0);
                break;
            case Enemies.SHIELD:
                 percentages = new SpawnChances(0, 100, 0);
                break;
            case Enemies.GUN:
                 percentages = new SpawnChances(0, 0, 100);
                break;
        }

        float rnd = Random.Range(0, 100);
        int spawnRef = Random.Range(0, spawnPoints.Count - 1);
        int enemyRef = -1;

        if (rnd <= percentages.walkers)
        {
            enemyRef = (int)Enemies.WALK;
        }
        else if (rnd <= percentages.shielders + percentages.walkers)
        {
            enemyRef = (int)Enemies.SHIELD;
        }
        else
        {
            enemyRef = (int)Enemies.GUN;
        }

        enemies.Add(Instantiate(prefabs[enemyRef], spawnPoints[spawnRef].position, Quaternion.identity, transform).GetComponent<AiUnit>());

        enemiesToSpawn--;
    }

    public void KillEnemy(AiUnit enemy)
    {
        enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void OnValidate()
    {
        foreach (SpawnChances chance in chances)
        {
            // Check needs a change to fix it
            //chance.Check();
        }
    }
}


