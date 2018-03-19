using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopDisplay : MonoBehaviour {

    public bool shop;
    public Vector2 show = new Vector2(3, 3);
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
            GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.gameObject.transform.localPosition, new Vector2(Screen.width / 2 - 150, Screen.height / 2 - 190), ref buttonVelocity, 0.1f);
        }
        else
        {
            GetComponent<RectTransform>().localPosition = Vector3.SmoothDamp(this.gameObject.transform.localPosition, new Vector2(Screen.width / 2 + 150, Screen.height / 2 - 190), ref buttonVelocity, 0.1f);
        }
	}
}
