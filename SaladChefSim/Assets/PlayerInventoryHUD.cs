using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerInventory))]
[RequireComponent(typeof(PlayerAnimationManager))]
public class PlayerInventoryHUD : MonoBehaviour
{
    public GameObject playerInventoryHUD;
    public Image[] vegetableIMGs = new Image[6];
    private PlayerAnimationManager animationManager;
    private Image iconOne;
    private Image iconTwo;

    readonly private float iconScale = 1.5f;

    private void Awake()
    {
        animationManager = GetComponent<PlayerAnimationManager>();
    }

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

                //animate hands
                animationManager.AddVeggie(veggie, true);
            }
            else
            {
                iconTwo = icon;

                //animate hands
                animationManager.AddVeggie(veggie, false);
            }
        }
    }

    //remove and shift icons
    public void RemoveIcon()
    {
        Destroy(iconOne.gameObject);
        if (iconTwo != null)
        {
            //animate hands
            animationManager.RemoveVeggie();

            //shift icons
            iconOne = iconTwo;
            iconTwo = null;
        }
        else
        {
            //animate hands
            animationManager.RemoveVeggie();

            //disable icons
            playerInventoryHUD.SetActive(false);
        }
    }
}
