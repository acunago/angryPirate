using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperConsole : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Objeto de variables globales")]
    public GlobalsScript myGlobals;

    private void Start()
    {
        ConsoleScript.instance.RegisterCommand("TGM", "Toggle God Mode.", GodMode);
    }

    // Comando que activa y desactiva el modo dios
    public void GodMode()
    {
        
    }
}