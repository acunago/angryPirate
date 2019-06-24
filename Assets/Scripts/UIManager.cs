using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Canvas Settings")]
    [Tooltip("Score points output")]
    [SerializeField]
    private Text score;
    [Tooltip("Time left output")]
    [SerializeField]
    private Slider timeLeft;
    [Tooltip("Basic bullets output")]
    [SerializeField]
    private Text basicBullets;
    [Tooltip("Spread bullets output")]
    [SerializeField]
    private Text spreadBullets;
    [Tooltip("Explosive bullets output")]
    [SerializeField]
    private Text explosiveBullets;

    [Header("Objects Settings")]
    [Tooltip("Players cannon")]
    [SerializeField]
    private CannonScript mCannon;

    private int mPoints;
    private int[] mBullets;

    void Start()
    {
        mPoints = GameManager.instance.GetPoints();
        mBullets = mCannon.GetBullets();
    }

    void Update()
    {
        RefreshTime();
        RefreshScore();
        RefreshBullets();
    }

    // Mantiene actualizado el puntaje
    private void RefreshScore()
    {
        if(mPoints == GameManager.instance.GetPoints())
            return;

        mPoints = GameManager.instance.GetPoints();
        score.text = mPoints + " Pts.";
    }

    // Mantiene actualizado el tiempo restante
    private void RefreshTime()
    {
        timeLeft.value = GameManager.instance.GetTime();
    }

    // Mantiene actualizado la cantidad de balas
    private void RefreshBullets()
    {
        if (mBullets == mCannon.GetBullets())
            return;

        mBullets = mCannon.GetBullets();
        basicBullets.text = "1: " + mBullets[0];
        spreadBullets.text = "2: " + mBullets[1];
        explosiveBullets.text = "3: " + mBullets[2];
    }
}
