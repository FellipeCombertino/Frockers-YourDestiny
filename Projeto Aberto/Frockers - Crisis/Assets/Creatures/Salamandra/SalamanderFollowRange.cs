using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderFollowRange : MonoBehaviour
{
    public Salamander mySalamander;
            
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Salamander"))
        {
            mySalamander.isInsideMaxRange = true;            
        }        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Salamander"))
        {
            mySalamander.isInsideMaxRange = false;
            if (mySalamander.salamanderCurrentState == Salamander.SalamanderState.Chasing)
            {
                mySalamander.salamanderCurrentState = Salamander.SalamanderState.Returning;
            }
        }
    }
}
