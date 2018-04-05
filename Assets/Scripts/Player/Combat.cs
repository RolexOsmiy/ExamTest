using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Combat : NetworkBehaviour {

	public LayerMask interactionMask; 
	Camera cam; 
	GameObject target;
	public bool isRange;
	float attackSpeed = 2f;
	float currAttackSpeed = 0;

	// Use this for initialization
	void Start () 
	{
		cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		currAttackSpeed -= Time.deltaTime;	
		if (isRange) 
		{			
			if (currAttackSpeed <= 0) {
				StartCoroutine (Attack ());
				currAttackSpeed = attackSpeed;
			}
		}


		if (Input.GetMouseButtonDown(1))
		{
			// Shoot out a ray

			RaycastHit hit;
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			// If we hit
			if (Physics.Raycast(ray, out hit, 100f, interactionMask))
			{
				target = hit.collider.gameObject;
			}
		}
		if (target) {
			if (Vector3.Distance (this.gameObject.transform.position, target.gameObject.transform.position) <= 2) {
				isRange = true;
			} else 
			{
				isRange = false;
				StopCoroutine (Attack ());
			}
			if (Vector3.Distance (this.gameObject.transform.position, target.gameObject.transform.position) >= 5) {
				target = null;
			}
		}
	}

	IEnumerator Attack()
	{
		if (target) {
			yield return new WaitForSeconds (1f);
			target.gameObject.GetComponent<CharacterStats> ().Hurt (this.gameObject.GetComponent<PlayerStats> ().damage);
            target.gameObject.GetComponent<PlayerStats>().Hurt(this.gameObject.GetComponent<PlayerStats>().damage);
            print ("Hit from player");
		}
	}
}
