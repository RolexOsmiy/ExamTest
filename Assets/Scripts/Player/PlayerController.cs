using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
	public float moveSpeed;
	public float maxSpeed = 3;
	public Rigidbody rb;
	public GameObject body;

	float currAttackSpeed;

	public GameObject attackTrigger;

	public Animator animator;

	public PlayerStats playerStats;

	public NetworkAnimator networkAnimator;

	bool isGas = false;

	void Start()
	{
		//attackTrigger = transform.Find ("AttackTrigger").gameObject;
		moveSpeed = this.gameObject.GetComponent<PlayerStats> ().moveSpeed;
	}

	void Update() 
	{		
		if (isGas) {
			playerStats.EnterGas (20);
		}

		currAttackSpeed += Time.deltaTime;
		animator = body.GetComponent<Animator> ();

		if(rb.velocity.magnitude > maxSpeed)
		{
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}

		if (Input.GetKey (KeyCode.W)) {
			rb.AddForce (body.transform.forward * moveSpeed);
			animator.SetInteger ("State", 1);
			attackTrigger.SetActive (false);
		} else if (Input.GetMouseButton (0)) {			
			animator.SetInteger ("State", 2);
			attackTrigger.SetActive (true);
			currAttackSpeed = 0;
		} else {
			animator.SetInteger ("State", 0);
			attackTrigger.SetActive (false);
		}

		Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
		Plane plane=new Plane(Vector3.up, Vector3.zero);
		float distance;
		if(plane.Raycast(ray, out distance)) {
			Vector3 target=ray.GetPoint(distance);
			Vector3 direction=target-transform.position;
			float rotation=Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg;
			body.transform.rotation=Quaternion.Euler(0, rotation, 0);
		}
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "HealSpot")
		{
			playerStats.Heal ();
			playerStats.healEffect.SetActive(true);
		}
		else if (coll.tag == "HealSpot")
		{
			playerStats.healEffect.SetActive(false);
		}
		if (coll.tag == "DeathGas") 
		{
			isGas = false;
		}
	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "HealSpot") 
		{
			playerStats.healEffect.SetActive (false);
		} 
		if (coll.tag == "DeathGas") 
		{			
			isGas = true;
		}
	}

	public override void OnStartLocalPlayer()
	{
		networkAnimator.SetParameterAutoSend (0, true);
	}

	public override void PreStartClient()
	{
		networkAnimator.SetParameterAutoSend (0, true);
	}
}