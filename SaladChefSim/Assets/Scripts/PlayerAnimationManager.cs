using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Use With playerMovementController to animate player
public class PlayerAnimationManager : MonoBehaviour
{
    public Animator playerAnimator;
    [HideInInspector]
    public ArmsAnimator armsAnimator;


    //sets the walkspeed variable and scales playback speed 
    public void SetWalkSpeed(float speed)
    {
        playerAnimator.SetFloat("MoveSpeed", speed);
        if (speed > 0.1f)
        {
            playerAnimator.speed = speed;
        }
        else
        {
            playerAnimator.speed = 1;
        }
    }


    //adds vegetables to the players hand
    public void AddVeggie(int vegIndex, bool rightHand)
    {
        armsAnimator.AddToHand(vegIndex, rightHand);
    }


    //removes vegetables from the players hand
    public void RemoveVeggie()
    {
        armsAnimator.ClearHand();
    }


    //adds a salad to the players hand
    public void AddSalad(Mixture mixture)
    {
        armsAnimator.CarrySalad(mixture);
    }


    //removes a salad from the players hand
    public void RemoveSalad()
    {
        armsAnimator.ClearSalad();
    }
}
