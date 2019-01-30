using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectronController : Enemy
{
    public bool GivesEnergy = false;
    public float Speed = 2f;
    public float ChargeSpeed = 0.1f;
    public Color NeutronColour;
    public Color ProtonColour;

    [SerializeField] private SpriteRenderer _spriteToCharge;
    [SerializeField] private float _timeToCharge = 3f;

    private List<GameObject> _attachedCharges;
    private int _charges = 0;

    private void Start()
    {
        Speed = Random.Range(1f, 3f);
        _attachedCharges = new List<GameObject>();
    }

    private void Update()
    {
        Vector3 direction = PlayerController.Player.transform.position - transform.position;
        direction.z = 0f;
        direction.Normalize();

        //transform.Translate(direction * Speed * Time.deltaTime);
        float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Clamp(angle, -40f, 40f);
        transform.Translate(direction * Speed * Time.deltaTime, Space.World);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle), 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_charges != 0)
        {
            return;
        }

        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        if(playerController != null)
        {
            GameManager.OnLoseEnergy();
            GameManager.OnDestroyElectron(transform.position);
            Destroy(gameObject);
        }
    }

    public void PositiveCharged(GameObject charge)
    {
        Debug.Log("Positive Charged");
        _charges++;
        Speed = ChargeSpeed;
        if(_charges == 1)
        {
            StartCoroutine(StartCharging());
        }

        _attachedCharges.Add(charge);
        charge.transform.SetParent(transform);
    }

    private IEnumerator StartCharging()
    {
        Color initialColour = _spriteToCharge.color;
        Color targetColour = NeutronColour;
        float chargeTime = 0f;
        float timeLeft = _timeToCharge;

        while (chargeTime < timeLeft)
        {
            int charges = _charges;
            yield return null;

            chargeTime += Time.deltaTime * charges;
            _spriteToCharge.color = Color.Lerp(initialColour, targetColour, chargeTime / _timeToCharge);

            if(charges < 2 && _charges >= 2)
            {
                initialColour = _spriteToCharge.color;
                targetColour = ProtonColour;
            }
        }

        Combust();
    }

    private void Combust()
    {
        if(_charges == 1)
        {
            _owner.SpawnNeutron(transform, GivesEnergy);
        }
        else
        {
            _owner.SpawnProton(transform, _attachedCharges);
        }

        GameManager.OnDestroyElectron(transform.position);
        Destroy(gameObject);
    }
}
