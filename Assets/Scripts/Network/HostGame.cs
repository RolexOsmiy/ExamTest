﻿using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class HostGame : MonoBehaviour {


	[SerializeField]
	uint roomSize =  10;
	//public Text roomSizeText;

	string roomName;

	public NetworkManager networkManager;

    public Text statusText;

	void Start()
	{
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        networkManager = NetworkManager.singleton;

        if (networkManager.matchMaker == null) 
		{
			networkManager.StartMatchMaker ();
		}
	}

	public void SetRoomName (string _name)
	{
		roomName = _name;
	}

	public void CreateRoom()
	{
		if (roomName != "" && roomName != null) 
		{
            statusText.text = "Creating room";
			print ("Create room " + roomName + " With room size for " + roomSize + " players;");

			//Create room
			networkManager.matchMaker.CreateMatch(roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
		}
	}

	public void Exit()
	{
		Application.Quit();
	}
}
