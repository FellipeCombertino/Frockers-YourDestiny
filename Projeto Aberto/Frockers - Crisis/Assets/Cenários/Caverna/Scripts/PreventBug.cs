using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PreventBug : MonoBehaviour
{

    void ReloadScene()
    {
        SceneManager.LoadScene("Caverna");
        Npc.currentNPCForQuest = 13;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ReloadScene();
        }
    }
}
