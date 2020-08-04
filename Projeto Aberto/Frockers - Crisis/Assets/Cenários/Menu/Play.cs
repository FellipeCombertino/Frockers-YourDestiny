using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.PostProcessing;

public class Play : MonoBehaviour
{

    public Animator theAnima, folha1, folha2, folha3;
    bool startCount;
    float timer;
    public PostProcessVolume myVolume;
    public GameObject nomes, blackScreen, botao1, botao2, botao3;

    bool isStartingNewGame;

    public void DisplayAnimation(bool newGame)        
    {
        theAnima.SetBool("openBook", true);
        startCount = true;
        nomes.SetActive(false);
        botao1.SetActive(false);
        botao2.SetActive(false);
        botao3.SetActive(false);
        Cursor.visible = false;
        isStartingNewGame = newGame;
    }

    private void Update()
    {
        
        if (!PlayerPrefs.HasKey("CurrentScene"))
        {
            botao1.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            PlayerPrefs.DeleteAll();
            Npc.currentNPCForQuest = 0;
        }

        if (startCount)
        {
            timer += Time.deltaTime;

            Vignette efeitoPreto;
            myVolume.profile.TryGetSettings(out efeitoPreto);


            if (timer > 3)
            {
                efeitoPreto.intensity.value += 0.008f;
            }

            if (timer > 3 && timer < 4)
            {
                folha1.Play("Turn");
            }

            if (timer > 4 && timer < 5)
            {
                folha2.Play("Turn");
            }

            if (timer > 5 && timer < 6)
            {
                folha3.Play("Turn");
            }

            if (timer > 6.2f)
            {
                blackScreen.SetActive(true);
            }

            if (timer > 6.5f)
            {
                LoadNextScene();
            }

        }
    }

    void LoadNextScene()
    {
        if (isStartingNewGame)
        {
            SceneManager.LoadScene("Floresta");
        }
        else
        {
            ContinueGame.hasClickedContinueButton = true;
            SceneManager.LoadScene(PlayerPrefs.GetString("CurrentScene"));
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
