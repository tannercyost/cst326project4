﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[System.Serializable]
public struct Group
{
    public GameObject enemy;
    public float spawnTime;
    public int numberOfEnemies;

    public Group(GameObject enemy, float spawnTime, int numberOfEnemies)
    {
        this.enemy = enemy;
        this.spawnTime = spawnTime;
        this.numberOfEnemies = numberOfEnemies;
    }
}

[System.Serializable]
public struct Wave
{
    public Group[] enemyGroups;

    public Wave(Group[] enemyGroups)
    {
        this.enemyGroups = enemyGroups;
    }
}


public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyA;
    public GameObject EnemyB;
    public float timeToWaitA = 1;
    public float timeToWaitB = 1.5f;
    public Wave currentWave;
    private bool trigger = false;
    public WaypointManager waypointManager;
    public GameManager gm;
    void Start()
    {
        Group groupA = new Group(EnemyA, 1f, 7);
        Group groupB = new Group(EnemyB, timeToWaitB, 10);

        Group[] groups = new Group[2]{groupA, groupB};
        currentWave = new Wave(new Group[2] { groupA, groupB });

        SpawnWave(currentWave);
    }

    private void Update()
    {
        if (trigger)
            if (transform.childCount <= 0)
                SceneManager.LoadScene(3);
    }

    private void SpawnWave(Wave newWave)
    {
        foreach (Group group in newWave.enemyGroups)
        {
            StartCoroutine(SpawnGroup(group));
        }
    }

    private IEnumerator SpawnGroup(Group @group)
    {
        while (@group.numberOfEnemies > 0)
        {
            yield return new WaitForSeconds(@group.spawnTime);
            GameObject enemy = Instantiate(@group.enemy);
            iMovement e = enemy.GetComponent<iMovement>();
            e.DeathEvent().AddListener(delegate { reportDeath(); });
            enemy.transform.parent = gameObject.transform;
            enemy.GetComponent<iMovement>().Initialize(waypointManager);
            @group.numberOfEnemies--;
        }
        trigger = true;
    }

    private void reportDeath()
    {
        gm.deathReported();
    }
}
