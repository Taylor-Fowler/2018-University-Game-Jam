using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtonController : Enemy
{
    [SerializeField] private float _baseSpeed = 3f;
    [SerializeField] private float _incrementalSpeed = 0.2f;
    [SerializeField] private float _decompositionTime = 2f;

    private int _charges = 0;
    private Vector3 _direction;
    private bool _decomposing = false;

    private void Start()
    {
        _direction = Random.insideUnitCircle.normalized;
    }

    private void Update()
    {
        if (!_decomposing)
        {
            float speed = _baseSpeed + (_incrementalSpeed * (_charges - 1));
            transform.Translate(speed * Time.deltaTime * _direction);

            if (Vector3.Distance(transform.position, Vector3.zero) > 20)
            {
                StartCoroutine(Decompose());
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

        if (playerController != null)
        {
            playerController.PositiveCharged(gameObject);
        }
    }

    private new void OnDestroy()
    {
        base.OnDestroy();

    }

    public void AddCharges(List<GameObject> chargeObjects)
    {
        foreach(var chargeObj in chargeObjects)
        {
            _charges++;
            chargeObj.transform.SetParent(transform);
        }
    }

    public void PositiveCharged(GameObject charge)
    {
        _charges++;
        charge.transform.SetParent(transform);
    }

    private IEnumerator Decompose()
    {
        float elapsedTime = 0f;
        _decomposing = true;

        while(elapsedTime < _decompositionTime)
        {
            yield return null;
            elapsedTime += Time.deltaTime;
        }

        Vector3 directionToOrigin = -transform.position;
        directionToOrigin.z = 0f;
        directionToOrigin.Normalize();

        _owner.SpawnRandomElectrons(transform.position, _charges, -30f, 30f, directionToOrigin, 1f, 3f);

        GameManager.OnDestroyProton(transform.position);
        Destroy(gameObject);
    }
}
