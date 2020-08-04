using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hack : MonoBehaviour
{

    public GameObject panel;
    public Text questAtual;
    public InputField campoInput;
    bool panelOpened;

    // Update is called once per frame
    void Update()
    {        
        if (!panel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                SceneManager.LoadScene(1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                SceneManager.LoadScene(2);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                SceneManager.LoadScene(3);
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SceneManager.LoadScene(4);
            }

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                SceneManager.LoadScene(5);
            }

            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene(6);
            }
        }

        if (Input.GetKeyDown (KeyCode.Tab)) {
            panel.SetActive(!panel.activeInHierarchy);

            if (panelOpened)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                panelOpened = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                panelOpened = true;
            }
        }

        questAtual.text = "Quest atual: " + Npc.currentNPCForQuest.ToString();              
    }

    public void ChangeQuest()
    {
        Npc.currentNPCForQuest = int.Parse(campoInput.text);
        campoInput.text = "";
    }

}
