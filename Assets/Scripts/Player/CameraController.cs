using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0.5f, 3f)] public float mouseSensitivity = 1.5f;
    public float Xmax = 30.0f;
    public float Xmin = -30.0f;
    public float speed = 100f;
    public float maxAngle = 20f;

    float xAxisClamp = 0.0f;

    Vector3 camCenter;

    GameObject Player;

    void Awake()
    {
        camCenter = transform.localPosition;

        Player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        Vector3 left = -transform.right;
        Vector3 right = transform.right;

        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");;

        float rotAmountX = MouseX * mouseSensitivity;
        float rotAmountY = MouseY * mouseSensitivity;

        xAxisClamp -= rotAmountY;

        Vector3 targetRotCam = transform.rotation.eulerAngles;
        Vector3 targetRotBody = Player.transform.rotation.eulerAngles;

        targetRotCam.x -= rotAmountY;
        targetRotCam.z = 0;
        targetRotBody.y += rotAmountX;

        if(xAxisClamp > Xmax)
        {
            xAxisClamp = Xmax;
            targetRotCam.x = Xmax;
        }
        else if (xAxisClamp < Xmin)
        {
            xAxisClamp = Xmin;
            targetRotCam.x = Xmin;
        }

        transform.rotation = Quaternion.Euler(targetRotCam);
        Player.transform.rotation = Quaternion.Euler(targetRotBody);
    }
}