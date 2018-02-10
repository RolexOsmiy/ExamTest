using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour {

	public Transform target;
	public Vector3 offest;
	public float pitch = 2f;
	float Zoom = 10f;
	public float zoomSpeed = 40f;
	float curZoom = 10f; 
	public float minZoom = 5f;
	public float maxZoom = 15f;
	public float yawSpeed = 100f;
	float yawInput = 0f;
	float curYaw = 0f;

	void Update(){
		curZoom -= Input.GetAxis ("Mouse ScrollWheel") * zoomSpeed;
		curZoom = Mathf.Clamp (curZoom, minZoom, maxZoom);

		curYaw -= Input.GetAxis ("Horizontal") * yawSpeed * Time.deltaTime;
	}

	void LateUpdate () {
		transform.position = target.position + offest * curZoom;
		transform.LookAt (target.position + Vector3.down * pitch);
		transform.RotateAround (target.position, Vector3.up, curYaw);
	}
}
