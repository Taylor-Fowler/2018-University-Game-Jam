using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Player;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _weaponTransform;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private Weapon[] _weapons;
    private int _weaponsBought = 0;
    private int _weaponSpeedUpgraded = 0;
    private int _speedUpgraded = 0;
    private Rigidbody2D _rigidBody2D;

    private void Awake()
    {
        GameManager.Event_OnGameOver += OnGameOver;
    }

    private void Start()
    {
        Player = this;
        _rigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        GameManager.Event_OnGameOver -= OnGameOver;
    }

    private void Update()
    {
        if(!GameManager.GameStarted)
        {
            return;
        }
        int vertical = 0;
        int horizontal = 0;

        if (Input.GetKey(KeyCode.W)) vertical += 1;
        if (Input.GetKey(KeyCode.S)) vertical -= 1;
        if (Input.GetKey(KeyCode.D)) horizontal += 1;
        if (Input.GetKey(KeyCode.A)) horizontal -= 1;

        transform.Translate(new Vector3(horizontal, vertical, 0f) * Time.deltaTime * _speed);
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 position = transform.position;
        Vector3 cameraBottomLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        if (screenPosition.x < 0f)
        {
            position.x = cameraBottomLeft.x;
        }
        else if(screenPosition.x >= Screen.width)
        {
            position.x = topRight.x;
        }

        if (screenPosition.y < 0f)
        {
            position.y = cameraBottomLeft.y;
        }
        else if (screenPosition.y >= Screen.height)
        {
            position.y = topRight.y;
        }

        transform.position = position;

        Vector2 mouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        mouseDirection.Normalize();

        _weaponTransform.rotation = Quaternion.Euler
            (
                new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(mouseDirection.y, mouseDirection.x))
            );


        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            foreach(var weapon in _weapons)
            {
                weapon.Fire();
            }
            //_weapon.Fire(mouseDirection);
        }
    }

    private void OnGameOver()
    {
        Destroy(this);
    }

    public void PositiveCharged(GameObject charge)
    {
        Vector2 motion = transform.position - charge.transform.position;
        _rigidBody2D.AddForce(motion * 2f);
    }

    public void UpgradeSpeed()
    {
        _speedUpgraded++;
        _speed += 1f;
    }

    public void UpgradeWeapons()
    {
        _weaponSpeedUpgraded++;
        foreach(var weapon in _weapons)
        {
            weapon.UpdateFireRate();
        }
    }

    public bool CanUpgradeWeapons()
    {
        return _weaponSpeedUpgraded < 3;
    }

    public bool CanBuyWeapons()
    {
        return _weaponsBought < 3;
    }

    public bool CanBuySpeed()
    {
        return _speedUpgraded < 3;
    }

    public void BuyWeapons()
    {
        _weaponsBought++;
        _weapons[_weaponsBought].gameObject.SetActive(true);
    }
}
