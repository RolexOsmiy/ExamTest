﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Essence", menuName = "Essence")]
public class Essence : ScriptableObject {

	public string name;
	public string description;
	public string race;

	public int branch;
	public int toBranch;

	public Sprite artwork;

	public GameObject skin;

	public int requiredLevel;
	public int defaultDamage;
	public int defaultHealth;
	public int defaultArmor;
	public float attackSpeed;
}
