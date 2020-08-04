using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoDisplay : MonoBehaviour
{

    public GameObject cTuto, cShiftTuto;
    float timer;
    public static bool hasDisplayedC, hasDisplayedCShift;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 3 && timer < 7)
        {
            if (!hasDisplayedC)
            {
                cTuto.SetActive(true);
                hasDisplayedC = true;
            }
        }
        else
        {
            cTuto.SetActive(false);
        }

        if (timer > 9 && timer < 16)
        {
            if(!hasDisplayedCShift)
            {
                cShiftTuto.SetActive(true);
                hasDisplayedCShift = true;
            }
        }
        else
        {
            cShiftTuto.SetActive(false);
        }
    }
}
