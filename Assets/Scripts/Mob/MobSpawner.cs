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
        //StartCoroutine(WaitMinute());
        InvokeRepeating("SpawnEnemy",0, 5);
    }

    void Update()
    {
        
    }

    public void SpawnEnemy()
    {
        Debug.Log("Spawn!");
        for (int i = 0; i < maxMobs; i++)
        {
            if (mobPool[i].activeSelf == false)
            {
                mobPool[i].transform.position = spawnPoints[i].transform.position;
                mobPool[i].transform.rotation = spawnPoints[i].transform.rotation;
                mobPool[i].GetComponent<Stats>().ResetStats();
                mobPool[i].SetActive(true);
            }
        }
    }

    IEnumerator WaitMinute()
    {
        yield return new WaitForSeconds(5);
        SpawnEnemy();
    }
}