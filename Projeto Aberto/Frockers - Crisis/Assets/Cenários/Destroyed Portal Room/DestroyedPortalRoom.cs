using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyedPortalRoom : MonoBehaviour
{

    // variáveis para definir os objetos da interface
    public Animator messageBox;
    public Text portalNameText, portalMessage;
    
    // mostrar a mensagem ao colidir com o portal -> Será chamado na classe ThisPortalMessage
    public void DisplayPortalMessage(string namePortal, string portalSentence)
    {
        messageBox.SetBool("isOpen", true); // ativo a animação p/ subir o portal
        StopAllCoroutines(); // cancelo todas coroutines para evitar bugs visuais
        StartCoroutine(TypeSentence(namePortal, portalSentence)); // inicio uma coroutine que recebe o nome do portal e a frase a ser mostrada
    }

    // esconder a interface caso a mensagem acabe
    public void EndPortalMessage()
    {
        messageBox.SetBool("isOpen", false);
    }

    // Coroutine para mostrar as letras uma por uma na tela
    IEnumerator TypeSentence(string namePortal, string portalSentence)
    {
        portalNameText.text = "";
        foreach (char letter in namePortal.ToCharArray())
        {
            portalNameText.text += letter;
            yield return null;
        }

        portalMessage.text = ""; // a caixa de texto sempre terá um valor, mesmo que vazio
        foreach (char letter in portalSentence.ToCharArray()) // para cada caráter na frase sendo analisada (convertida em caráteres separados)
        {
            portalMessage.text += letter; // a caixa de texto receberá um caráter
            yield return null; // a cada frame
        }
    }

    // método para mostrar a mensagem que acabou de receber acesso a um portal -> Será chamado no Start da classe ThisPortalMessage
    public void DisplayGainedAccessMessage(string namePortal, TeleportDestroyed.Places whatPortalIsThis, string accessMessage)
    {
        bool justOpenedPortal = false; // define como falsa uma variável para saber se acabou de receber acesso 

        // identificar qual portal é esse p/ saber se já recebeu acesso a ele ou não
        switch (whatPortalIsThis)
        {  
            case TeleportDestroyed.Places.Ferreiro:
                justOpenedPortal = TeleportTo.ferreiroJustOpened; // define a variável com o valor estático de cada portal
                break;
            case TeleportDestroyed.Places.Porto:
                justOpenedPortal = TeleportTo.portoJustOpened;
                break;
            case TeleportDestroyed.Places.Neutro:
                break;
            default:
                break;
        }
        
        // CASO tenha ACABADO de receber acesso a um portal
        if (justOpenedPortal)
        {
            messageBox.SetBool("isOpen", true); // mostra a interface
            StopAllCoroutines(); // para todas as coroutines para evitar bug
            StartCoroutine(TypeSentence(namePortal, accessMessage)); // escreve na interface o nome do portal e a frase de acesso
            StartCoroutine(EndGrantedMessage(whatPortalIsThis)); // inicia a coroutine para fechar a interface após 3 segundos
        }
    }

    // Coroutine para fechar a mensagem de acesso garantido ao portal após 3 segundos
    IEnumerator EndGrantedMessage(TeleportDestroyed.Places whatPortalIsThis)
    {

        // confere em qual portal está
        switch (whatPortalIsThis)
        {
            case TeleportDestroyed.Places.Neutro:
                break;
            case TeleportDestroyed.Places.Ferreiro:
                TeleportTo.ferreiroJustOpened = false; // define a variável estática que ACABOU de receber acesso como falsa, afinal a pessoa recebe acesso permanente apenas uma vez
                break;
            case TeleportDestroyed.Places.Porto:
                TeleportTo.portoJustOpened = false;
                break;

            default:
                break;
        }

        // espera 3 segundos para realizar o switch acima
        yield return new WaitForSeconds(3);

        // fecha a interface
        messageBox.SetBool("isOpen", false);

        // espera 3 segundos para fechar
        yield return new WaitForSeconds(3);
    }

}