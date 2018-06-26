using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Player_SyncHealth : NetworkBehaviour {

	public PlayerStats playerStats;
    

    void Start()
    {
        
    }

    void FixedUpdate () 
	{
        if (playerStats.Health < 1)
        {
            playerStats.Health = playerStats.MaxHealthPoints;

            // called on the Server, but invoked on the Clients
            RpcRespawn();
        }
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

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            print("respawn");
            // move back to zero location
            playerStats.lives -= 1;
            transform.position = new Vector3(5, 20, 1);
        }
    }
}
