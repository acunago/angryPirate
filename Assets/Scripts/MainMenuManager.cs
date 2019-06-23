using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("Camera Transform")]
    public Transform mCamera;
    [Tooltip("Camera Transforms to use")]
    public Transform[] cameraPositions;
    [Tooltip("Camera movement speed")]
    [SerializeField]
    private float cameraSpeed = 0.1f;

    private int currentCP; // Posicion actual en la que debe estar la camara

    void Start()
    {
        currentCP = 0;
    }

    void Update()
    {
        CameraMove();
    }

    // Mueve la camara a la posicion correspondiente
    private void CameraMove()
    {
        mCamera.localPosition = Vector3.Lerp(mCamera.localPosition, cameraPositions[currentCP].position, cameraSpeed);
        mCamera.localRotation = Quaternion.Slerp(mCamera.localRotation, cameraPositions[currentCP].rotation, cameraSpeed);
    }

    // Cambia la posicion actual en la que debe estar la camara
    public void CameraChangePosition(int _positionIndex)
    {
        currentCP = _positionIndex;
    }

    // Carga la escena indicada
    public void GoToScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    // Abandona el juego
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting..");
    }
}
