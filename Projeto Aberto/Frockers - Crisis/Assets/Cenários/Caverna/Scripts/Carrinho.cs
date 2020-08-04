using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Carrinho : MonoBehaviour
{

    public PathCreator trilho;
    float distanceTravelled;
    float speed;

    public static bool insideWagon;
    public GameObject playerPrefab, player, blakeMesh, fakeMesh;
    public BlakeController desativarBlakeController;
    public Animator fakeBlakeAnimator;

    public GameObject tutorial;

    Vector3 savePlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }
    
    void FollowPath()
    {
        distanceTravelled += Time.deltaTime * speed;
        transform.position = trilho.path.GetPointAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
        transform.rotation = trilho.path.GetRotationAtDistance(distanceTravelled, EndOfPathInstruction.Stop);
    }

    void BlakeInsideWagon()
    {
        speed = 10;
        savePlayerPos = player.transform.position;
        player.transform.position = transform.position;
        playerPrefab.transform.parent = transform;
        blakeMesh.SetActive(false);
        fakeBlakeAnimator.SetBool("sit", true);
        fakeMesh.SetActive(true);
        desativarBlakeController.enabled = false;
    }

    void BlakeOutsideWagon()
    {
        StopAllCoroutines();
        speed = 0;
        fakeBlakeAnimator.SetBool("sit", false);
        StartCoroutine(DisplayBlakeFromFakeBlake());
    }

    IEnumerator DisplayBlakeFromFakeBlake()
    {
        yield return new WaitForSeconds(2);
        playerPrefab.transform.parent = null;
        blakeMesh.SetActive(true);
        fakeMesh.SetActive(false);
        desativarBlakeController.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Npc.currentNPCForQuest > 13)
        {
            if (other.CompareTag("Player"))
            {
                tutorial.SetActive(true);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Npc.currentNPCForQuest > 13)
        {
            if (other.CompareTag("Player"))
            {
                if (Input.GetKeyDown(KeyCode.F))
                {
                    insideWagon = !insideWagon;

                    if (insideWagon)
                    {
                        BlakeInsideWagon();
                    }
                    else
                    {
                        BlakeOutsideWagon();
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Npc.currentNPCForQuest > 13)
        {
            if (other.CompareTag("Player"))
            {
                tutorial.SetActive(false);
            }
        }
    }
    
}
