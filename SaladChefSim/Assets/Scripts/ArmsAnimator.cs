using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;


[RequireComponent(typeof(SaladGFXController))]
// this class controls the animation state of the arms and the items they carry
public class ArmsAnimator : MonoBehaviour
{
    [Header("Arm Constraints")]
    public MultiRotationConstraint rightArmConstraint;
    public MultiRotationConstraint leftArmConstraint;

    [Header("Objects to Carry")]
    public GameObject[] veggies;


    //stores the objects in hand to be referenced later
    private GameObject rightHandGrip;
    private GameObject leftHandGrip;
    //controls salad display
    private SaladGFXController saladGFX;


    private void Awake()
    {
        //initialize
        saladGFX = GetComponent<SaladGFXController>();
    }


    //locks left arm to rotation
    public void RaiseLeftArm()
    {
        leftArmConstraint.weight = 1;
    }


    //locks right arm to rotation
    public void RaiseRightArm()
    {
        rightArmConstraint.weight = 1;
    }


    //allows regular animation of left arm
    public void LowerLeftArm()
    {
        leftArmConstraint.weight = 0;
    }


    //allows regular animation of right arm
    public void LowerRightArm()
    {
        rightArmConstraint.weight = 0;
    }


    //Adds the vegetable specified to the hand specified
    public void AddToHand(int vegetableIndex, bool rightHand)
    {
        if (rightHand == true)
        {
            rightHandGrip = Instantiate(veggies[vegetableIndex], rightArmConstraint.data.sourceObjects[0].transform, true);
            rightHandGrip.transform.position = rightArmConstraint.data.sourceObjects[0].transform.position;
            RaiseRightArm();
        }
        else
        {
            leftHandGrip = Instantiate(veggies[vegetableIndex], leftArmConstraint.data.sourceObjects[0].transform, true);
            leftHandGrip.transform.position = leftArmConstraint.data.sourceObjects[0].transform.position;
            RaiseLeftArm();
        }

    }


    //removes objects from hands in FIFO order
    public void ClearHand()
    {
        if(leftHandGrip != null)
        {
            GameObject toDel;
            toDel = rightHandGrip;
            rightHandGrip = leftHandGrip;
            rightHandGrip.transform.position = toDel.transform.position;
            Destroy(toDel);
            LowerLeftArm();
            leftHandGrip = null;
        }
        else
        {
            Destroy(rightHandGrip);
            LowerRightArm();
        }
    }


    //Adds a salad to the players hand
    public void CarrySalad(Mixture mixture)
    {
        saladGFX.displaySalad(mixture);

        if(rightHandGrip != null)
        {
            Destroy(rightHandGrip);
        }
        if (leftHandGrip != null)
        {
            Destroy(rightHandGrip);
        }
        RaiseRightArm();
    }


    //removes the salad from the players hand
    public void ClearSalad()
    {

        saladGFX.HideSalad();
        LowerRightArm();

    }
}
