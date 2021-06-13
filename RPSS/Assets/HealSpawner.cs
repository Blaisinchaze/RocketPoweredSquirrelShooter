using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class HealSpawner : MonoBehaviour
{
    public List<GameObject> heals = new List<GameObject>();
    private float timer;
    Random it;

    private void Start()
    {
        it.InitState();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 30f)
            SpawnRandomHeal();
    }

    private void SpawnRandomHeal()
    {
        heals[it.NextInt(0, heals.Count - 1)].SetActive(true);
        timer = 0f;
    }
}
