using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsScript : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Speed of clouds")]
    [SerializeField]
    private float speed;

    void Update()
    {
        transform.Rotate( Vector3.up * speed * Time.deltaTime);
    }
}
