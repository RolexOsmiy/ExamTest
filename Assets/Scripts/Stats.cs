using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    /* public float attackSpeed;
     public float damage;
     public float movemmentSpeed;*/

    public float exp;

    [System.Serializable]
    public class lvl
    {
        public float currExp = 0;
        public float needExp = 120;
    }
    [Header("Level System")]
    public int currLevel = 0;
    public lvl[] level;


    [SerializeField]
    private Image image;

    [SerializeField]
    private float maxHealthPoints = 100;

    [SerializeField]
    private float healthBarStepsLength = 10;

    [SerializeField]
    private float damagesDecreaseRate = 10;

    private float currentHealthPoints;
    public GameObject healEffect;

    private RectTransform imageRectTransform;

    private float damages;

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
        if (Health < 1)
        {
            this.gameObject.SetActive(false);
        }
        if (Damages > 0)
        {
            Damages -= damagesDecreaseRate * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Hurt(10);
        }

        image.gameObject.transform.rotation = Quaternion.Euler(90, -90, 0);
    }

    public float Hurt(float damagesPoints)
    {
        float returnExp = 0;
        Damages = damagesPoints;
        Health -= Damages;
        if (Health < 1)
        {
            returnExp = exp;            
        }        
        return returnExp;
    }

    public void Respawn()
    {
        currentHealthPoints = maxHealthPoints;
    }

    public void Heal(float reg)
    {
        if (currentHealthPoints <= maxHealthPoints)
        {
            currentHealthPoints += reg * Time.deltaTime;
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

    void Start()
    {
        
    }

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "HealSpot" & currentHealthPoints < maxHealthPoints)
        {
            Health += coll.GetComponent<HealSpot>().regPerSec * Time.deltaTime;
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
}
