using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Livro : MonoBehaviour
{

    public bool doorOpened;

    private void OnTriggerStay (Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown (KeyCode.F))
            {
                doorOpened = true;
            }
        }
    }

}
