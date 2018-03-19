using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour {

    public Item item;

    public GameObject toolTip;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;
    public Sprite emptyArtworkImage;

    public Text costText;
    public Text attackText;
    public Text healthText;

    public bool display = false;

    void Start()
    {
             
    }

    void Update()
    {
        if (item)
        {
            artworkImage.sprite = item.artwork;
        }
    }

	void FixedUpdate () {
        if (display & item)
        {
            toolTip.SetActive(true);
            toolTip.transform.position = Input.mousePosition + new Vector3(100,100,0);
            nameText.text = "" + item.name;
            descriptionText.text = "" + item.description;
            /* costText.text = "" + item.cost;
             attackText.text = "" + item.attack;
             healthText.text = "" + item.health;*/
        }
        else
        {
            toolTip.SetActive(false);
        }
        if (!item)
        {
            artworkImage.sprite = emptyArtworkImage;
        }
    }

    public void OnMouseOver()
    {
        display = true;
    }

    public void OnMouseExit()
    {
        display = false;
    }

    public void SellItem()
    {
        if (Input.GetMouseButtonDown(1))
        {
            item = null;
        }        
    }
}
