using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScript : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Objetivos a seguir")]
    public List<Transform> targets;
    [Tooltip("Direccion de la cámara")]
    [SerializeField]
    private Vector3 cameraDirection = new Vector3(1f, 1.5f, -1f);
    [SerializeField]
    private Transform point1;
    [SerializeField]
    private Transform point2;
    [Tooltip("Distancia de la cámara")]
    [SerializeField]
    private float cameraDistance = 20f;

    [Header("Movement Settings")]
    [Tooltip("Zoom mínimo de la cámara")]
    [SerializeField]
    private float zoomMin = 60f;
    [Tooltip("Zoom máximo de la cámara")]
    [SerializeField]
    private float zoomMax = 20f;
    [Tooltip("Factor de ajuste del zoom")]
    [SerializeField]
    private float zoomFix = 10f;
    [Tooltip("Factor de ajuste del movimiento")]
    [SerializeField]
    private float moveFix = 0.3f;

    private Vector3 smoothVelocity; // Espacio de memoria para el SmoothDamp (NO USAR)

    private Transform tr;
    private Camera cam;

    void Awake()
    {
        tr = GetComponent<Transform>();
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;
        PointDirection();
        Follow();
        Zoom();
    }

    // Establece la direccion en base a dos puntos
    private void PointDirection()
    {
        if (point1 == null || point2 == null)
            return;

        cameraDirection = point2.position - point1.position;
    }

    // Mueve la cámara para que siga a los targets
    private void Follow()
    {
        Vector3 _focus = GetFocus();
        Vector3 _destination = _focus + cameraDirection.normalized * cameraDistance;

        tr.position = Vector3.SmoothDamp(tr.position, _destination, 
            ref smoothVelocity, moveFix);

        tr.LookAt(_focus);
    }

    // Ajusta el zoom de la cámara
    private void Zoom()
    {
        float _zoom = Mathf.Lerp(zoomMax, zoomMin, GetAmplitude() / zoomFix);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, _zoom, Time.deltaTime);
    }

    // Devuelve la posición central entre los targets
    private Vector3 GetFocus()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }

    // Devuelve la distancia máxima entre los targets (segun los ejes)
    private float GetAmplitude()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);

        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }

        float _amplitude = (bounds.size.x > bounds.size.z) ? bounds.size.x : bounds.size.z;
        
        return _amplitude;
    }

    // ubica mi camaran en un transform dado
    public void SetTransform(Transform data)
    {
        tr.position = data.position;
    }
}
