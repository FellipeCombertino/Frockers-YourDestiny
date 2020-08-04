using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Porta : MonoBehaviour
{

    Livro meuLivro;
    public string irParaCena;

    // Start is called before the first frame update
    void Start()
    {
        meuLivro = FindObjectOfType<Livro>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (meuLivro.doorOpened)
                {
                    SceneManager.LoadScene(irParaCena);
                }
            }
        }
    }

}
