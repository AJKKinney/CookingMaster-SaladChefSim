using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Use With playerMovementController to animate player
public class PlayerAnimationManager : MonoBehaviour
{
    public Animator playerAnimator;


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

}
