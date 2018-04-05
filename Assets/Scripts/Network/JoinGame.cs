﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

	List<GameObject> roomList = new List<GameObject> ();

	NetworkMatch matchMaker;

	[SerializeField]
	Text status;

	[SerializeField]
	GameObject  roomListItemPrefab;

	[SerializeField]
	Transform roomListParent;

	NetworkManager networkManager;

	void Start()
	{
		matchMaker = gameObject.AddComponent<NetworkMatch>();
		networkManager = NetworkManager.singleton;
		if (networkManager.matchMaker == null) 
		{
			networkManager.StartMatchMaker ();
		}
		RefreshRoomList ();
	}

	public void RefreshRoomList()
	{
		networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);
		status.text = "Loading...";
	}

	public void OnMatchList(bool succes,string extendedInfo,List<MatchInfoSnapshot> matchList)
	{
		status.text = "";
		if (matchList == null) 
		{
			status.text = "Couldn't get room list.";
			return;
		}

		ClearRoomList ();

		foreach (MatchInfoSnapshot match in matchList) 
		{
			GameObject _roomListItemGO = Instantiate (roomListItemPrefab);
			_roomListItemGO.transform.SetParent (roomListParent);


			RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem> ();
			if (_roomListItem != null) 
			{
				_roomListItem.Setup (match, JoinRoom);
			}

			roomList.Add (_roomListItemGO);
		}

		if (roomList.Count == 0) 
		{
			status.text = "No rooms at the moment.";	
		}
	}

	void ClearRoomList()
	{
		for (int i = 0; i < roomList.Count; i++) {
			Destroy (roomList [i]);
		}
		roomList.Clear ();
	}

	public void JoinRoom(MatchInfoSnapshot _match)
	{
		networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
		ClearRoomList ();
		status.text = "JOINING...";
	}

}
