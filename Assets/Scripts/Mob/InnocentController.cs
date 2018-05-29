using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InnocentController : MonoBehaviour {

	public float changePosTimer = 10;
	public float runRange = 10.0f;

	public float currentHealth {get;protected set;}	// Current amount of health

	[Header("Stats")]
	public float health;

	[SerializeField]
	private Image image;

	[SerializeField]
	private float maxHealthPoints = 100;

	[SerializeField]
	private float healthBarStepsLength = 10;

	[SerializeField]
	private float damagesDecreaseRate = 10;

	private float currentHealthPoints;

	[Header("Effects")]
	public GameObject healEffect;
	public ParticleSystem hitEffect;


	private RectTransform imageRectTransform;

	private float damages;

	NavMeshAgent agent;

	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		currentHealth = health;
		hitEffect.Stop ();
		InvokeRepeating("ChangePos", Random.Range(0,10), changePosTimer);
	}

	bool RandomPoint(Vector3 center, float range, out Vector3 result) {
		for (int i = 0; i < 30; i++) {
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}

	public float Health
	{
		get { return currentHealthPoints; }
		set
		{
			currentHealthPoints = Mathf.Clamp(value, 0, MaxHealthPoints);
			image.material.SetFloat("_Percent", currentHealthPoints / MaxHealthPoints);

			if (currentHealthPoints < Mathf.Epsilon)
				Damages = 0;
		}
	}

	public float Damages
	{
		get { return damages; }
		set
		{
			damages = Mathf.Clamp(value, 0, MaxHealthPoints);
			image.material.SetFloat("_DamagesPercent", damages / MaxHealthPoints);
		}
	}

	public float MaxHealthPoints
	{
		get { return maxHealthPoints; }
		set
		{
			maxHealthPoints = value;
			image.material.SetFloat("_Steps", MaxHealthPoints / healthBarStepsLength);
		}
	}


	protected void Update()
	{		
		if (Input.GetKeyDown(KeyCode.Space)) {
			Hurt (50);
		}

		if (currentHealthPoints > maxHealthPoints)
		{
			currentHealthPoints = maxHealthPoints;
		}
		if (currentHealthPoints < 1)
		{
			this.gameObject.SetActive(false);
			Debug.Log ("Innocent mob die!");
		}
		if (Damages > 0)
		{
			Damages -= damagesDecreaseRate * Time.deltaTime;
		}

		image.gameObject.transform.rotation = Quaternion.Euler(90, -90, 0);
	}

	public float Hurt(float damagesPoints)
	{
		float returnExp = 0;
		Damages = damagesPoints;
		Health -= Damages;

		hitEffect.Play();

		if (Health < 1)
		{
			this.gameObject.SetActive (false);            
		}     
		Vector3 point;
		RandomPoint (transform.position, runRange, out point);
		agent.SetDestination (point);
		return returnExp;
	}

	public void Respawn()
	{		
		currentHealth = health;
		MaxHealthPoints = MaxHealthPoints;
		currentHealthPoints = MaxHealthPoints;
		Hurt (0);
	}

	public void Heal()
	{
		if (currentHealthPoints <= maxHealthPoints)
		{
			currentHealthPoints += (1/(100/currentHealthPoints)) * Time.deltaTime;
		}
	}

	void Awake()
	{
		imageRectTransform = image.GetComponent<RectTransform>();
		image.material = Instantiate(image.material); // Clone material

		image.material.SetVector("_ImageSize", new Vector4(imageRectTransform.rect.size.x, imageRectTransform.rect.size.y, 0, 0));

		MaxHealthPoints = MaxHealthPoints;
		currentHealthPoints = MaxHealthPoints;
	}

	void OnTriggerStay(Collider coll)
	{
		if (coll.tag == "HealSpot" & currentHealth < maxHealthPoints)
		{
			Heal ();
			healEffect.SetActive (true);
		}
		else if (coll.tag == "HealSpot" & currentHealth >= maxHealthPoints)
		{
			healEffect.SetActive (false);
		}
	}
	void OnTriggerExit(Collider coll)
	{
		if (coll.tag == "HealSpot")
		{
			healEffect.SetActive (false);
		}
	}

	// Damage the character
	public void TakeDamage (int damage)
	{
		damage = Mathf.Clamp(damage, 0, int.MaxValue);


		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");


		if (currentHealth <= 1)
		{
			this.gameObject.SetActive (false);
		}
	}

	void ChangePos()
	{
		Vector3 point;
		RandomPoint (transform.position, runRange, out point);
		agent.SetDestination (point);
	}
}
