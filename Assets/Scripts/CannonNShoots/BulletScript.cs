using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    protected Transform tr;
    protected Rigidbody rb;

    protected void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    protected virtual void Update()
    {
        KillMeNow(-2.5f);
    }

    // Spawnea objetos con una direccion y una fuerza adicional
    public void SpawnWithDirection(GameObject _prefab, Vector3 _direction, float _force)
    {
        GameObject mSpawn;
        mSpawn = Instantiate(_prefab, tr.position, tr.rotation);
        mSpawn.transform.forward = rb.velocity.normalized;
        Vector3 _aligned = _direction.x * tr.right
            + _direction.y * tr.up
            + _direction.z * tr.forward;
        _aligned = _aligned * rb.velocity.magnitude;
        mSpawn.GetComponent<Rigidbody>().velocity = _aligned;

        GameManager.instance.AddTarget(mSpawn.transform);

        if (_force != 0)
        {
            Vector3 additionalForce = _direction.normalized * _force;
            mSpawn.GetComponent<Rigidbody>().AddForce(additionalForce, ForceMode.Impulse);
        }
    }

    // Destruye la bala al entrar al mar
    protected void KillMeNow(float nivel)
    {
        if (tr.position.y < nivel)
        {
            CancelInvoke("KillMeNow");
            GameManager.instance.RemoveTarget(transform);
            Destroy(gameObject);
        }
    }

    // Destruye la bala
    protected void KillMeNow()
    {
            GameManager.instance.RemoveTarget(transform);
            Destroy(gameObject);
    }

    protected void OnCollisionEnter(Collision collision)
    {
        Invoke("KillMeNow",3);
    }
}
