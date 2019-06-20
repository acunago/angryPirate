using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBullet : BulletScript
{
    [Header("General Settings")]
    [Tooltip("Action Key")]
    [SerializeField]
    protected KeyCode mKey = KeyCode.E;

    [Header("Spread Settings")]
    [Tooltip("Fragments Prefab")]
    [SerializeField]
    private GameObject fPrefab;
    [Tooltip("Fragments Quantity")]
    [SerializeField]
    private int fCount = 3;
    [Tooltip("Spread Radius")]
    [SerializeField]
    private float sRadius = 0;
    [Tooltip("Additional Force")]
    [SerializeField]
    private float aForce = 0;

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(mKey))
            MyBehaviour();
    }
    
    // Fragmenta la bala en varios fragmentos
    private void MyBehaviour()
    {
        GameManager.instance.ClearTargets();

        float _step = 360f / fCount;
        float _ang = 0f;

        for (int i = 0; i < fCount; i++)
        {
            Vector3 _dir = Vector3.zero;
            _dir.x = Mathf.Sin(_ang * Mathf.PI / 180) * sRadius;
            _dir.y = Mathf.Cos(_ang * Mathf.PI / 180) * sRadius;
            _dir.z = 1f;
            _dir = _dir.normalized + rb.velocity;

            SpawnWithDirection(fPrefab, _dir, aForce);

            _ang += _step;
        }

        Destroy(gameObject);
    }
}
