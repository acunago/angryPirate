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

    public float countCannon = 5;
    public float countExplosive = 5;
    public float countSpread = 5;


    private float yaw;
    private float pitch;

    public float rotationSmoothTime = .1f;
    public GameObject CannonPrefab;
    public GameObject ExpolosivePrefab;
    public GameObject SpreadPrefab;

    public GameObject CannonShoot;

    public Transform puntoDisparo;

    public float cannonSpeed;


    public float timeShoot = 2;

    public float auxShoot;
    private bool hasShoot;

    Vector3 rotationSmoothVelocity;
    Vector3 rotationSmoothVelocityVert;
    Vector3 currentRotation;
    Vector3 currentRotationVert;

    private Vector3 startRotation;

    public ActiveCanon status;

    void Start()
    {
        startRotation = transform.rotation.eulerAngles;
        CannonShoot = CannonPrefab;
    }

    void Update()
    {
        if (status == ActiveCanon.Active)
        {
            if (hasShoot)
            {
                auxShoot += Time.deltaTime;
                if (auxShoot >= timeShoot)
                {
                    auxShoot = 0;
                    hasShoot = false;
                }
            }
            Movement();
            InputPress();
        }
    }

    // Movimiento del cañon
    private void Movement()
    {
        if (hasShoot) return;
        Vector3 posFinal = Vector3.zero;
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;


        pitch = Mathf.Clamp(pitch, pitchMin.y, pitchMin.z);

        yaw = Mathf.Clamp(yaw, yawMin.y, yawMin.z);


        currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(0, yaw + startRotation.y), ref rotationSmoothVelocity, rotationSmoothTime);
        horizontal.transform.eulerAngles = currentRotation;


        currentRotationVert = Vector3.SmoothDamp(currentRotationVert, new Vector3(pitch + startRotation.x, yaw + startRotation.y), ref rotationSmoothVelocityVert, rotationSmoothTime);
        vertical.transform.eulerAngles = currentRotationVert;
    }

    // Consola de inputs
    private void InputPress()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!hasShoot)
            {
                Shoot(CannonShoot);
                hasShoot = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (countExplosive >= 0)
            {
                CannonShoot = ExpolosivePrefab;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (countSpread >= 0)
            {
                CannonShoot = SpreadPrefab;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (countCannon >= 0)
            {
                CannonShoot = CannonPrefab;
            }
        }
    }

    // Disparo del cañon
    private void Shoot(GameObject cannonShot)
    {
        GameObject cannon = Instantiate(cannonShot, puntoDisparo.position, transform.rotation);
        Vector3 _direction = puntoDisparo.forward;
        Vector3 _force = _direction.normalized * cannonSpeed;
        cannon.transform.up = cannon.transform.forward;
        cannon.transform.GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);

        GameManager.instance.ClearTargets();
        GameManager.instance.AddTarget(cannon.transform);

        //status = ActiveCanon.Disable;
    }


}
