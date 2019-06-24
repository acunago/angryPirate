using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBullet : BulletScript
{
    [Header("General Settings")]
    [Tooltip("Action Key")]
    [SerializeField]
    protected KeyCode mKey = KeyCode.Mouse0;
    [Tooltip("Action sound")]
    public AudioClip mSound;

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

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(mKey))
            MyBehaviour();
    }

    // Explota la bala
    private void MyBehaviour()
    {
        Instantiate(eEffect, tr.position, Quaternion.identity);
        AudioManager.instance.PlaySound(mSound);

        Collider[] colliders = Physics.OverlapSphere(tr.position, eRadius);
        foreach (Collider nearby in colliders)
        {
            Rigidbody afected = nearby.GetComponent<Rigidbody>();
            if (afected != null)
                afected.AddExplosionForce(eForce, tr.position, eRadius);
        }

        colliders = Physics.OverlapSphere(tr.position, eRadius/2);
        foreach (Collider nearby in colliders)
        {
            TargetScript victim = nearby.GetComponent<TargetScript>();
            if (victim != null)
                victim.Die();
        }
        GameManager.instance.SetWitness(tr.position);
        GameManager.instance.RemoveTarget(tr);
        Destroy(gameObject);
    }
}
