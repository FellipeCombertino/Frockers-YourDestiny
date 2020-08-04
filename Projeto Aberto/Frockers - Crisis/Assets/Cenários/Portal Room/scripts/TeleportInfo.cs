using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportInfo : MonoBehaviour
{
    
    // defino quais objetos serão teleportados em minha cena
    public GameObject blake, cam;

    // defino as posições para onde meus objetos serão teleportados -> Elas são atualizadas em um método na classe TeleportTo
    public static Vector3 spawnPorto = new Vector3(-180, 23, 122);
    public static Vector3 spawnCondado = new Vector3(220, 64, 320);
    public static Vector3 spawnFerreiro = new Vector3(905, 26, 180);
    public static Vector3 spawnFloresta = new Vector3(-55, 21, -285);
    public static Vector3 spawnLabirinto = new Vector3(789, 35, -515);
    public static Vector3 spawnCaverna = new Vector3(789, 35, -515);

    // variável que recebe o valor de onde o(s) objeto(s) será(ão) spawnado(s)
    public static Vector3 positionToSpawn;

    // teleporta o player para a posição do parâmetro recebido (posição em que será teleportado) -> Start
    public void TeleportPlayer(Vector3 spawnPosition)
    {
        if (blake)
            blake.transform.position = spawnPosition;
        if (cam)
            cam.transform.position = spawnPosition;
    }

    private void Start()
    {
        if (positionToSpawn != Vector3.zero) // se houver uma posição nova para teleportar
        {
            TeleportPlayer(positionToSpawn); // teletransporto o player
        }        
    }
}
