using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathOnWater : MonoBehaviour
{
    bool isRewinding;
    List<InfoToStoreOnRewind> infoToRecord;

    Rigidbody blakeRB;

    public GameObject painel;
    public AudioSource fonteAudio;
    public AudioClip somAgua;

    private void Start()
    {
        infoToRecord = new List<InfoToStoreOnRewind>();
        blakeRB = GetComponent<Rigidbody>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == 4) // layer da água
        {
            fonteAudio.PlayOneShot(somAgua);
            StartRewinding();
            painel.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        }
        else
        {
            Record();
        }
    }

    void Rewind()
    {
        if (infoToRecord.Count > 0)
        {
            InfoToStoreOnRewind pointInTime = infoToRecord[0];
            transform.position = pointInTime.position;
            transform.rotation = pointInTime.rotation;
            blakeRB.velocity = pointInTime.velocity;
            infoToRecord.RemoveAt(0);
        }
        else
        {
            StopRewind();
        }
    }
    
    void Record()
    {
        if (infoToRecord.Count > Mathf.Round(3.5f / Time.fixedDeltaTime))
        {
            infoToRecord.RemoveAt(infoToRecord.Count - 1);
        }
        infoToRecord.Insert(0, new InfoToStoreOnRewind(transform.position, transform.rotation, blakeRB.velocity));
    }

    void StartRewinding()
    {
        isRewinding = true;
        blakeRB.isKinematic = true;
    }

    void StopRewind()
    {
        isRewinding = false;
        blakeRB.isKinematic = false;
        painel.SetActive(false);
    }
    
}
