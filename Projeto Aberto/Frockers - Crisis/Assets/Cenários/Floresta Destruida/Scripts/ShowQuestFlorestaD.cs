using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuestFlorestaD : MonoBehaviour
{

    private Camera mainCamera;
    public GameObject offscreenMarker;
    GameObject target;
    Vector3 updatePos;

    public GameObject crixie, box, portal, loui;

    [Space]
    [Range(0, 100)]
    public float limiteBorda;

    public Image myIconImage;
    GameObject player;

    public GameObject cachoeira;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        UpdateNPCInfo();
        if (target)
        {
            offscreenMarker.SetActive(true);
            ChangeIconPosition();
            DisableIconWhenCloseToNpc();
        }
        else
        {
            offscreenMarker.SetActive(false);
        }
        DisableCachoeiraFlickering();
    }
    
    void DisableCachoeiraFlickering()
    {
        float dist = Vector3.Distance(player.transform.position, cachoeira.transform.position);
        if (dist < 1000)
        {
            cachoeira.SetActive(true);
        }
        else
        {
            cachoeira.SetActive(false);
        }
    }

    void DisableIconWhenCloseToNpc()
    {
        float dist = Vector3.Distance(target.transform.position, player.transform.position);
        if (dist < 15)
        {
            myIconImage.enabled = false;
        }
        else
        {
            myIconImage.enabled = true;
        }
    }

    void ChangeIconPosition()
    {
        updatePos = target.transform.position;

        updatePos = mainCamera.WorldToViewportPoint(updatePos);

        if (updatePos.z < 0)
        {
            updatePos.x = 1f - updatePos.x;
            updatePos.y = 1f - updatePos.y;
            updatePos.z = 0.1f;
            updatePos = MaximizeVector3(updatePos);
        }
        updatePos = mainCamera.ViewportToScreenPoint(updatePos);
        updatePos.x = Mathf.Clamp(updatePos.x, limiteBorda, Screen.width - limiteBorda);
        updatePos.y = Mathf.Clamp(updatePos.y, limiteBorda, Screen.height - limiteBorda);
        offscreenMarker.transform.position = updatePos;
    }

    public Vector3 MaximizeVector3(Vector3 vetor)
    {
        Vector3 returnVector = vetor;
        float max = 0;
        max = vetor.x > max ? vetor.x : max;
        max = vetor.y > max ? vetor.y : max;
        max = vetor.z > max ? vetor.z : max;
        returnVector /= max;
        return returnVector;
    }

    void UpdateNPCInfo()
    {
        switch (Npc.currentNPCForQuest)
        {
            case 16:
                target = crixie;
                break;
            case 17:
                target = box;
                break;
            case 18:
                target = crixie;
                break;
            case 19:
                target = portal;
                break;
            case 20:
                target = loui;
                break;
            default:
                break;           
        }
    }

}