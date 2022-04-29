using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInventory))]
public class PlayerInventoryHUD : MonoBehaviour
{
    public GameObject playerInventoryHUD;
    public Image[] vegetableIMGs = new Image[6];
    private Image iconOne;
    private Image iconTwo;

    readonly private float iconScale = 1.5f;

    //call to add a veggie icon to hud
    public void CreateCarriedIcon(int veggie)
    {
        if (iconTwo == null)
        {
            playerInventoryHUD.SetActive(true);
            GameObject newIcon = GameObject.Instantiate(new GameObject(), playerInventoryHUD.transform);
            Image icon = newIcon.AddComponent<Image>();
            icon.transform.localScale *= iconScale;
            icon.sprite = vegetableIMGs[veggie].sprite;
            icon.color = vegetableIMGs[veggie].color;
            icon.name = vegetableIMGs[veggie].name;

            if (iconOne == null)
            {
                iconOne = icon;
            }
            else
            {
                iconTwo = icon;
            }
        }
    }

    //remove and shift icons
    public void RemoveIcon()
    {
        Destroy(iconOne.gameObject);
        if (iconTwo != null)
        {
            iconOne = iconTwo;
            iconTwo = null;
        }
        else
        {
            playerInventoryHUD.SetActive(false);
        }
    }
}
