using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{   
    // o nome do NPC
    public string nameNpc;

    // o nome do Player
    [HideInInspector]
    public string namePlayer = "Blake";

    // frase padrão que o personagem fala 
    [TextArea(3, 5)]
    public string standardSentence;

    [System.Serializable]
    public struct QuestSentences
    {
        // a ordem dessa frase na quest
        public int orderInQuest;

        // a frase é uma resposta do player ou é própria do npc?
        public bool isResponse;

        // quem está falando a frase?
        public string whosTalking;

        // a frase em si
        [TextArea(3, 5)]
        public string sentence;      
    }

}

