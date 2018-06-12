using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGas_Script : MonoBehaviour {

	public ParticleSystem gasEffect;
	public CapsuleCollider gasTrigger;

	public float speed;

	void Start () {
		
	}

	void Update () 
	{
		if (gasEffect.shape.radius >= 0 && gasTrigger.radius >= 0) 
		{
			var shape = gasEffect.shape;
			shape.radius -= Time.deltaTime * speed;
			gasTrigger.radius -= Time.deltaTime * speed;
		}
	}
}
