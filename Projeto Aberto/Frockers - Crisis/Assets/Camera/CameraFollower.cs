using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{

    public float cameraSpeed = 120.0f; // velocidade da Câmera
    public float clampAngle = 40.0f; // limitar angulação que a câmera rotaciona
    public float inputSensitivity = 150.0f; // sensibilidade da câmera
    public float mouseX, mouseY;
    private float rotX = 0;
    private float rotY = 0;

    public GameObject target; // o que a câmera vai seguir > cameraTarget inside player
    public Transform player; // o objeto player que carrega tudo

    Quaternion originalRotation;
    
    // Start is called before the first frame update
    void Start()
    {

        Vector3 rot = transform.localRotation.eulerAngles; // recebendo a rotação em euler angles da câmera
        rotY = rot.y; // atribuindo os valores para as variáveis que temos criadas
        rotX = rot.x; // ^ 
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        originalRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        
    }

    // Update is called once per frame
    void Update()
    {

        RotateCameraAroundPlayer();

    }

    private void LateUpdate()
    {

        CameraUpdater();

    }

    void CameraUpdater()
    {

        Transform myTarget = target.transform;

        // move-se para o alvo
        float step = cameraSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, myTarget.position, step);


    }

    void RotateCameraAroundPlayer()
    {

        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        
        rotX += mouseY * inputSensitivity * Time.deltaTime;
        rotY += mouseX * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rotationLocal = Quaternion.Euler(rotX, rotY, 0);

        if (Input.GetKey (KeyCode.LeftAlt) && FindObjectOfType<BlakeController>().blakeCurrentState == BlakeController.BlakeState.Idle)
            transform.rotation = rotationLocal;
        else
        {
            transform.rotation = rotationLocal;
            player.rotation = Quaternion.Euler(0, rotY, 0);
        }

    }

}
