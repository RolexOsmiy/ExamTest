using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour {

	public float currentHealth {get;protected set;}	// Current amount of health

	[Header("Stats")]
	public float health;
	public float damage;
	public float armor;

	public float exp = 100;

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
		if (currentHealthPoints < maxHealthPoints) {
			currentHealthPoints += Time.deltaTime;
		}
		if (currentHealthPoints > maxHealthPoints)
		{
			currentHealthPoints = maxHealthPoints;
		}

		if (Damages > 0)
		{
			Damages -= damagesDecreaseRate * Time.deltaTime;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			Hurt(damage);
		}

		image.gameObject.transform.rotation = Quaternion.Euler(90, 0, 0);
	}

	public void Hurt(float damagesPoints)
	{
		Damages = damagesPoints;
		Health -= Damages;

		hitEffect.Play();
	}

	public void Respawn()
	{		
		currentHealth = health;
		MaxHealthPoints = MaxHealthPoints;
		currentHealthPoints = MaxHealthPoints;
		Hurt (0);
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
		
	}
	void OnTriggerExit(Collider coll)
	{
		
	}

	// Damage the character
	public void TakeDamage (int damage)
	{
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		Debug.Log(transform.name + " takes " + damage + " damage.");
	}
}