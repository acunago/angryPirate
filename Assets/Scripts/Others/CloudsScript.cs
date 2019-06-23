using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour
{
    private float i;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        i += Time.deltaTime;
            transform.Rotate( new  Vector3(0, Mathf.Abs(Mathf.Sin(i)) * Time.deltaTime, 0));
    }
}
