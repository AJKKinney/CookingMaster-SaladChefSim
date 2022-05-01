using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmsAnimator : MonoBehaviour
{
    public MultiRotationConstraint rightArmConstraint;
    public MultiRotationConstraint leftArmConstraint;
    private GameObject rightHandGrip;
    private GameObject leftHandGrip;

    public GameObject[] veggies;
    public GameObject salad;



    public void RaiseLeftArm()
    {
        leftArmConstraint.weight = 1;
    }

    public void RaiseRightArm()
    {
        rightArmConstraint.weight = 1;
    }

    public void LowerRightArm()
    {
        rightArmConstraint.weight = 0;
    }

    public void LowerLeftArm()
    {
        leftArmConstraint.weight = 0;
    }

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


    //removes objects from hands
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

    public void CarrySalad()
    {
        salad.SetActive(true);

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

    public void ClearSalad()
    {

        salad.SetActive(false);
        LowerRightArm();

    }
}
