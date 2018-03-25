using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EssenceDisplay : MonoBehaviour {

	public Essence essence;

	public Text nameText;
	public Text descriptionText;
	public Text raceText;

	public Image essenceIcon;

	public Text requiredLevelText;
	public Text defaultDamageText;
	public Text defaultHealthText;
	public Text defaultArmorText;

	bool display = false;

	public GameObject toolTip;

	void Start () 
	{
		essenceIcon.sprite = essence.artwork;
	}
	

	void Update () 
	{
		if (display)
		{
			toolTip.SetActive(true);
			toolTip.transform.position = Input.mousePosition + new Vector3(100,100,0);
			nameText.text = essence.name;
			descriptionText.text = essence.description;
			raceText.text = essence.race;
			requiredLevelText.text = "Level" + essence.requiredLevel;
			defaultDamageText.text = "Damage: " + essence.defaultDamage;
			defaultHealthText.text = "Health: " + essence.defaultHealth;
			defaultArmorText.text = "Armor: " + essence.defaultArmor;
		}
		else
		{
			toolTip.SetActive(false);
		}
	}

	public void OnMouseOver()
	{
		display = true;
	}

	public void OnMouseExit()
	{
		display = false;
	}
}
