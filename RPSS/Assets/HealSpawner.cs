using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class HealSpawner : MonoBehaviour
{
    public List<GameObject> pickups = new List<GameObject>();
    public GameObject[] prefabs;
    public float spawnDelay;
    public int maxPickups;
    [Space]
    public NavGrid grid;
    [SerializeField] private float timer;

    int i = 0;

    private void Update()
    {
        if (GameManager.Instance.currentState != GameStates.INGAME) return;

        timer += Time.deltaTime;
        if (timer > spawnDelay && pickups.Count < maxPickups)
        {
            SpawnRandomPickup();
            timer = 0f;
        }
    }

    private void SpawnRandomPickup()
    {
        int idx = pickups.Count;
        pickups.Add(Instantiate(prefabs[i]));

        pickups[idx].transform.position = grid.RandomNodeWorldPos();
        if (pickups[idx].TryGetComponent(out Pickup pickup))
        {
            pickup.manager = this;
        }

        i++;
        if (i >= prefabs.Length) i = 0;
    }
}
