using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeOneByOne : MonoBehaviour
{

    float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 13f)
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
