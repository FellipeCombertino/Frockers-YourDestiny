using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaveFog : MonoBehaviour
{
    public string sceneName;
    public int questToEnter;
    public GameObject marker;

    private void Update()
    {
        if (questToEnter == Npc.currentNPCForQuest)
        {
            marker.SetActive(true);
        }
        else
        {
            marker.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Npc.currentNPCForQuest >= questToEnter)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
