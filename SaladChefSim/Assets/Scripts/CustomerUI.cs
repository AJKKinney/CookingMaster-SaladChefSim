using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Controls the customer order UI
public class CustomerUI : MonoBehaviour
{
    public Canvas customerHUD;
    public Slider timerUI;
    public GameObject desiresPanel;

    public Image[] vegetableIMGs = new Image[6];
    private readonly float iconScale = 1.5f;


    //display the desired vegetables
    public void UpdateDesires(int[] desiredVeggies)
    {
        //clear previous desires
        foreach (Transform child in desiresPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        //populate with new desires
        for (int i = 0; i < vegetableIMGs.Length; i ++)
        {
            //create images
            for(int num = 0; num < desiredVeggies[i]; num++)
            {
                CreateDesire(i);
            }
        }
    }


    //updates timer display
    public void SetTimerUI(float timer, float maxtime)
    {
        timerUI.value = timer / maxtime;
    }


    //create desire icons for desired veggies
    public void CreateDesire(int veggie)
    {
        GameObject newIcon = GameObject.Instantiate(new GameObject(), desiresPanel.transform);
        Image icon = newIcon.AddComponent<Image>();
        icon.transform.localScale *= iconScale;
        icon.sprite = vegetableIMGs[veggie].sprite;
    }
}
