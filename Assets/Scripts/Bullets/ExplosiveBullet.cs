using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletScript
{
    [Header("Explosive Settings")]
    [Tooltip("Explosion Force")]
    [SerializeField]
    private float eForce = 10;
    [Tooltip("Explosion Radius")]
    [SerializeField]
    private float eRadius = 5;
    [Tooltip("Efecto de explosión de la bomba")]
    [SerializeField]
    private GameObject eEffect;

    void Update()
    {
        if (Input.GetKeyDown(mKey))
            MyBehaviour();
    }

    // Explota la bala
    private void MyBehaviour()
    {
        Instantiate(eEffect, tr.position, Quaternion.identity);

        Collider[] colliders = Physics.OverlapSphere(tr.position, eRadius);
        foreach (Collider nearby in colliders)
        {/* GENERAMOS DAÑO O ALGUN EVENTO?????
            if (nearby.gameObject.layer == myGlobals.playerLayer
            || nearby.gameObject.layer == myGlobals.enemyLayer)
            {
                nearby.gameObject.SendMessage("ApplyDamage", explosionDamage); // MEJORAR CURVA DE DAÑO
            }*/

            Rigidbody afected = nearby.GetComponent<Rigidbody>();
            if (afected != null)
            {
                afected.AddExplosionForce(eForce, tr.position, eRadius);
            }
        }

        Destroy(gameObject);

    }
}
