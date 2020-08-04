using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CavernaDestruidaInicio : MonoBehaviour
{

    public GameObject blakeMesh, fakeBlakeMesh;
    public BlakeController myBlakeController;
    public PostProcessVolume myPpProfile;
    public TeleportCavernaDestruida teleport;
    public GameObject louiNpc;
    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        blakeMesh.SetActive(false);
        fakeBlakeMesh.SetActive(true);
        myBlakeController.enabled = false;
        StartCoroutine(ChangeMeshToOriginal());
        EfeitoPretoInicial();
        teleport.enabled = false;
    }       

    // Update is called once per frame
    void Update()
    {
        EfeitoPretoAoContrario();
        EnablePortal();
        DisableLoui();
        EnableMarker();
    }
    
    void EnableMarker()
    {
        if (Npc.currentNPCForQuest == 16)
        {
            marker.SetActive(true);
        }
        else
        {
            marker.SetActive(false);
        }
    }

    void DisableLoui()
    {
        if (Npc.currentNPCForQuest == 15)
        {
            louiNpc.SetActive(true);
        }
        else
        {
            louiNpc.SetActive(false);
        }
    }

    void EnablePortal()
    {
        if (Npc.currentNPCForQuest > 15)
        {
            teleport.enabled = true;
        }
    }

    void EfeitoPretoAoContrario()
    {
        Vignette efeitoPreto;
        myPpProfile.profile.TryGetSettings(out efeitoPreto);
        if (efeitoPreto != null)
        {
            if (efeitoPreto.intensity.value > 0.462f)
            {
                efeitoPreto.intensity.value -= 0.005f;
            }
        }
    }

    IEnumerator ChangeMeshToOriginal()
    {
        yield return new WaitForSeconds(8);
        blakeMesh.SetActive(true);
        fakeBlakeMesh.SetActive(false);
        myBlakeController.enabled = true;
    }

    void EfeitoPretoInicial()
    {
        Vignette efeitoPreto;
        myPpProfile.profile.TryGetSettings(out efeitoPreto);
        if (efeitoPreto != null)
        {
            efeitoPreto.intensity.value = 1f;
        }
    }

}
