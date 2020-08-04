using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeepSadSoundTrack : MonoBehaviour
{
    bool onSceneToDisplaySong()
    {
        return SceneManager.GetActiveScene() == SceneManager.GetSceneByName("CavernaDestruida") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("FlorestaDestruida") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("PortalRoomDestroyed");
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (!onSceneToDisplaySong())
        {
            Destroy(gameObject);
        }
    }
}
