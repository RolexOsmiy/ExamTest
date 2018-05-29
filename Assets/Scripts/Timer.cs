using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public bool isGame = true;

	public float timer = 0;
	public float sec = 0;
	public float min = 0;

	public Text timerText;

	void Start () 
	{
		
	}

	void Update () 
	{
		timer += Time.deltaTime;

		string minutes = Mathf.Floor(timer / 60).ToString("00");
		string seconds = (timer % 60).ToString("00");

		timerText.text = string.Format("{0}:{1}", minutes, seconds);
	}
}
