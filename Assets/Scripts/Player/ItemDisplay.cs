using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class ItemDisplay : NetworkBehaviour {

    public Item item;

    public GameObject toolTip;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Sprite emptyArtworkImage;

    public Text costText;
    public Text attackText;
    public Text healthText;

    bool display = false;

	public PlayerStats playerStats;

	public Shop shop;


    void Update()
    {
        if (item)
        {
            artworkImage.sprite = item.artwork;
        }
    }

	void FixedUpdate () {
        if (display & item)
        {
            toolTip.SetActive(true);
            toolTip.transform.position = Input.mousePosition + new Vector3(100,100,0);
            nameText.text = "" + item.name;
            descriptionText.text = "" + item.description;
            costText.text = "Cost:" + item.cost;
			attackText.text = "Damage:" + item.damage;
			healthText.text = "Health:" + item.health;
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (!item)
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

    public void SellItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
			shop.gold += item.cost / 2;            
			playerStats.damage -= item.damage;
			playerStats.MaxHealthPoints = playerStats.MaxHealthPoints - item.health;
			item = null;
			print (playerStats.damage);
        }        
    }
}
