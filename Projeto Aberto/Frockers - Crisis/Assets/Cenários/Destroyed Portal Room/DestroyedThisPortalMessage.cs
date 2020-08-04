using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedThisPortalMessage : MonoBehaviour
{

    // o nome específico do portal
    public string portalName;

    // variáveis para receber informações de outras classes
    DestroyedPortalRoom portalManager;
    TeleportDestroyed thisPortal;
    
    // todas as frases que este portal mostrará
    [TextArea(3, 5)]
    public string blockedMessage, freeMessage, accessGrantedMessage;

    // a frase que será mostrada de fato quando a animação da interface for iniciada
    string messageToDisplay;

    private void Start()
    {
        portalManager = GetComponentInParent<DestroyedPortalRoom>();
        thisPortal = GetComponentInChildren<TeleportDestroyed>();
        WhatPortalIsThis(); // defino em qual portal estou
        portalManager.DisplayGainedAccessMessage(portalName, thisPortal.placesToTeleport, accessGrantedMessage); // mostro a mensagem de acesso concedido SE necessário
    }

    // definir em qual portal estou e qual mensagem será mostrada conforme condições de acesso -> Start
    void WhatPortalIsThis()
    {
        switch (thisPortal.placesToTeleport)
        {
            case TeleportDestroyed.Places.Neutro:
                messageToDisplay = freeMessage; // sempre mostrará a mensagem de acesso livre
                break;
            case TeleportDestroyed.Places.Ferreiro:
                if (TeleportTo.onFerreiro) // se eu já possuo acesso ao ferreiro
                    messageToDisplay = freeMessage; // mostro a mensagem de acesso livre
                else // senão
                    messageToDisplay = blockedMessage; // mostro a mensagem de acesso bloqueado
                break;
            case TeleportDestroyed.Places.Porto:
                if (TeleportTo.onPorto)
                    messageToDisplay = freeMessage;
                else
                    messageToDisplay = blockedMessage;
                break;
            default:
                break;
        }       
    }

    // ao entrar em contato com o Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // se for o player
        {
            portalManager.DisplayPortalMessage(portalName, messageToDisplay); // chamo o método da classe PortalRoom para mostrar a mensagem de acesso livre ou bloqueado
        }
    }

    // enquanto estiver em contato com o Trigger
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // caso seja o player
        {
            if (Input.GetKeyDown(KeyCode.E)) // se apertar E enquanto está visualizando a mensagem
            {
                portalManager.EndPortalMessage(); // encerra a mensagem
            }
        }
    }

    // ao sair de contato com o Trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) // caso seja o player
        {
            portalManager.EndPortalMessage(); // chama o método da classe PortalRoom para fechar a interface
        }
    }
}
