using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Score points")]
    [SerializeField]
    private int mPoints;
    [Tooltip("Collision Sound")]
    [SerializeField]
    private AudioClip mSound;

    private Animator anim;

    void Start()
    {
        if (GetComponent<Animator>())
        {
            anim = GetComponent<Animator>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            GameManager.instance.SetPoints(mPoints);
            Destroy(gameObject, 1);
            if(anim != null)
                anim.SetBool("death", true);
            if (mSound != null)
                AudioManager.instance.PlaySound(mSound);
        }
    }

}
