using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif

public class Npc : MonoBehaviour
{

    public GameObject marker;

    public Dialogue npcInfo;
    public Dialogue.QuestSentences[] questSentenceInfo;

    public static int currentNPCForQuest;

    [HideInInspector]
    public List<string> whosSpeakingTheSentence = new List<string>();

    [HideInInspector]
    public List<string> questSentencesToDisplay = new List<string>();

    public static bool isSentenceQueued;

#if UNITY_EDITOR
    // inspector customizado para alterar variável conforme valor da booleana
    #region EditorPersonalizado
    [CustomEditor(typeof(Npc))]
    public class NpcEditor : Editor
    {
        override public void OnInspectorGUI()
        {
            var npcVar = target as Npc;
            base.DrawDefaultInspector(); // mostro as variáveis padrões do meu script
                        
            for (int i = 0; i < npcVar.questSentenceInfo.Length; i++)
            {               
                if (npcVar.questSentenceInfo[i].isResponse) // se a bool isResponse for marcada como true
                {
                    npcVar.questSentenceInfo[i].whosTalking = npcVar.npcInfo.namePlayer; // quem está falando é o player
                }
                else // se for false
                {
                    npcVar.questSentenceInfo[i].whosTalking = npcVar.npcInfo.nameNpc; // quem está falando é o NPC
                }
            }
        }
    }
    #endregion
#endif

    private void Update()
    {
        DisplayMarker();
    }

    // avançar para a próxima quest
    void AdvanceQuest()
    {
        if (questSentenceInfo.Length > 0)
        {
            for (int i = 0; i < questSentenceInfo.Length; i++)
            {
                if (questSentenceInfo[i].orderInQuest == currentNPCForQuest)
                {
                    if (!isSentenceQueued)
                    {
                        currentNPCForQuest++;
                        break;
                    }                    
                }
            }
        }
    }

    // mostrar o Marker em cima do NPC
    void DisplayMarker()
    {
        if (marker)
        {
            for (int i = 0; i < questSentenceInfo.Length; i++)
            {
                if (questSentenceInfo[i].orderInQuest == currentNPCForQuest)
                {
                    marker.SetActive(true);
                    return;
                }
                marker.SetActive(false);
            }
        }
    }

    // método para mostrar a próxima frase
    void changeDialogue()
    {
        FindObjectOfType<DialogueManager>().DisplayNextSentence();
    }

    // ao entrar em contato com o NPC
    private void OnTriggerEnter(Collider other)
    {        
        if (other.gameObject.CompareTag("Player"))
        {
            whosSpeakingTheSentence.Clear(); // limpo minha lista de nomes 
            questSentencesToDisplay.Clear(); // limpo minha lista de frases ao entrar em contato com um NPC

            if (questSentenceInfo.Length > 0) // se o npc possui falas de quest
            {
                for (int i = 0; i < questSentenceInfo.Length; i++)
                {                    
                    if (questSentenceInfo[i].orderInQuest == currentNPCForQuest) // vai procurar todas as falas da quest atual
                    {
                        whosSpeakingTheSentence.Add(questSentenceInfo[i].whosTalking); // vai adicionar o nome de quem está falando a frase em uma fila 
                        questSentencesToDisplay.Add(questSentenceInfo[i].sentence); // vai adicionar a frase em outra fila
                    }                   
                }                
            }
            else // apenas para evitar erro caso esqueça de criar as frases de um npc
            {
                Debug.Log(npcInfo.standardSentence);
            }

            FindObjectOfType<DialogueManager>().DisplayDialogue(this); // começa o diálogo
        }
    }

    // enquanto estiver em contato com o NPC
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E)) // se apertar E enquanto está conversando com o NPC
            {
                changeDialogue(); // muda o diálogo
                AdvanceQuest();
            }
        }
    }

    // quando sair de contato com o NPC
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AdvanceQuest();
            FindObjectOfType<DialogueManager>().EndDialogue();
        }
    }    

}
