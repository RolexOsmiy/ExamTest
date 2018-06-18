using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class Player_SyncName : NetworkBehaviour {


	private string userName;

	[SerializeField] Transform myTransform;
	public TextMeshProUGUI userNameText;

	void FixedUpdate () 
	{
		if (isLocalPlayer) 
		{
			CmdSendNameToServer (UserRegistration.inputUserName);
		}
	}

	[Command]
	void CmdSendNameToServer(string nameToSend)
	{
		RpcSetPlayerName(nameToSend);
	}

	[ClientRpc]
	void RpcSetPlayerName(string name)
	{
		userNameText.SetText (name);
		myTransform.name = name;
	}
}
