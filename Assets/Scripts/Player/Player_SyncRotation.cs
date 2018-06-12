using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_SyncRotation : NetworkBehaviour {

	[SyncVar] private Quaternion syncPlayerRotation;
	[SyncVar] private Quaternion syncCamRotation;

	[SerializeField]public Transform playerTransform;
	[SerializeField]private float lerpRate = 15;

	void FixedUpdate () {
		TransmitRotations ();
		LerpRotations ();
	}

	void LerpRotations()
	{
		if (!isLocalPlayer) 
		{
			playerTransform.rotation = Quaternion.Lerp (playerTransform.rotation, syncPlayerRotation, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvideRotationsToServer(Quaternion playerRot)
	{
		syncPlayerRotation = playerRot;
	}

	[Client]
	void TransmitRotations()
	{
		if (isLocalPlayer) {
			CmdProvideRotationsToServer (playerTransform.rotation);
		}
	}
}
