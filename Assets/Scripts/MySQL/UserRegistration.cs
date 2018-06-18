using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class UserRegistration : NetworkBehaviour {

	public static string inputUserName = "qwe";
	public Text usernameTextMM;
	public GameObject login;
	public GameObject matchMaker;

	public void SetUserName(Text usernameText)
	{
		if (!string.IsNullOrEmpty (usernameText.text)) 
		{
			inputUserName = usernameText.text;
			usernameTextMM.text = inputUserName;
			login.SetActive (false);
			matchMaker.SetActive (true);
		}
    }

}
