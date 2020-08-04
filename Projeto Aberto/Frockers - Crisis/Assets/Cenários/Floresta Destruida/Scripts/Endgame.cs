using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class Endgame : MonoBehaviour
{

    public PostProcessVolume myPPvolume;

    private void Update()
    {
        if (Npc.currentNPCForQuest == 21)
        {
            EndGame();
        }
    }
    
    void EndGame()
    {
        Vignette efeitoPreto;
        myPPvolume.profile.TryGetSettings(out efeitoPreto);

        efeitoPreto.intensity.value += 0.01f;

        if (efeitoPreto.intensity.value >= 2)
        {
            SceneManager.LoadScene("Endgame");
        }
    }

}
