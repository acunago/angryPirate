using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("General Settings")]
    [Tooltip("Pause Key")]
    public KeyCode pauseKey;
    [Tooltip("First stage time")]
    public float stageTime1;
    [Tooltip("First stage points")]
    public float stagePoints1;

    [Header("Camera Settings")]
    [Tooltip("Main Camera Access")]
    public CameraScript mCamera;
    [Tooltip("Camera respawn position after shooting")]
    public Transform cameraRespawn;
    [Tooltip("Cannon target position")]
    public Transform cannonTarget;
    [Tooltip("Witness target position")]
    public Transform witnessTarget;

    [Header("UI Settings")]
    [Tooltip("Message Text")]
    [SerializeField]
    private Text gameText;
    [Tooltip("Console Panel")]
    [SerializeField]
    private GameObject consoleScreen;
    [Tooltip("Pause panel")]
    [SerializeField]
    private GameObject pauseScreen;
    [Tooltip("Resume Button")]
    [SerializeField]
    private Button resumeButton;
    [Tooltip("Exit Button")]
    [SerializeField]
    private Button exitButton;

    [Header("Private Checks")]
    [SerializeField]
    private int points;
    [SerializeField]
    private float mTime;
    [SerializeField]
    private bool gameOver;
    [SerializeField]
    private bool gamePause;
    [SerializeField]
    private bool godMode = false;

    void Awake()
    {
        MakeSingleton();
        CursorSettings();
        CannonTarget();
    }

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (!godMode)
            mTime += Time.deltaTime;

        if (mCamera.targets.Count < 1)
            CannonTarget();

        if (Input.GetKeyDown(pauseKey))
            PauseGame();

        CheckPause();
        CheckGame();

        if (gameOver)
            GameOver();
    }

    // Convierte el objeto en singleton
    private void MakeSingleton()
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

    // Inicia el juego
    public void StartGame()
    {
        Time.timeScale = 1;
        gameOver = false;
        gamePause = false;
        points = 0;
        mTime = 0;
    }

    // Pausa el juego
    private void PauseGame()
    {
        if (!gameOver)
            gamePause = !gamePause;
    }

    // Revisa la pausa del juego
    private void CheckPause()
    {
        if (consoleScreen.activeSelf)
        {
            Time.timeScale = 0;
        }
        else if (gamePause)
        {
            Time.timeScale = 0;
            gameText.text = "PAUSE";
            resumeButton.GetComponentInChildren<Text>().text = "Resume";
            exitButton.GetComponentInChildren<Text>().text = "Quit";
            pauseScreen.SetActive(true);
            Cursor.visible = true;
        }
        else
        {
            pauseScreen.SetActive(false);
            Cursor.visible = false;
            Time.timeScale = 1;
        }
    }

    // Controla el si termina el juego
    private void CheckGame()
    {
        if (mTime >= stageTime1)
            gameOver = true;
        if (points >= stagePoints1)
            gameOver = true;
    }

    // Termina el juego
    private void GameOver()
    {
        gamePause = true;
        if (mTime >= stageTime1)
        {
            gameText.text = "YOU LOSE";
            resumeButton.GetComponentInChildren<Text>().text = "Retry";
            exitButton.GetComponentInChildren<Text>().text = "Exit";
        }
        else
        {
            gameText.text = "YOU WIN";
            resumeButton.GetComponentInChildren<Text>().text = "Restart";
            exitButton.GetComponentInChildren<Text>().text = "Exit";
        }
    }

    //Comportamiento del boton Resume
    public void Resume()
    {
        PauseGame();

        if (gameOver)
            GoToScene("Level01");
    }

    // Ajusta la configuracion del Mouse
    private void CursorSettings()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Devuelve el target al cañon
    private void CannonTarget()
    {
        mCamera.targets.Clear();
        mCamera.SetTransform(cameraRespawn);
        mCamera.targets.Add(cannonTarget);
    }

    // Limpia la lista de targets
    public void ClearTargets()
    {
        mCamera.targets.Clear();
    }

    // Coloca al witness la lista de targets
    public void SetWitness(Vector3 _position)
    {
        witnessTarget.position = _position;
        mCamera.targets.Add(witnessTarget);
        witnessTarget.GetComponent<WitnessScript>().Watch();
    }

    // Agrega un target a la camara
    public void AddTarget(Transform target)
    {
        mCamera.targets.Add(target);
    }

    // elimina un target a la camara
    public void RemoveTarget(Transform target)
    {
        if (mCamera.targets.Contains(target))
            mCamera.targets.Remove(target);
    }

    // Devuelve los puntos
    public int GetPoints()
    {
        return points;
    }

    // Devuelve el tiempo restante
    public float GetTime()
    {
        return (stageTime1 - mTime) / stageTime1;
    }

    // Suma puntos
    public void SetPoints(int _addingPoints)
    {
        points += _addingPoints;
    }

    // Devuelve el god mode
    public bool GetGodMode()
    {
        return godMode;
    }

    // Cambia el god mode
    public void ChangeGodMode()
    {
        godMode = !godMode;
    }

    // Inicia escena indicada
    public void GoToScene(string i)
    {
        SceneManager.LoadScene(i);
    }
}
