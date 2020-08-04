using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoToStoreOnRewind
{
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 velocity;

    public InfoToStoreOnRewind (Vector3 myPosition, Quaternion myRotation, Vector3 myVelocity)
    {
        position = myPosition;
        rotation = myRotation;
        velocity = myVelocity;
    }
}
