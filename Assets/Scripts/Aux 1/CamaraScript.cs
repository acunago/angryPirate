using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraScript : MonoBehaviour
{
    public GameObject Cannon;
    public GameObject camera;
    public float timeFollow;
    public bool followCannon;
    public bool backOriginal;
    public Vector3 sumPath;
    private float auxTimeFollow;
    private Vector3 originalPos;
    private Vector3 followPath;
    // Start is called before the first frame update

    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {

        if (Cannon != null && followCannon)
        {
            auxTimeFollow += Time.deltaTime;
            if (timeFollow < auxTimeFollow)
            {
                followPath = new Vector3(Cannon.transform.position.x + sumPath.x, Cannon.transform.position.y + sumPath.y, Cannon.transform.position.z + sumPath.z);
                camera.transform.position = Vector3.Lerp(transform.position, followPath, 1f);
            }
        }
        if (backOriginal)
        {
            BackOriginal();
        }
    }

    public void BackOriginal()
    {
        camera.transform.position = Vector3.Lerp(transform.position, originalPos, 0.5f);
    }
}

