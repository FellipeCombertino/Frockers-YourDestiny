using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class TeleportDestroyed : MonoBehaviour
{

    // enum para definir com qual portal o player está interagindo
    public enum Places { Ferreiro, Porto, Neutro }
    public Places placesToTeleport;

    // para onde o portal irá me levar -> TriggerEnter
    void WhereAmIGoingTo()
    {
        switch (placesToTeleport)
        {
            case Places.Ferreiro:
                if (TeleportTo.onFerreiro) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnFerreiro; // salvo a posição do spawn como posição do ferreiro
                    SceneManager.LoadScene("FlorestaDestruida"); // irei para floresta
                }
                break;
            case Places.Porto:
                if (TeleportTo.onPorto) // se já tenho o portal desbloqueado
                {
                    TeleportInfo.positionToSpawn = TeleportInfo.spawnPorto; // salvo a posição do spawn como posição do porto
                    SceneManager.LoadScene("FlorestaDestruida"); // irei para floresta
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
            WhereAmIGoingTo();
        }
    }

}
