using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLightDisplay : MonoBehaviour
{
    Light pointLightRendering;
    Transform player;
    float distance;

    private void Start()
    {
        pointLightRendering = GetComponent<Light>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        distance = Vector3.Distance(transform.position, player.position);
        if (distance < 250)
        {
            pointLightRendering.enabled = true;
        }
        else
        {
            pointLightRendering.enabled = false;
        }
    }
}
