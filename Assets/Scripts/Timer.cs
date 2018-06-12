using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Timer : NetworkBehaviour {

	public bool isGame = true;
	[SyncVar]
	public bool pause = true;

	public float timer = 0;
	public float sec = 0;
	public float min = 0;

	public Text timerText;
	public GameObject readyButton;
	public Text readyText;

	public GameObject[] objectsToDisable;

	public GameObject[] players;

	void Start () 
	{
		
	}

	void Update () 
	{
		players = GameObject.FindGameObjectsWithTag("Player");

		if (!isServer) 
		{			
			readyButton.SetActive (false);
		}

		if (!pause) 
		{
			for (int i = 0; i < objectsToDisable.Length; i++) {
				objectsToDisable [i].SetActive(false);
				pause = false;
			}
			for (int i = 0; i < players.Length; i++) 
			{
				Time.timeScale = 1;
			}

			timer += Time.deltaTime;

			string minutes = Mathf.Floor (timer / 60).ToString ("00");
			string seconds = (timer % 60).ToString ("00");

			timerText.text = string.Format ("{0}:{1}", minutes, seconds);
		} else 
		{
			for (int i = 0; i < players.Length; i++) 
			{
				timerText.text = "Pause!";
				Time.timeScale = 0;
			}

		}
	}

	public void Ready()
	{		
		if (isServer) 
		{
			pause = false;
			readyButton.SetActive (false);
			print ("ready");
		}
	}
}
