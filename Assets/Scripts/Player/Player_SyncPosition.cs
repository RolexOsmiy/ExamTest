using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player_SyncPosition : NetworkBehaviour {

	[SyncVar]
	private Vector3 syncPos;

	[SerializeField]public Transform myTransform;
	[SerializeField] float lerpRate = 15;	

	void FixedUpdate () 
	{
		TransmitPosition ();
		LerpPosition ();
	}

	void LerpPosition()
	{
		if (!isLocalPlayer) 
		{
			myTransform.position = Vector3.Lerp (myTransform.position, syncPos, Time.deltaTime * lerpRate);
		}
	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer) {
			CmdProvidePositionToServer (myTransform.position);
		}
	}
}
