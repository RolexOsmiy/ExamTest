using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour
{
	float distance = 0f;
	GameObject parent = null;
	public float speed = 10f;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		if (Vector3.Distance(parent.transform.position, transform.position) >= distance) {
			Destroy (this.gameObject);
		}
	}

	public void SetVars(float _distance, GameObject _parent)
	{
		distance = _distance;
		parent = _parent;
	}

	void OnCollisionEnter(Collision coll)
	{
		if (coll.gameObject.tag == "Player") {
			Destroy (this.gameObject);
			Debug.Log ("Hit Player!");
		}
	}
}

