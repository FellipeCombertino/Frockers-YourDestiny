using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoToStoreToContinue
{
    public Vector3 position;
    public Quaternion rotation;
    public int currentQuest;
    public string currentScene;
   
    public InfoToStoreToContinue(Vector3 myPosition, Quaternion myRotation, int myQuest, string myScene)
    {
        position = myPosition;
        rotation = myRotation;
        currentQuest = myQuest;
        currentScene = myScene;
    }
}
