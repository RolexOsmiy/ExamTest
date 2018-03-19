﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject {

    public string name;
    public string description;

    public Sprite artwork;

    public int cost;
    public int attack;
    public int health;

	public void Print()
    {
        Debug.Log(name + ": " + description + " Item cost: " + cost);
    }
}
