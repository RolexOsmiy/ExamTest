using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class Controller : MonoBehaviour {

	Camera cam;
	PlayerMotor motor;
	public LayerMask movementMask;
	GameObject target;

	void Start () {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor> ();
	}	

	void Update ()
	{
		if (Input.GetMouseButton(1)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100, movementMask)) {
				motor.MoveToPoint (hit.point);
			}
		}

		if (Input.GetMouseButtonDown(0)) {
			Ray ray = cam.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) 
			{
				if (hit.collider.gameObject.tag == "Destroy") {
					target = hit.collider.gameObject;
				}
				if (target) {
					motor.MoveToPoint (hit.point);
				}
			}

		}

		if (target) {
			if (Vector3.Distance(target.transform.position,transform.position)<= 1) {
				
			}

		}
	}

	void OnCollisionStay(Collision coll)
	{
		if (target) {
			if (coll.gameObject.name == target.gameObject.name) {			
				StartCoroutine(Attack());
			}
		}
	}

	IEnumerator Attack()
	{
		yield return new WaitForSeconds(0.5f);
		Destroy(target);
	}
}
