using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueGame : MonoBehaviour
{
    string whichSceneAmIAt;
    public static bool hasClickedContinueButton;
    public GameObject myCamera;
    public TutoDisplay tutorial;

    void Start()
    {
        if (hasClickedContinueButton)
        {
            LoadSavedInfo();
        }
    }

    void Update()
    {
        DefineScene();
        Record();
    }

    void DefineScene()
    {
        whichSceneAmIAt = SceneManager.GetActiveScene().name;
    }

    void Record()
    {
        InfoToStoreToContinue infoToRecord = new InfoToStoreToContinue(transform.position, transform.rotation, Npc.currentNPCForQuest, whichSceneAmIAt);

        PlayerPrefs.SetFloat("PlayerPosX", infoToRecord.position.x);
        PlayerPrefs.SetFloat("PlayerPosY", infoToRecord.position.y);
        PlayerPrefs.SetFloat("PlayerPosZ", infoToRecord.position.z);

        PlayerPrefs.SetFloat("PlayerRotX", infoToRecord.rotation.x);
        PlayerPrefs.SetFloat("PlayerRotY", infoToRecord.rotation.y);
        PlayerPrefs.SetFloat("PlayerRotZ", infoToRecord.rotation.z);
        PlayerPrefs.SetFloat("PlayerRotW", infoToRecord.rotation.w);


        PlayerPrefs.SetInt("CurrentQuest", infoToRecord.currentQuest);

        PlayerPrefs.SetString("CurrentScene", infoToRecord.currentScene);
    }

    void LoadSavedInfo()
    {
        Vector3 savedPos = new Vector3(PlayerPrefs.GetFloat("PlayerPosX"), PlayerPrefs.GetFloat("PlayerPosY"), PlayerPrefs.GetFloat("PlayerPosZ"));
        Quaternion savedRot = new Quaternion(PlayerPrefs.GetFloat("PlayerRotX"), PlayerPrefs.GetFloat("PlayerRotY"), PlayerPrefs.GetFloat("PlayerRotZ"), PlayerPrefs.GetFloat("PlayerRotW"));

        transform.position = savedPos;
        myCamera.transform.position = savedPos;
        transform.rotation = savedRot;
        Npc.currentNPCForQuest = PlayerPrefs.GetInt("CurrentQuest");

        if (tutorial)
        {
            tutorial.enabled = false;
        }

        hasClickedContinueButton = false;
    }
}
