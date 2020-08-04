using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TeleportCavernaDestruida : MonoBehaviour
{

    // enum para definir com qual portal o player está interagindo
    public enum Places { Caverna }
    public Places placesToTeleport;

    // variáveis para definir em qual cena do game o player está
    bool onCaveScene;
    
    private void Update()
    {
        WhichSceneAmIAt();
    }
     
    // define em qual cena eu estou -> Update
    void WhichSceneAmIAt()
    {
        onCaveScene = SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CavernaDestruida");
    }
       
    // ativa o portal após passar por ele -> TriggerEnter
    void ActivatePortal()
    {
        switch (placesToTeleport)
        {
            case Places.Caverna:
                if (!TeleportTo.onCaverna) // se ainda não ativei o portal
                {
                    TeleportTo.onCaverna = true;
                    TeleportTo.cavernaJustOpened = true;
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
            if (onCaveScene) // se estiver na cena da caverna ou floresta 
            {
                ActivatePortal(); // desbloqueio a passagem do portal que acabei de entrar
                SceneManager.LoadScene("PortalRoomDestroyed"); // irei para a sala dos portais
            }
        }
    }

}
