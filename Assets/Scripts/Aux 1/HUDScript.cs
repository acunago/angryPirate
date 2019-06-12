using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Objeto de variables globales")]
    public GlobalsScript myGlobals;
    [Tooltip("Sprites de los logos a utilizar para los personajes")]
    [SerializeField]
    private Sprite[] LogoPJ;

    [Header("Player 1")]
    [Tooltip("Image de logo del player")]
    [SerializeField]
    private Image logoP1;
    [Tooltip("Image de la vida del player")]
    [SerializeField]
    private Image lifeP1;
    [Tooltip("Text de las balas del arma 1 del player")]
    [SerializeField]
    private Text[] ammoP1;

    private void Start()
    {
        SwapLogo();
    }

    private void Update()
    {
        LifeUpdate();
        AmmoUpdate();
    }

    // Intercambia el logo del player por el del personaje que controla
    private void SwapLogo()
    {
        logoP1.sprite = LogoPJ[myGlobals.player1 - 1];
    }

    // Actualiza la barra de vida del player
    private void LifeUpdate()
    {
        lifeP1.fillAmount = myGlobals.lifeP1;
    }

    // Actualiza las balas del arma del player
    private void AmmoUpdate()
    {
        for (int i = 0; i < 3; i++)
        {
            if(myGlobals.WeaponsP1[i])
            {
                ammoP1[i].text = "" + myGlobals.ammoP1[i];
            }
            else
            {
                ammoP1[i].text = "X";
            }
        }
    }
}
