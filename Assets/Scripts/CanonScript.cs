using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActiveCanon
{
    Active,
    Disable
}

public class CanonScript : MonoBehaviour
{
    public GameObject horizontal;
    public GameObject vertical;
    public float mouseSensitivity = 10;
    public Vector3 pitchMin = new Vector3(0, -55, 10);
    public Vector3 yawMin = new Vector3(0,-55,50);
    private float yaw;
    private float pitch;

    public float rotationSmoothTime = .1f;
    public GameObject CannonPrefab;
    public GameObject puntoDisparo;

    public float cannonSpeed;

    Vector3 rotationSmoothVelocity;
    Vector3 rotationSmoothVelocityVert;
    Vector3 currentRotation;
    Vector3 currentRotationVert;

    private Vector3 startRotation;

    public ActiveCanon status;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == ActiveCanon.Active)
        {
            Movement();
            InputPress();
        }
    }
    private void Movement()
    {
        Vector3 posFinal = Vector3.zero;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;


        pitch = Mathf.Clamp(pitch, pitchMin.y, pitchMin.z);

        yaw = Mathf.Clamp(yaw, yawMin.y, yawMin.z);

        Debug.Log(yaw + startRotation.y);
        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yaw + startRotation.y), ref rotationSmoothVelocity, rotationSmoothTime);
        horizontal.transform.eulerAngles = currentRotation;


        currentRotationVert = Vector3.SmoothDamp(currentRotationVert, new Vector3(pitch + startRotation.x, yaw + startRotation.y), ref rotationSmoothVelocityVert, rotationSmoothTime);
        vertical.transform.eulerAngles = currentRotationVert;
    }

    private void InputPress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

    }

    private void Shoot()
    {
        GameObject cannon = Instantiate(CannonPrefab, puntoDisparo.transform.position, transform.rotation);
        Vector3 _direction = 2 * puntoDisparo.transform.forward;
        Vector3 _force = _direction.normalized * cannonSpeed;
        cannon.transform.up = cannon.transform.forward;
        cannon.transform.GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);

        //status = ActiveCanon.Disable;
    }


}
