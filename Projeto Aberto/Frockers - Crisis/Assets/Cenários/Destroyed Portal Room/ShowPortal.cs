using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPortal : MonoBehaviour
{

    private Camera mainCamera;
    public GameObject offscreenMarker;
    GameObject target;
    Vector3 updatePos;

    public GameObject portalFerreiro, portalPorto;

    [Space]
    [Range(0, 100)]
    public float limiteBorda;

    public Image myIconImage;
    GameObject player;

    void Start()
    {
        mainCamera = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Npc.currentNPCForQuest != 16 && Npc.currentNPCForQuest != 19)
        {
            offscreenMarker.SetActive(false);
        }
        else
        {
            offscreenMarker.SetActive(true);
            UpdateNPCInfo();
            ChangeIconPosition();
            DisableIconWhenCloseToNpc();
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
                target = portalFerreiro;
                break;
            case 19:
                target = portalPorto;
                break;
            default:
                break;
        }
    }

}