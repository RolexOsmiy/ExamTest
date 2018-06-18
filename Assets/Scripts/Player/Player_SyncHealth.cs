using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_SyncHealth : NetworkBehaviour {

	public PlayerStats playerStats;

	void FixedUpdate () 
	{
		if (!isLocalPlayer) 
		{
			CmdSendHealthToServer (playerStats.Health);
		} else 
		{
			RpcSetPlayerHealth (playerStats.Health);
		}
	}

	[Command]
	void CmdSendHealthToServer(float healthToSend)
	{
		RpcSetPlayerHealth(healthToSend);
	}

	[ClientRpc]
	void RpcSetPlayerHealth(float hp)
	{
		playerStats.Health = hp;
	}
}
