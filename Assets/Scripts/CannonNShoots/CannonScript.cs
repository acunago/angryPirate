using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CannonState
{
    Active,
    Shooting,
    Disable
}

public class CannonScript : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Mouse sensitivity for aiming")]
    public float mouseSensitivity = 2;
    [Tooltip("Minimum time between shots")]
    public float shootCooldown = 2;

    [Header("Shoot Settings")]
    [Tooltip("Shot spawning point")]
    public Transform shootSpawn;
    [Tooltip("Cannon shooting force")]
    public float shootForce = 2500;
    [Tooltip("Shooting prefabs")]
    public List<GameObject> prefabs;
    [Tooltip("Shooting bullets")]
    public List<int> bullets;
    [Tooltip("Shooting sound")]
    public AudioClip shootSound;

    [Header("Movement Settings")]
    [Tooltip("Cannon barrel for the vertical movement")]
    public Transform mBarrel;
    [Tooltip("Movement yaw, minumum yaw, maximum yaw")]
    [SerializeField]
    private Vector3 mYaw = new Vector3(0, -30, 30);
    [Tooltip("Movement pitch, minumum pitch, maximum pitch")]
    [SerializeField]
    private Vector3 mPitch = new Vector3(0, -55, 10);
    [Tooltip("Movement fix factor")]
    [SerializeField]
    private float moveFix = 0.1f;

    [Header("Private Checks")]
    [SerializeField]
    private CannonState mState;
    [SerializeField]
    private int currentShoot;

    private float shootLapse; // Cuenta el tiempo transcurrido desde el ultimo disparo
    private Vector3 startRotation; // Mantiene la rotacion inicial para anclar los limitadores
    private Vector3 horizontalRotation; // Mantiene la rotacion horizontal
    private Vector3 verticalRotation; // Mantiene la rotacion vertical

    private Vector3 smoothVelocityH; // Espacio de memoria para el SmoothDamp (NO USAR)
    private Vector3 smoothVelocityV; // Espacio de memoria para el SmoothDamp (NO USAR)

    private Transform tr;

    void Awake()
    {
        tr = GetComponent<Transform>();

        currentShoot = 0;
        startRotation = transform.rotation.eulerAngles;
        horizontalRotation = Vector3.zero;
        verticalRotation = Vector3.zero;
        shootLapse = 0;
    }

    void Update()
    {
        switch (mState)
        {
            case CannonState.Active:
                InputPress();
                Movement();
                break;
            case CannonState.Shooting:
                ShootCooldown();
                break;
            case CannonState.Disable:
                break;
            default:
                break;
        }
    }

    // Consola de inputs
    private void InputPress() // REVISAR UBICACION DE LOS IMPUTS
    {
        if (Input.GetMouseButtonDown(0) && bullets[currentShoot] > 0)
            Shoot();
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
            currentShoot = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2))
            currentShoot = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3))
            currentShoot = 2;
    }

    // Movimiento del cañon
    private void Movement()
    {
        mYaw.x += Input.GetAxis("Mouse X") * mouseSensitivity;
        mYaw.x = Mathf.Clamp(mYaw.x, mYaw.y, mYaw.z);
        mPitch.x -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        mPitch.x = Mathf.Clamp(mPitch.x, mPitch.y, mPitch.z);

        horizontalRotation = Vector3.SmoothDamp(horizontalRotation, 
            new Vector3(0, mYaw.x + startRotation.y, 0), 
            ref smoothVelocityH, moveFix);
        verticalRotation = Vector3.SmoothDamp(verticalRotation, 
            new Vector3(mPitch.x + startRotation.x, 0, 0), 
            ref smoothVelocityV, moveFix);

        tr.eulerAngles = horizontalRotation;
        mBarrel.eulerAngles = horizontalRotation + verticalRotation;
    }

    // Disparo del cañon
    private void Shoot()
    {
        mState = CannonState.Shooting;
        GameObject mShot = Instantiate(prefabs[currentShoot], shootSpawn.position, shootSpawn.rotation);
        bullets[currentShoot]--;

        Vector3 _force = shootSpawn.forward * shootForce;
        mShot.transform.GetComponent<Rigidbody>().AddForce(_force, ForceMode.Impulse);
        AudioManager.instance.PlaySound(shootSound);

        GameManager.instance.ClearTargets();
        GameManager.instance.AddTarget(mShot.transform);
    }

    // Reactiva el cañon luego del lapso de tiempo de cooldown
    private void ShootCooldown()
    {
        shootLapse += Time.deltaTime;
        if (shootLapse >= shootCooldown)
        {
            shootLapse = 0;
            mState = CannonState.Active;
        }
    }

    // Devuelve la cantidad de balas
    public int[] GetBullets()
    {
        return bullets.ToArray();
    }
}
