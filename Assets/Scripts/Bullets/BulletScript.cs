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
        mSpawn = Instantiate(_prefab, tr.position, Quaternion.identity);
        //mSpawn.transform.SetParent(tr);
        mSpawn.GetComponent<Rigidbody>().velocity = _direction;

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
            GameManager.instance.CannonTarget();
            Destroy(gameObject);
        }
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            GameManager.instance.SetPoints(5);
            //GameManager.instance.SetPoints(collision.gameObject.GetComponent<>())
        }

       /* GENERAMOS DAÑO O ALGUN EVENTO?????
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
