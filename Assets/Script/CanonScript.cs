using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonScript : MonoBehaviour
{
    public GameObject horizontal;
    public GameObject vertical;
    public float mouseSensitivity = 10;
    private float yaw;
    private float pitch;

    public float rotationSmoothTime = .1f;
    Vector3 rotationSmoothVelocity;
    Vector3 rotationSmoothVelocityVert;
    Vector3 currentRotation;
    Vector3 currentRotationVert;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posFinal = Vector3.zero;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;


        pitch = Mathf.Clamp(pitch, -55, 10);

        yaw = Mathf.Clamp(yaw, -50, 50);

        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yaw), ref rotationSmoothVelocity, rotationSmoothTime);
        horizontal.transform.eulerAngles = currentRotation;


        currentRotationVert = Vector3.SmoothDamp(currentRotationVert, new Vector3(pitch, yaw), ref rotationSmoothVelocityVert, rotationSmoothTime);
        vertical.transform.eulerAngles = currentRotationVert;
    }
}
