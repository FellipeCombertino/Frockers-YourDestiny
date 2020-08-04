using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    // objetos da UI p/ escrever o nome e o texto
    public Text npcNameText, npcDialogueText;

    // identificar o animator da caixa de texto
    public Animator dialogueUIAnimator;

    // filas para armazenas os nomes e frases
    private Queue <string> sentences;
    private Queue<string> names;

    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
    }
           
    #region SistemaAtualizado
    // começar o diálogo com o NPC
    public void DisplayDialogue(Npc npc)
    {
        dialogueUIAnimator.SetBool("isOpen", true); // ativa animação da caixa de diálogo abrindo
        names.Clear(); // limpa qualquer nome que estiver na fila
        sentences.Clear(); // limpa qualquer frase que estiver na fila
        
        foreach (string name in npc.whosSpeakingTheSentence) // para cada nome na lista 
        {
            names.Enqueue(name); // o adiciona à fila
        }
        
        foreach (string sentence in npc.questSentencesToDisplay) // para cada frase na fila
        {
            sentences.Enqueue(sentence); // a adiciona na fila
        }

        if (sentences.Count > 0) // se houver alguma frase na fila
        {
            DisplayNextSentence(); // mostra a primeira frase
        }
        else // se não há frase, é porque o NPC não apresenta nenhuma quest
        {
            StopAllCoroutines(); // portanto paramos a coroutines para evitar bugs
            StartCoroutine(TypeSentence(npc.npcInfo.nameNpc, npc.npcInfo.standardSentence)); // e mostramos a frase padrão
        }
    }

    // mudar para a próxima fila
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // se não houver nenhuma frase na fila
        {
            Npc.isSentenceQueued = false;
            EndDialogue(); // termina o diálogo
            return; // impede o método de prosseguir
        }
        else
        {
            Npc.isSentenceQueued = true;
        }

        string name = names.Dequeue(); // o nome mais antigo é removido da fila
        string sentence = sentences.Dequeue(); // a frase mais antiga da fila é removida        

        StopAllCoroutines(); // para todas as coroutines antes de começar uma nova (caso o player mude de frase, as letras não continuarão aparecendo da frase anterior)
        StartCoroutine(TypeSentence(name, sentence)); // começa a coroutine com a frase que foi removida da fila (a mais antiga)
    }

    // Coroutine para mostrar as letras uma por uma na tela
    IEnumerator TypeSentence (string name, string sentence)
    {
        npcNameText.text = "";
        foreach (char letter in name.ToCharArray())
        {
            npcNameText.text += letter;
            yield return null;
        }

        npcDialogueText.text = ""; // a caixa de texto sempre terá um valor, mesmo que vazio
        foreach (char letter in sentence.ToCharArray()) // para cada caráter na frase sendo analisada (convertida em caráteres separados)
        {
            npcDialogueText.text += letter; // a caixa de texto receberá um caráter
            yield return null; // a cada frame
        }
    } 

    // finalizar o diálogo
    public void EndDialogue()
    {
        dialogueUIAnimator.SetBool("isOpen", false); // ativa animação da caixa de diálogo fechando
    }   
    #endregion
}
