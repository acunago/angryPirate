using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{

    public float Points;

    private Animator anim;
    // Start is called before the first frame update
    private void Start()
    {
        if (transform.GetComponent<Animator>())
        {
            anim = transform.GetComponent<Animator>();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject, 1);
            anim.SetBool("death", true);
        }

    }

}
