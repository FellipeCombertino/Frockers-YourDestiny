using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{

    public float minDistance = 4.5f;
    public float maxDistance = 8.5f;
    public float smooth = 5.0f;
    public float theDistance;

    Vector3 dollyDir;

    // Start is called before the first frame update
    void Awake()
    {

        dollyDir = transform.localPosition.normalized;
        theDistance = transform.localPosition.magnitude;

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDistance);
        RaycastHit hit;

        if (Physics.Linecast (transform.parent.position, desiredCameraPos, out hit))
        {

            theDistance = Mathf.Clamp(hit.distance * 0.4f, minDistance, maxDistance);

        }
        else
        {

            theDistance = maxDistance;

        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * theDistance, Time.deltaTime * smooth);

    }

}
