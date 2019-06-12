using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Objeto de variables globales")]
    public GlobalsScript myGlobals;

    [Header("Objects Settings")]
    [Tooltip("Panel contenedor de la pantalla Press Any Button")]
    [SerializeField]
    private GameObject pressAnyPanel;
    [Tooltip("Panel contenedor de la pantalla principal de menu")]
    [SerializeField]
    private GameObject MainMenuPanel;
    [Tooltip("Botón de inicio del juego")]
    [SerializeField]
    private Button startButton;
    [Tooltip("Botón de volver a seleccionar")]
    [SerializeField]
    private Button backButton;

    private int playersCount; // Guarda si se han elegido todos los players

    private void Awake()
    {
        playersCount = 1;
        ButtonState(startButton.gameObject, false);
        ButtonState(backButton.gameObject, false);
        pressAnyPanel.SetActive(true);
        MainMenuPanel.SetActive(false);
    }

    private void Update()
    {
        if (pressAnyPanel.activeSelf && Input.anyKeyDown)
        {
            pressAnyPanel.SetActive(false);
            MainMenuPanel.SetActive(true);
        }
    }

    //Asigna los personajes seleccionados
    public void PlayerSelect(int _index)
    {
        if (playersCount <= myGlobals.players )
        {
            switch (playersCount)
            {
                case 1:
                    myGlobals.player1 = _index;
                    ButtonState(backButton.gameObject, true);
                    break;
                case 2:
                    myGlobals.player2 = _index;
                    break;
                case 3:
                    myGlobals.player3 = _index;
                    break;
                case 4:
                    myGlobals.player4 = _index;
                    break;
                default:
                    break;
            }

            GameObject.Find("PJ0" + _index).GetComponent<Button>().interactable = false;

            if (playersCount == myGlobals.players)
            {
                EndSelect();
                ButtonState(startButton.gameObject, true);
            }
            else
            {
                playersCount++;
            }
        }
    }

    //Retrocede la seleccion de personajes
    public void BackSelect()
    {
        if (playersCount >= 1)
        {
            switch (playersCount)
            {
                case 1:
                    GameObject.Find("PJ0" + myGlobals.player1).GetComponent<Button>().interactable = true;
                    ButtonState(backButton.gameObject, false);
                    break;
                case 2:
                    GameObject.Find("PJ0" + myGlobals.player2).GetComponent<Button>().interactable = true;
                    break;
                case 3:
                    GameObject.Find("PJ0" + myGlobals.player3).GetComponent<Button>().interactable = true;
                    break;
                case 4:
                    GameObject.Find("PJ0" + myGlobals.player4).GetComponent<Button>().interactable = true;
                    break;
                default:
                    break;
            }

            if (playersCount > 1)
                playersCount--;
        }

        if (startButton.interactable)
        {
            ReSelect();
            ButtonState(startButton.gameObject, false);
        }
    }

    //Limpia el panel de seleccion
    public void EndSelect()
    {
        for (int i = 1; i <= 4; i++)
        {
            if (GameObject.Find("PJ0" + i).GetComponent<Button>().interactable == true)
            {
                GameObject.Find("PJ0" + i).SetActive(false);
            }
        }
    }

    //Reactiva el panel de seleccion
    public void ReSelect()
    {
        for (int i = 1; i <= 4; i++)
        {
            GameObject.Find("MainMenuSystem/Canvas/Selection/PJs/" + "PJ0" + i).SetActive(true);
        }
    }

    //Activa o desactiva un boton con texto
    public void ButtonState(GameObject _button, bool _state)
    {
        _button.GetComponent<Button>().interactable = _state;
        if (_state)
        {
            _button.GetComponentInChildren<Text>().color = Color.white;
        }
        else
        {
            _button.GetComponentInChildren<Text>().color = Color.grey;
        }

    }

    //Inicia escena indicada
    public void GoToScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }

    //Termina el juego
    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
