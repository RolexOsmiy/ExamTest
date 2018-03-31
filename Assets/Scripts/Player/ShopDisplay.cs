using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShopDisplay : NetworkBehaviour {

    public bool shop;
    private Vector3 buttonVelocity = Vector3.zero;

    void Start () {
		
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.M) & !shop)
		{
			shop = true;

		}
		else if (Input.GetKeyDown(KeyCode.M) & shop)
		{
			shop = false;
		}


        if (shop)
        {
			GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.gameObject.transform.localPosition, new Vector2(Screen.width / 2 - 340, Screen.height / 2 + 5), ref buttonVelocity, 0.1f);
        }
        else
        {
            GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.gameObject.transform.localPosition, new Vector2(Screen.width / 2 + 340, Screen.height / 2 + 5), ref buttonVelocity, 0.1f);
        }
	}

	public void ShowUIShop()
	{
		if (!shop)
		{
			shop = true;

		}
		else if (shop)
		{
			shop = false;
		}
	}
}
