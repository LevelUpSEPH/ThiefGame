using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    public List<bool> isFull;
    public GameObject backpackDisplay;
    public Button bt;
    public List<int> slot;
    public int count = 0;
    public List<Sprite> gallery;
    void Start()
    {
        bt.onClick.AddListener(setToggle);
        for (int i = 0; i < isFull.Count; i++)
            isFull[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
    

    }
    public void setToggle()
    {
        if (backpackDisplay.activeSelf)
        {
            backpackDisplay.SetActive(false);
        }
        else
            backpackDisplay.SetActive(true);

       
    }
    public void addToInventory(int objType ) // Add to inventory
    {
        if (!isFull[count])
        {
            slot.Add(objType);
            if (objType == 0)
            {
                backpackDisplay.transform.GetChild(count).gameObject.SetActive(true);
                backpackDisplay.transform.GetChild(count).GetComponent<Button>().image.sprite = gallery[0];
                backpackDisplay.transform.GetChild(count).GetComponent<ButtonScript>().objType = objType;
            }
            count++;
            isFull[count] = true;
        }
    }
}