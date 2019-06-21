using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraScript myCam;
    public Transform myCannon;

    private float points;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        CannonTarget();

    }

    void Update()
    {
        if (myCam.targets.Count < 1)
            CannonTarget();
    }

    // Devuelve los puntos
    public float GetPoints()
    {
        return points;
    }

    // Suma puntos
    public void SetPoints(float aux)
    {
        points = points + aux;
    }

    // Limpia la lista de targets
    public void ClearTargets()
    {
        myCam.targets.Clear();
    }

    // Agrega un target a la camara
    public void AddTarget(Transform target)
    {
        myCam.targets.Add(target);
    }

    // elimina un target a la camara
    public void RemoveTarget(Transform target)
    {
        if (myCam.targets.Contains(target))
            myCam.targets.Remove(target);
    }

    // Devuelve el target al cañon
    public void CannonTarget()
    {
        myCam.targets.Clear();
        myCam.targets.Add(myCannon);
    }
}
