using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerStats : NetworkBehaviour {

	public Text damageText;
	public Text healthText;
	public Text armorText;
	public Text essenceText;
	public Text expText;
	public Text livesText;
	public Image essenceIcon;
	public GameObject skin;
	public GameObject bodyTransform;
	public PlayerController playerController;
	public Player_SyncPosition playerSyncPosition;
	public Player_SyncRotation playerSyncRotation;
	public NetworkAnimator networkAnimator;

	public GameObject popoutDamage;

    [SerializeField]
    private Image image;

	[Header("Stats")]
	public Essence essence;
	public LevelArray[] lvlArray;
	public int currLevel = 0;
	public float currExp = 0;
	public int lives;


    [SerializeField]
	private float maxHealthPoints = 100;

    [SerializeField]
    private float healthBarStepsLength = 10;

    [SerializeField]
    private float damagesDecreaseRate = 10;

	public float damage;
	public float armor;

	[SyncVar]
	private float curHealth;
	[SerializeField] float lerpRate = 15;

	[SerializeField] public float currentHealthPoints;

	public float moveSpeed = 3;    

	[Header("Effects")]
	public GameObject healEffect;
	public ParticleSystem hitEffect;

    private RectTransform imageRectTransform;

    private float damages;

	public NetworkStartPosition[] spawnPoints;

	void Start()
	{
		spawnPoints = FindObjectsOfType<NetworkStartPosition> ();
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
		if (lives <= 0) 
		{
			SceneManager.LoadScene (0);
		}

		if (currExp >= lvlArray[currLevel+1].exp) 
		{
			currLevel++;
			currExp -= lvlArray[currLevel].exp;
		}

		Health -= 0;

		livesText.text = "Lives: " + lives;
		damageText.text = "Damage: " + damage;
		healthText.text = "Health: " + (int)currentHealthPoints + "/" + maxHealthPoints;
		armorText.text = "Armor: " + armor;
		essenceText.text = "Essence: " + essence.name;
		expText.text = "Exp: " + currExp + "/" + lvlArray [currLevel + 1].exp;
		essenceIcon.sprite = essence.artwork;


		if (currentHealthPoints > maxHealthPoints) {
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
		GameObject clone;
		clone = Instantiate(popoutDamage, transform.parent.position, transform.parent.rotation);
		clone.GetComponentInChildren<PopoutDamage>().damageText.text = damagesPoints + "";
    }

    public void Respawn()
    {
		transform.parent.transform.position = spawnPoints [Random.Range (0, spawnPoints.Length)].transform.position;
		currentHealthPoints = maxHealthPoints;
    }

    public void Heal()
    {
        if (currentHealthPoints <= maxHealthPoints)
        {
			currentHealthPoints += (1/(100/currentHealthPoints)) * Time.deltaTime;
        }
    }

	public void EnterGas(float percent)
	{
		if (currentHealthPoints >= 0)
		{
			currentHealthPoints -= (percent/(100/currentHealthPoints)) * Time.deltaTime;
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
		skin = essence.skin;

		GameObject oldModel = bodyTransform;
		GameObject newModel =  Instantiate(skin, bodyTransform.transform.position, bodyTransform.transform.rotation);
		newModel.transform.SetParent (transform.parent);
		playerController.body = newModel;
		playerController.animator = newModel.GetComponent<Animator> ();
		playerController.attackTrigger = newModel.transform.Find ("AttackTrigger").gameObject;
		playerController.attackTrigger.GetComponent<AttackTrigger> ().playerStats = this.gameObject.GetComponent<PlayerStats> ();
		playerController.attackTrigger.GetComponent<AttackTrigger> ().popOutTransform = this.gameObject.transform.parent;
		bodyTransform = newModel;
		playerSyncPosition.myTransform = newModel.transform;
		playerSyncRotation.playerTransform = newModel.transform;
		networkAnimator.animator = newModel.GetComponent<Animator> (); 
		Destroy (oldModel);

		maxHealthPoints = essence.defaultHealth;
		damage = essence.defaultDamage;
		armor = essence.defaultArmor;

		Health -= 0;
	}

	void LerpHealth()
	{
		if (!isLocalPlayer) 
		{
			currentHealthPoints = curHealth;
		}
	}

	[Command]
	void CmdProvideHealthToServer(float health)
	{
		curHealth = Health;
	}

	[ClientCallback]
	void TransmitHealth()
	{
		if (isLocalPlayer) {
			CmdProvideHealthToServer (curHealth);
		}
	}
}

[System.Serializable]
public class LevelArray
{
	public int level;
	public float exp;
}
