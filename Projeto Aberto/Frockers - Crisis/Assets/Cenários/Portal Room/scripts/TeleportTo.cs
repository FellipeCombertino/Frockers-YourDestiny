using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TeleportTo : MonoBehaviour
{

    // enum para definir com qual portal o player está interagindo
    public enum Places { Condado, Ferreiro, Labirinto, Porto, Floresta, Caverna }
    public Places placesToTeleport;

    // variáveis para definir em qual cena do game o player está
    bool onForestScene, onCaveScene, onPortalRoom;
    
    // variáveis para conferir se o player já liberou acesso a certo portal
    public static bool onFerreiro, onLabirinto, onPorto, onFloresta, onCaverna;

    // variáveis para conferir se o player ACABOU de receber acesso a certo portal
    public static bool ferreiroJustOpened, labirintoJustOpened, portoJustOpened, florestaJustOpened, cavernaJustOpened;

    private void Update()
    {
        WhichSceneAmIAt();
    }
     
    // define em qual cena eu estou -> Update
    void WhichSceneAmIAt()
    {
        onForestScene = SceneManager.GetActiveScene() == SceneManager.GetSceneByName("Floresta");
        onCaveScene = SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CavernaDestruida");
        onPortalRoom = SceneManager.GetActiveScene() == SceneManager.GetSceneByName("TeleportRoom");
    }

    // para onde o portal irá me levar -> TriggerEnter
    void WhereAmIGoingTo()
    {
        switch (placesToTeleport)
        {
            case Places.Condado:                
                TeleportInfo.positionToSpawn = TeleportInfo.spawnCondado; // salvo a posição do spawn como posição do condado
                SceneManager.LoadScene("Floresta"); // irei para floresta
                break;
            case Places.Ferreiro:
                if (onFerreiro) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnFerreiro; // salvo a posição do spawn como posição do ferreiro
                    SceneManager.LoadScene("Floresta"); // irei para floresta
                }
                break;
            case Places.Labirinto:
                if (onLabirinto) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnLabirinto; // salvo a posição do spawn como posição do labirinto
                    SceneManager.LoadScene("Floresta"); // irei para floresta
                }
                break;
            case Places.Porto:
                if (onPorto) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnPorto; // salvo a posição do spawn como posição do porto
                    SceneManager.LoadScene("Floresta"); // irei para floresta
                }
                break;
            case Places.Floresta:
                if (onFloresta) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnFloresta; // salvo a posição do spawn como posição da floresta
                    SceneManager.LoadScene("Floresta"); // irei para floresta
                }
                break;
            case Places.Caverna:
                if (onCaverna) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnCaverna; // salvo a posição do spawn como posição da caverna
                }
                break;
            default:
                break;
        }
    }

    // ativa o portal após passar por ele -> TriggerEnter
    void ActivatePortal()
    {
        switch (placesToTeleport)
        {
            case Places.Condado:
                break;
            case Places.Ferreiro:
                if (!onFerreiro) // se ainda não ativei o portal
                { 
                    onFerreiro = true; // libero acesso ao portal
                    ferreiroJustOpened = true; // digo que acabei de liberar este portal -> Esta variável torna-se falsa em uma Coroutine na classe PortalRoom
                }
                break;
            case Places.Labirinto:
                if (!onLabirinto) // se ainda não ativei o portal
                {
                    onLabirinto = true;
                    labirintoJustOpened = true;
                }
                break;
            case Places.Porto:
                if (!onPorto) // se ainda não ativei o portal
                {
                    onPorto = true;
                    portoJustOpened = true;
                }
                break;
            case Places.Floresta:
                if (!onFloresta) // se ainda não ativei o portal
                {
                    onFloresta = true;
                    florestaJustOpened = true;
                }
                break;
            case Places.Caverna:
                if (!onCaverna) // se ainda não ativei o portal
                {
                    onCaverna = true;
                    cavernaJustOpened = true;
                }
                break;
            default:
                break;
        }
    }

    // ao colidir com o portal
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            if (onForestScene || onCaveScene) // se estiver na cena da caverna ou floresta 
            {
                ActivatePortal(); // desbloqueio a passagem do portal que acabei de entrar
                SceneManager.LoadScene("TeleportRoom"); // irei para a sala dos portais
            }
            else if (onPortalRoom) // se estiver na cena dos portais 
            {
                WhereAmIGoingTo(); // para onde me levará?
            }
        }
    }

}
