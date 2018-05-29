using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {

	public Text damageText;
	public Text healthText;
	public Text armorText;
	public Text essenceText;
	public Image essenceIcon;

	public GameObject popoutDamage;

    [SerializeField]
    private Image image;

	[Header("Stats")]
	public Essence essence;

	public int lvl = 0;
    [SerializeField]
    private float maxHealthPoints = 100;

    [SerializeField]
    private float healthBarStepsLength = 10;

    [SerializeField]
    private float damagesDecreaseRate = 10;

	public float damage;
	public float armor;
    private float currentHealthPoints;

	public float moveSpeed = 3;
    

	[Header("Effects")]
	public GameObject healEffect;
	public ParticleSystem hitEffect;

    private RectTransform imageRectTransform;

    private float damages;


	void Start()
	{
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
		Health -= 0;


		damageText.text = "Damage: " + damage;
		healthText.text = "Health: " + (int)currentHealthPoints + "/" + maxHealthPoints;
		armorText.text = "Armor: " + armor;
		essenceText.text = "Essence: " + essence.name;
		essenceIcon.sprite = essence.artwork;


        if (currentHealthPoints > maxHealthPoints)
        {
            currentHealthPoints = maxHealthPoints;
        }
		if (currentHealthPoints < 1)
        {
            this.gameObject.SetActive(false);
			Debug.Log ("Player die!");
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
		GameObject clone;
		clone = Instantiate(popoutDamage, transform.position, transform.rotation);
		clone.GetComponentInChildren<PopoutDamage>().damageText.text = damagesPoints + "";

        if (Health < 1)
        {
            //return exp           
        }
    }

    public void Respawn()
    {
        currentHealthPoints = maxHealthPoints;
    }

    public void Heal()
    {
        if (currentHealthPoints <= maxHealthPoints)
        {
			currentHealthPoints += (15/(100/currentHealthPoints)) * Time.deltaTime;
        }
    }

    void Awake()
    {
		SetStats ();


        imageRectTransform = image.GetComponent<RectTransform>();
        image.material = Instantiate(image.material); // Clone material

        image.material.SetVector("_ImageSize", new Vector4(imageRectTransform.rect.size.x, imageRectTransform.rect.size.y, 0, 0));

        MaxHealthPoints = MaxHealthPoints;
        currentHealthPoints = MaxHealthPoints;
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "HealSpot" & currentHealthPoints < maxHealthPoints)
        {
			Heal ();
			healEffect.SetActive(true);
        }
        else if (coll.tag == "HealSpot" & currentHealthPoints >= maxHealthPoints)
        {
            healEffect.SetActive(false);
        }
    }
    void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "HealSpot")
        {
            healEffect.SetActive(false);
        }
    }

    public void ResetStats()
    {
        Health = maxHealthPoints;
    }

	public void SetStats()
	{
		maxHealthPoints = essence.defaultHealth;
		damage = essence.defaultDamage;
		armor = essence.defaultArmor;
	}

	public void EvolutionSetStats()
	{		
		damageText.text = "Damage: " + damage;
		healthText.text = "Health: " + (int)currentHealthPoints + "/" + maxHealthPoints;
		armorText.text = "Armor: " + armor;
		essenceText.text = "Essence: " + essence.name;
		essenceIcon.sprite = essence.artwork;

		maxHealthPoints = essence.defaultHealth;
		damage = essence.defaultDamage;
		armor = essence.defaultArmor;

		Health -= 0;
	}
}
