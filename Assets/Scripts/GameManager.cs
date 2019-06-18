using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private float points;
    public  static GameManager instance;
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetPoints()
    {

        return points;
    }
    public void SetPoints( float aux)
    {
        points = points - aux;

    }

}
