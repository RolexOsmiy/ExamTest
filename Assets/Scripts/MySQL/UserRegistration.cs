using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

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

	/*public static string inputPassword = "NONE";

	public string[] playerInfoValues;

	string CreateUserURL = "http://rawdex.pro/register.php";

	string LoginUserURL = "http://rawdex.pro/login.php";

	public Text errorText;

	public GameObject loginObj;
	//public GameObject registrationObj;
	public GameObject matchMaker;

	public Text usernameLogin;
	public Text passwordLogin;

	bool login = false;

	
	public Text winsText;
	public Text losesText;


	 [SyncVar] public string playerUniqueIdentity;
	NetworkInstanceId playerNetID;
	Transform myTransform; 

	public override void OnStartLocalPlayer()
	{
		GetNetIdentity();
		SetIdentity ();
	}

	void Awake()
	{
		myTransform = transform;
	}

	void Update()
	{
		if (myTransform.name == "" || myTransform.name == "Player(Clone)") {
			SetIdentity ();
		}
	}

	[Client]
	void GetNetIdentity()
	{
		playerNetID = GetComponent<NetworkIdentity> ().netId;
		CmdTellServerMyIdentity (MakeUniqueIdentity ());
	}

	void SetIdentity()
	{
		if (isLocalPlayer) {
			myTransform.name = playerUniqueIdentity;
		} else {
			myTransform.name = MakeUniqueIdentity();
		}
	}

	string MakeUniqueIdentity()
	{
		string uniqueName = inputUserName;
		return uniqueName;
	}

	[Command]
	void CmdTellServerMyIdentity(string name)
	{
		playerUniqueIdentity = name;
	}

	public void CreateUser(string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("passwordPost", password);

		WWW www = new WWW (CreateUserURL, form);
	}

	public void SetUsername(Text username)
	{
		inputUserName = username.text;
	}

	public void SetPassword(Text password)
	{
		inputPassword = password.text;
	}

	public void RegisterUser()
	{
		if (!string.IsNullOrEmpty (inputUserName) && !string.IsNullOrEmpty (inputPassword)) {
			CreateUser (inputUserName, inputPassword);
			errorText.text = "";
			loginObj.SetActive (true);
			registrationObj.SetActive (false);
		} 
		else 
		{
			errorText.text = "Неверное имя или пароль.";
		}
	}

	public void Login()
	{
		if (login) {
			login = false;	
		} else {
			login = true;
		}
	}

	void Update()
	{
		if (login) 
		{
			StartCoroutine (LoginDB(usernameLogin.text, passwordLogin.text));
		}
	}

	IEnumerator LoginDB(string username, string password)
	{
		WWWForm form = new WWWForm ();
		form.AddField ("usernamePost", username);
		form.AddField ("passwordPost", password);

		WWW playerInfo = new WWW (LoginUserURL, form);

		yield return playerInfo;
		string playerInfoValuesString = playerInfo.text;
		print (playerInfoValuesString);
		playerInfoValues = playerInfoValuesString.Split (';');
		usernameText.text = GetUserInfo (playerInfoValues [0], "|Username: ");
		winsText.text = "Wins: " + GetUserInfo (playerInfoValues [0], "|Wins: ");
		losesText.text = "Loses: " + GetUserInfo (playerInfoValues [0], "|Loses: ");

		globalUsername = GetUserInfo (playerInfoValues [0], "|Username: ");
		globalWins = int.Parse(GetUserInfo (playerInfoValues [0], "|Wins: "));
		glovalLoses = int.Parse(GetUserInfo (playerInfoValues [0], "|Loses: "));

		//StopCoroutine (LoginDB(usernameLogin.text, passwordLogin.text));
		loginObj.SetActive (false);
		matchMaker.SetActive (true);
	}

	string GetUserInfo(string data, string index)
	{
		string value = data.Substring (data.IndexOf (index) + index.Length);
		if (value.Contains("|")){
			value = value.Remove (value.IndexOf ("|"));
		}
		return value;		
	}*/

}
