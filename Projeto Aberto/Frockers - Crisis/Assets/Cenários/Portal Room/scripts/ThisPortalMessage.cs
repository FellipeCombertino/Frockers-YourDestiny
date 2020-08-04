using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisPortalMessage : MonoBehaviour
{

    // o nome específico do portal
    public string portalName;

    // variáveis para receber informações de outras classes
    PortalRoom portalManager;
    TeleportTo thisPortal;
    
    // todas as frases que este portal mostrará
    [TextArea(3, 5)]
    public string blockedMessage, freeMessage, accessGrantedMessage;

    // a frase que será mostrada de fato quando a animação da interface for iniciada
    string messageToDisplay;

    private void Start()
    {
        portalManager = GetComponentInParent<PortalRoom>();
        thisPortal = GetComponentInChildren<TeleportTo>();
        WhatPortalIsThis(); // defino em qual portal estou
        portalManager.DisplayGainedAccessMessage(portalName, thisPortal.placesToTeleport, accessGrantedMessage); // mostro a mensagem de acesso concedido SE necessário
    }

    // definir em qual portal estou e qual mensagem será mostrada conforme condições de acesso -> Start
    void WhatPortalIsThis()
    {
        switch (thisPortal.placesToTeleport)
        {
            case TeleportTo.Places.Condado:
                messageToDisplay = freeMessage; // sempre mostrará a mensagem de acesso livre
                break;
            case TeleportTo.Places.Ferreiro:
                if (TeleportTo.onFerreiro) // se eu já possuo acesso ao ferreiro
                    messageToDisplay = freeMessage; // mostro a mensagem de acesso livre
                else // senão
                    messageToDisplay = blockedMessage; // mostro a mensagem de acesso bloqueado
                break;
            case TeleportTo.Places.Labirinto:
                if (TeleportTo.onLabirinto)
                    messageToDisplay = freeMessage;
                else
                    messageToDisplay = blockedMessage;
                break;
            case TeleportTo.Places.Porto:
                if (TeleportTo.onPorto)
                    messageToDisplay = freeMessage;
                else
                    messageToDisplay = blockedMessage;
                break;
            case TeleportTo.Places.Floresta:
                if (TeleportTo.onFloresta)
                    messageToDisplay = freeMessage;
                else
                    messageToDisplay = blockedMessage;
                break;
            case TeleportTo.Places.Caverna:
                if (TeleportTo.onCaverna)
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
