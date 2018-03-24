using UnityEngine;
using UnityEngine.UI;

/* Contains all the stats for a character. */

public class CharacterStats : MonoBehaviour {

	public float currentHealth {get;protected set;}	// Current amount of health

	[Header("Stats")]
	public float health;
	public float damage;
	public float armor;

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


	void Start()
	{
		currentHealth = health;
		hitEffect.Stop ();
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
		if (currentHealthPoints > maxHealthPoints)
		{
			currentHealthPoints = maxHealthPoints;
		}
		if (currentHealthPoints < 1)
		{
			this.gameObject.SetActive(false);
			Debug.Log ("Mob die!");
		}
		if (Damages > 0)
		{
			Damages -= damagesDecreaseRate * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Hurt(damage);
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
}