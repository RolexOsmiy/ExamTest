using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class EvolutionSystem : NetworkBehaviour
{
	public EssenceArray[] essenceArray;
	public int _currentBranch;
	public Slot[] slots;

	public bool ShowDialogue = true;

	public PlayerStats playerStats;

	void Update()
	{
		if (ShowDialogue) 
		{
			for (int i = 0; i < essenceArray.Length; i++) 
			{
				if (playerStats.lvl >= essenceArray [i].minLvl & _currentBranch == essenceArray [i].essence.branch) {
					print ("activate!");
					slots [i].slotObject.GetComponent<EssenceDisplay> ().essence = essenceArray [i].essence;
					slots [i].slotObject.SetActive (true);
				} 
				else 
				{
					print ("deactivate!");
					slots [i].slotObject.SetActive (false);
				}
			}
		}
	}

	public void Evolution(GameObject slot)
	{
		_currentBranch = slot.GetComponent<EssenceDisplay>().essence.toBranch;
		playerStats.essence = slot.GetComponent<EssenceDisplay>().essence;
		playerStats.EvolutionSetStats ();
		print (_currentBranch);
	}
}

[System.Serializable]
public class EssenceArray
{
	public Essence essence;
	public int minLvl;
}


[System.Serializable]
public class Slot
{
	public GameObject slotObject;
}

