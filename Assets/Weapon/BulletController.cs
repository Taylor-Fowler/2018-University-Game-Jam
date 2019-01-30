using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public static float Speed = 7f;

    public float DestroyAfter = 3f;
    public Vector3 Direction;
    private Rigidbody2D _rigidBody;
    private Collider2D _collider2D;
    private float _lifetime;

    private void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
        _lifetime = 0f;
    }

    private void Update()
    {
        transform.Translate(Direction * Speed * Time.deltaTime);
        _lifetime += Time.deltaTime;

        if(_lifetime > 3f)
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ElectronController electronController = collision.gameObject.GetComponent<ElectronController>();

        if(electronController)
        {
            electronController.PositiveCharged(gameObject);
            Destroy(_rigidBody, 0.01f);
            Destroy(_collider2D, 0.01f);
            Destroy(this);
            return;
        }

        ProtonController protonController = collision.gameObject.GetComponent<ProtonController>();
        if(protonController)
        {

        }


        Destroy(gameObject);

    }
}
