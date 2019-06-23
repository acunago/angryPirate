using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Camera Settings")]
    [Tooltip("Main Camera Access")]
    public CameraScript mCamera;
    [Tooltip("Camera respawn position after shooting")]
    public Transform cameraRespawn;
    [Tooltip("Cannon target position")]
    public Transform cannonTarget;

    [Header("Private Checks")]
    [SerializeField]
    private int points;

    void Start()
    {
        if (instance != null)
        {
            GameObject.Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        CursorSettings();
        CannonTarget();

        points = 0;
    }

    void Update()
    {
        if (mCamera.targets.Count < 1)
            CannonTarget();
    }

    // Ajusta la configuracion del Mouse
    private void CursorSettings()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Devuelve el target al cañon
    private void CannonTarget()
    {
        mCamera.targets.Clear();
        mCamera.SetTransform(cameraRespawn);
        mCamera.targets.Add(cannonTarget);
    }

    // Limpia la lista de targets
    public void ClearTargets()
    {
        mCamera.targets.Clear();
    }

    // Agrega un target a la camara
    public void AddTarget(Transform target)
    {
        mCamera.targets.Add(target);
    }

    // elimina un target a la camara
    public void RemoveTarget(Transform target)
    {
        if (mCamera.targets.Contains(target))
            mCamera.targets.Remove(target);
    }

    // Devuelve los puntos
    public int GetPoints()
    {
        return points;
    }

    // Suma puntos
    public void SetPoints(int _addingPoints)
    {
        points += _addingPoints;
    }
    
}
