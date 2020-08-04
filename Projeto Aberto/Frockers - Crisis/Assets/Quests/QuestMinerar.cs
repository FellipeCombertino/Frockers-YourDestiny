using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;

public class QuestMinerar : MonoBehaviour
{
    public int questNumber;
    public int howManyTimesToBeatBeforeExplosion;
    public GameObject marker, tutoMouse, offScreenIndicator;

    AudioSource audioHit;
    public AudioClip picaretaHit;

    public BlakeController getBlakeState;

    public AudioSource somTrilha, fonteExplosao;
    public AudioClip somExplosao;

    bool hasPlayedExplosionSound;

    public PostProcessVolume postProcessingComponent;

    public GameObject blackScreen;

    public string qualCenaIreiQuandoDestruir;

    private void Start()
    {
        audioHit = GetComponent<AudioSource>();
    }
    private void Update()
    {
        DisplayMarker();
        Explosion();
    }

    void Explosion()
    {
        if (!hasPlayedExplosionSound)
        {
            if (howManyTimesToBeatBeforeExplosion <= 0)
            {
                Npc.currentNPCForQuest = 15;
                somTrilha.Stop();
                fonteExplosao.PlayOneShot(somExplosao);
                hasPlayedExplosionSound = true;
            }
        }

        if (howManyTimesToBeatBeforeExplosion <= 0)
        {
            StartCoroutine(ApagarTela());
        }
    }

    IEnumerator ApagarTela()
    {
        yield return new WaitForSeconds(3);
        Vignette efeitoPreto;        
        postProcessingComponent.profile.TryGetSettings(out efeitoPreto);        
        if (efeitoPreto != null)
        {
            efeitoPreto.intensity.value += 0.005f;
        }
        getBlakeState.enabled = false;
        Destroy(offScreenIndicator);

        yield return new WaitForSeconds(4);
        blackScreen.SetActive(true);

        yield return new WaitForSeconds(6);
        SceneManager.LoadScene(qualCenaIreiQuandoDestruir);
    }

    void DisplayMarker()
    {
        if (marker || tutoMouse)
        {
            if (Npc.currentNPCForQuest == questNumber)
            {
                marker.SetActive(true);
                tutoMouse.SetActive(true);
                return;
            }
            marker.SetActive(false);
            tutoMouse.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (getBlakeState.blakeCurrentState == BlakeController.BlakeState.AttackingWeapon)
        {
            if (other.CompareTag("PicaretaPlayer"))
            {
                if (questNumber == Npc.currentNPCForQuest)
                {
                    audioHit.PlayOneShot(picaretaHit);
                    howManyTimesToBeatBeforeExplosion--;
                }
            }
        }
    }
}
