using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScript : MonoBehaviour
{
    public Dictionary<string, string> commandsDescriptions = new Dictionary<string, string>();
    public delegate void FunctionPrototype0P(); // Comandos sin parametros
    public Dictionary<string, FunctionPrototype0P> noPCommands = new Dictionary<string, FunctionPrototype0P>();

    [Header("Objects Settings")]
    [Tooltip("Panel contenedor de la consola")]
    public GameObject consolePanel;
    [Tooltip("Text de salida de información")]
    public Text outputText;
    [Tooltip("InputField de ingreso de información")]
    public InputField inputField;
    [Tooltip("Tecla para abrir o cerrar la consola")]
    public KeyCode consoleKey;

    public static ConsoleScript instance;

    private void Awake()
    {
        MakeSingleton();

        ClearOutput();
        BasicCommands();
    }

    private void Update()
    {
        if (Input.GetKeyDown(consoleKey))
        {
            consolePanel.SetActive(!consolePanel.activeSelf);

            if (consolePanel.activeSelf)
                inputField.Select();
        }

        if (consolePanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Write("> " + inputField.text);

                if (noPCommands.ContainsKey(inputField.text))
                {
                    noPCommands[inputField.text].Invoke();
                }
                else
                {
                    Write("The \"" + inputField.text + "\" command does not exist or is badly entered.");
                }

                ClearInput();
            }
        }
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

        DontDestroyOnLoad(gameObject);
    }

    // Registra un comando en los diccionarios correspondientes
    public void RegisterCommand(string _name, string _description, FunctionPrototype0P _command)
    {
        commandsDescriptions.Add(_name, _description);
        noPCommands.Add(_name, _command);
    }

    // Escribe un texto en la consola
    public void Write(string _text)
    {
        outputText.text += _text + "\n";
    }

    // Escribe un texto en la consola
    private void ClearOutput()
    {
        outputText.text = "Welcome to the developer console" + "\n";
    }

    // Escribe un texto en la consola
    private void ClearInput()
    {
        inputField.text = "";
        inputField.Select();
        inputField.ActivateInputField();
    }

    // Registra los comandos básicos en la consola
    private void BasicCommands()
    {
        RegisterCommand("ExitGame", "Leave the game.", ExitGame);
        RegisterCommand("Help", "Shows the command list.", Help);
    }

    // Comando básico para abandonar el juego
    public void ExitGame()
    {
        Application.Quit();
    }

    // Comando básico de ayuda para ver lista de comandos
    public void Help()
    {
        Write("\n" + "COMMAND LIST" + "\n" + "\"[Command]\": [Description.]" + "\n");

        foreach (var item in noPCommands)
        {
            Write("\"" + item.Key + "\": " + commandsDescriptions[item.Key]);
        }
    }
}
