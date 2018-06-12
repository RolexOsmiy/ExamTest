using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerSetup : NetworkBehaviour {

	[SerializeField]
	Behaviour[] componentsToDisable;

	public GameObject usersList;

	void Start () 
	{
		if (!isLocalPlayer) 
		{
			for (int i = 0; i < componentsToDisable.Length; i++) {
				componentsToDisable [i].enabled = false;
			}
		}
		usersList = GameObject.FindGameObjectWithTag("UsersList");
		usersList.GetComponent<Text> ().text = transform.name + ";\n";
		usersList.GetComponent<Text>().text.Replace("NEWLINE","\n");

	}
}
