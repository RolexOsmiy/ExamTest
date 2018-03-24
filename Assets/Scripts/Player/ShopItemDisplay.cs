using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemDisplay : MonoBehaviour {

	public Item item;

	public GameObject toolTip;

	public Text nameText;
	public Text descriptionText;
	public Text costText;
	public Text attackText;
	public Text healthText;
	public Text armorText;

	public Image artworkImage;
	public Sprite emptyArtworkImage;

	public bool display = false;



	void Start () 
	{
		
	}

	void Update () 
	{
		if (display & item) 
		{
			toolTip.SetActive (true);
			nameText.text = "" + item.name;
			descriptionText.text = "" + item.description;
			costText.text = "Cost:" + item.cost;
			attackText.text = "Damage:" + item.damage;
			healthText.text = "Health:" + item.health;
			armorText.text = "Armor:" + item.armor;
		} 
		else 
		{
			toolTip.SetActive (false);
		}

		if (item) 
		{
			artworkImage.sprite = item.artwork; 
		} 
		else 
		{
			artworkImage.sprite = emptyArtworkImage;
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
