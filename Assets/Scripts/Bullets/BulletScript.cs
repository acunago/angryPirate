using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("General Settings")]
    [Tooltip("Action Key")]
    [SerializeField]
    protected KeyCode mKey = KeyCode.E;

    protected Transform tr;
    protected Rigidbody rb;

    protected void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    // Spawnea objetos con una direccion y una fuerza adicional
    public void SpawnWithDirection(GameObject _prefab, Vector3 _direction, float _force)
    {
        GameObject mSpawn;
        mSpawn = Instantiate(_prefab, tr.position, Quaternion.identity);
        mSpawn.transform.SetParent(tr);
        mSpawn.GetComponent<Rigidbody>().velocity = _direction;

        if (_force != 0)
        {
            Vector3 additionalForce = _direction.normalized * _force;
            mSpawn.GetComponent<Rigidbody>().AddForce(additionalForce, ForceMode.Impulse);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {/* GENERAMOS DAÑO O ALGUN EVENTO?????
        if (collision.gameObject.layer == gameObject.layer)
            return;

        if (collision.gameObject.layer == myGlobals.playerLayer
            || collision.gameObject.layer == myGlobals.enemyLayer)
        {
            collision.gameObject.SendMessage("ApplyDamage", bulletDamage);
        }

        Destroy(gameObject);*/
    }
}
