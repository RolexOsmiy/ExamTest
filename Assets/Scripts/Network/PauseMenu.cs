using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public GameObject[] Ui;
    public GameObject pauseMenu;
    NetworkManager networkManager;


    bool pauseOn = false;

    void Start()
    {
        networkManager = NetworkManager.singleton;
    }

	void Update ()
    {
        if (Input.GetButtonDown("Pause") && pauseOn == true)
        {
            for (int i = 0; i < Ui.Length; i++)
            {
                Ui[i].SetActive(true);
                pauseMenu.SetActive(false);
                pauseOn = false;
            }
        }
        else if (Input.GetButtonDown("Pause") && pauseOn == false)
        {
            for (int i = 0; i < Ui.Length; i++)
            {
                Ui[i].SetActive(false);
                pauseMenu.SetActive(true);
                pauseOn = true;
            }
        }
	}

    public void LeaveRoom()
    {
        /*MatchInfo matchInfo = networkManager.matchInfo;
        networkManager.matchMaker.DropConnection(matchInfo.networkId, matchInfo.nodeId, 0, networkManager.OnDropConnection);
        networkManager.StopHost();
        networkManager.client.Disconnect();*/
		SceneManager.LoadScene (0);
    }
}
