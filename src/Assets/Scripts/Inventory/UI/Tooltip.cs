using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * script handling showing information about an item
 */ 
public class Tooltip : MonoBehaviour {

    [SerializeField]
    private GameObject tooltipPanel;
    [SerializeField]
    private Text text;
    [SerializeField]
    private float xOffset = 10f;
    [SerializeField]
    private float yOffset = 30f;

    private Item item;
    private RectTransform panelRT;
    private RectTransform textRT;
    private float sizeMultiplier;

	// Use this for initialization
	void Start () {
        tooltipPanel.SetActive(false);
        panelRT = tooltipPanel.GetComponent<RectTransform>();
        textRT = text.GetComponent<RectTransform>();
        sizeMultiplier = Screen.height / 1080f;
    }
	
	// Update is called once per frame
	void Update () {
       if(item != null)
        {
            SetPosition();
            Show();
            item = null;
        }
        else
        {
            Hide();
        }
	}

    private void SetPosition()
    {
        transform.position = Input.mousePosition + new Vector3(xOffset * sizeMultiplier, -yOffset * sizeMultiplier, 0);

        panelRT.sizeDelta = textRT.sizeDelta + new Vector2(5, 2);
        if (transform.position.x + panelRT.sizeDelta.x * sizeMultiplier > Screen.width)
        {
            transform.position = new Vector3(transform.position.x - panelRT.sizeDelta.x*sizeMultiplier, transform.position.y, transform.position.z);
        }
        if (transform.position.y - panelRT.sizeDelta.y * sizeMultiplier < 0)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + panelRT.sizeDelta.y * sizeMultiplier + yOffset*sizeMultiplier, transform.position.z);
        }
    }
    

    public void SetItem(Item newItem)
    {
        item = newItem;
        SetContent();
    }

    private void Hide()
    {
        tooltipPanel.SetActive(false);
    }

    private void Show()
    {
        tooltipPanel.SetActive(true);
    }

    private void SetContent()
    {
        text.text = item.getContentString();
    }

}
