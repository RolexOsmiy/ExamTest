using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

	public float reloadTime = 1f;
	public float waitTime = 0f;
	public GameObject core;
	public Transform shotPoint;
	public float maxDistance = 10f;

	void Start () {
		StartCoroutine (WaitBefore());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Shoot()
	{
		yield return new WaitForSeconds(reloadTime);
		GameObject clone = Instantiate(core, shotPoint.position, shotPoint.rotation);
		clone.GetComponent<Core> ().SetVars (maxDistance, this.gameObject);
		StartCoroutine (Shoot ());
	}

	IEnumerator WaitBefore()
	{
		yield return new WaitForSeconds(waitTime);
		StartCoroutine (Shoot ());
	}
}
