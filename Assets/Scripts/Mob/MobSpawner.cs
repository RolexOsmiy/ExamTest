using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class MobSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject prefab;
    public int maxMobs;
    bool spawning = true;

    List<GameObject> mobPool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < maxMobs; i++)
        {
            GameObject mob = Instantiate(prefab, spawnPoints[i].transform.position, spawnPoints[i].transform.rotation);
            mob.SetActive(false);
            mobPool.Add(mob);
        }
        InvokeRepeating("SpawnEnemy",0, 5);
    }

    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        for (int i = 0; i < maxMobs; i++)
        {
            if (mobPool[i].activeSelf == false)
            {
                mobPool[i].transform.position = spawnPoints[i].transform.position;
                mobPool[i].transform.rotation = spawnPoints[i].transform.rotation;
                mobPool[i].SetActive(true);
				mobPool [i].GetComponent<CharacterStats> ().Respawn ();
            }
        }
    }
}