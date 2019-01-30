using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float FireRate = 0.25f;

    [SerializeField] private Transform _bulletAnchor;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletListAnchor;

    private float _lastFired = 0f;

    private void Start()
    {
        _lastFired = -FireRate;
    }

    public void Fire()
    {
        if(!gameObject.activeInHierarchy)
        {
            return;
        }

        if (Time.time - _lastFired >= FireRate)
        {
            _lastFired = Time.time;
            GameObject createdBullet = Instantiate(_bulletPrefab, _bulletListAnchor);
            Vector3 direction = _bulletAnchor.position - transform.position;
            direction.Normalize();
            createdBullet.GetComponent<BulletController>().Direction = direction;
            createdBullet.transform.position = _bulletAnchor.position;
        }
    }

    public void UpdateFireRate()
    {
        FireRate *= 0.9f;
    }
}
