using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public int SpawnCount = 3;
    public List<Trace> spawnedEnemys;
    public int NextLevel; 

    private void Start()
    {
        SpawnMonster();
    }

    private void Update()
    {

    }

    void SpawnMonster()
    {
        for (int i = 0; i < SpawnCount; i++)
        {
            spawnedEnemys.Add(Instantiate(EnemyPrefab).GetComponent<Trace>());
        }


    }

    public void RemoveMonster(Trace remove)
    {
        spawnedEnemys.Remove(remove);

        if (spawnedEnemys.Count <= 0)
        {
            SceneManager.LoadScene(NextLevel);
        }


    }
}
