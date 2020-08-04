using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowNextQuestCaverna : MonoBehaviour
{

    private Camera mainCamera;
    public GameObject offscreenMarker;
    GameObject target;
    Vector3 updatePos;

    public GameObject henx, minerar;

    [Space]
    [Range(0, 100)]
    public float limiteBorda;

    Image myIconImage;
    GameObject player;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        myIconImage = offscreenMarker.GetComponent<Image>();
    }

    void Update()
    {
        UpdateNPCInfo();
        if (offscreenMarker)
        {
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
            case 13:
                target = henx;
                break;
            case 14:
                target = minerar;
                break;
            default:
                break;
        }
    }

}