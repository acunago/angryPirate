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

        CannonTarget();

    }

    void Update()
    {
        
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

    // Devuelve el target al cañon
    public void CannonTarget()
    {
        myCam.targets.Clear();
        myCam.targets.Add(myCannon);
    }
}
