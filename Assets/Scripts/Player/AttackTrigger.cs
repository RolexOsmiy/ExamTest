using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour {

	public PlayerStats playerStats;
	public float attackSpeed;
	public float currAttackSpeed;
	public static float expForKills = 100;
	public GameObject popOutExp;
	public Transform popOutTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		attackSpeed = playerStats.essence.attackSpeed;
		currAttackSpeed += Time.deltaTime;
	}

	void OnTriggerStay(Collider coll)
	{		
		if (currAttackSpeed >= attackSpeed) 
		{
			if (coll.gameObject.GetComponent<CharacterStats> ()) 
			{
				coll.gameObject.GetComponent<CharacterStats> ().Hurt ((int)playerStats.damage);
				if (coll.gameObject.GetComponent<CharacterStats>().Health <= 0) 
				{
					playerStats.currExp += coll.gameObject.GetComponent<CharacterStats> ().exp;
					GameObject clone = Instantiate(popOutExp, popOutTransform.position, popOutTransform.rotation);
					clone.GetComponentInChildren<PopoutDamage>().damageText.text = coll.gameObject.GetComponent<CharacterStats> ().exp.ToString();
					Destroy (coll.gameObject);
				}
				currAttackSpeed = 0;
			}

			if (coll.gameObject.GetComponent<PlayerStats> ()) 
			{
				coll.gameObject.GetComponent<PlayerStats> ().Hurt ((int)playerStats.damage);
				if (coll.gameObject.GetComponent<PlayerStats>().currentHealthPoints <= 0) 
				{
					playerStats.currExp += expForKills * coll.gameObject.GetComponent<PlayerStats>().currLevel;
					GameObject clone = Instantiate(popOutExp, popOutTransform.position, popOutTransform.rotation);
					clone.GetComponentInChildren<PopoutDamage>().damageText.text = expForKills * coll.gameObject.GetComponent<PlayerStats>().currLevel + "";
					clone.GetComponent<PlayerStats>().Respawn();
				}
				currAttackSpeed = 0;
			}

			if (coll.gameObject.GetComponent<InnocentController> ()) 
			{
				coll.gameObject.GetComponent<InnocentController> ().Hurt ((int)playerStats.damage);
				if (coll.gameObject.GetComponent<InnocentController>().currentHealthPoints <= 0) 
				{
					playerStats.currExp += expForKills;
					GameObject clone = Instantiate(popOutExp, popOutTransform.position, popOutTransform.rotation);
					clone.GetComponentInChildren<PopoutDamage>().damageText.text = expForKills.ToString();
					Destroy (coll.gameObject);
				}
				currAttackSpeed = 0;
			}
		}
	}

}

