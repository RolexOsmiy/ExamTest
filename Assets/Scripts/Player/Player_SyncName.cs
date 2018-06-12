using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Player_SyncName : NetworkBehaviour {

	[SyncVar]
	private string userName = UserRegistration.inputUserName;

	[SerializeField] Transform myTransform;
	public TextMeshProUGUI userNameText;
	[SerializeField] float lerpRate = 15;

	void FixedUpdate () 
	{
		TransmitName ();
		LerpName ();
	}

	void LerpName()
	{
		if (!isLocalPlayer) 
		{
			myTransform.name = userName;
			userNameText.SetText(userName);
		}
	}

	[Command]
	void CmdProvideNameToServer(string name)
	{
		myTransform.name = name;
		userNameText.SetText(name);
	}

	[Client]
	void TransmitName()
	{
		if (isLocalPlayer) {
			CmdProvideNameToServer (userName);
		}
	}
}
