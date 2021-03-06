﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Shop : NetworkBehaviour {

	public ItemDisplay[] itemSlot; 

	public PlayerStats playerStats;

	public int gold = 200;
	public Text goldText;

	void Start () {
		
	}

	void Update () {
		goldText.text = "" + gold;
	}

	public void BuyItem(Item item)
	{
		if (Input.GetMouseButtonDown(1)) 
		{
			for (int i = 0; i < itemSlot.Length; i++) {
				if (itemSlot[i].item == null & gold >= item.cost) 
				{					
					itemSlot [i].item = item;
					playerStats.damage = playerStats.damage + itemSlot[i].item.damage;
					print (playerStats.damage);
					playerStats.MaxHealthPoints = playerStats.MaxHealthPoints + itemSlot[i].item.health;
					gold -= item.cost;
					playerStats.Hurt (0);
					break;
				}
			}
		}
	}
}
