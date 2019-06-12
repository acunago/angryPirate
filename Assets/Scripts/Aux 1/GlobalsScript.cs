using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyGlobals", menuName = "My Globals")]
public class GlobalsScript : ScriptableObject
{
    [Header("Generals")]
    public int players = 1;
    public bool gameOver = false;

    public float timeSpeed = 1;
    public float iaLevel = 1;

    [Header("Player 1")]
    [Tooltip("Guarda el indice de cuerpo a utilizar")]
    public int player1 = 0;
    [Tooltip("Guarda el puntaje del player")]
    public int scoreP1 = 0;
    [Tooltip("Guarda la vida del player")]
    public float lifeP1 = 1f;
    [Tooltip("Guarda las balas de cada arma del player")]
    public int[] ammoP1 = new int[] { 0, 0, 0 };
    [Tooltip("Guarda las armas que tiene desbloqueadas el player")]
    public bool[] WeaponsP1 = new bool[] { true, false, false };

    [Header("Player 2")]
    [Tooltip("Guarda el indice de cuerpo a utilizar")]
    public int player2 = 0;
    [Tooltip("Guarda el puntaje del player")]
    public int scoreP2 = 0;
    [Tooltip("Guarda la vida del player")]
    public float lifeP2 = 1f;
    [Tooltip("Guarda las balas de cada arma del player")]
    public int[] ammoP2 = new int[] { 0, 0, 0 };
    [Tooltip("Guarda las armas que tiene desbloqueadas el player")]
    public bool[] WeaponsP2 = new bool[] { true, false, false };

    [Header("Player 3")]
    [Tooltip("Guarda el indice de cuerpo a utilizar")]
    public int player3 = 0;
    [Tooltip("Guarda el puntaje del player")]
    public int scoreP3 = 0;
    [Tooltip("Guarda la vida del player")]
    public float lifeP3 = 1f;
    [Tooltip("Guarda las balas de cada arma del player")]
    public int[] ammoP3 = new int[] { 0, 0, 0 };
    [Tooltip("Guarda las armas que tiene desbloqueadas el player")]
    public bool[] WeaponsP3 = new bool[] { true, false, false };

    [Header("Player 4")]
    [Tooltip("Guarda el indice de cuerpo a utilizar")]
    public int player4 = 0;
    [Tooltip("Guarda el puntaje del player")]
    public int scoreP4 = 0;
    [Tooltip("Guarda la vida del player")]
    public float lifeP4 = 1f;
    [Tooltip("Guarda las balas de cada arma del player")]
    public int[] ammoP4 = new int[] { 0, 0, 0 };
    [Tooltip("Guarda las armas que tiene desbloqueadas el player")]
    public bool[] WeaponsP4 = new bool[] { true, false, false };

    [Header("Physics Settings")]
    [Tooltip("Distancia al suelo para considerarlo grounded")]
    public float groundDistance = 0.1f;
    [Tooltip("Layer del suelo")]
    public LayerMask groundLayer;
    [Tooltip("Layer de players")]
    [SerializeField]
    public LayerMask playerLayer;
    [Tooltip("Layer de enemigos")]
    [SerializeField]
    public LayerMask enemyLayer;
}
