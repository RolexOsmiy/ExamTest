using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class MobSpawner : NetworkBehaviour
{
    public Transform[] spawnPoints;
    public GameObject prefab;
    public int maxMobs;
	public bool spawning = true;

	public List<GameObject> mobPool = new List<GameObject>();

    void Start()
    {
		SpawnEnemy ();
    }

    void Update()
    {
		if (mobPool.Count == null) {
			print ("epmty");
		}
    }

    public void SpawnEnemy()
    {
		if (spawning) 
		{
			for (int i = 0; i < maxMobs; i++)
			{				
				GameObject newMob = Instantiate (prefab, spawnPoints [i].transform.position, spawnPoints [i].transform.rotation);
				mobPool.Add(newMob);
			}
		}        
    }

	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "DeathGas") 
		{			
			spawning = false;
		}
	}
}