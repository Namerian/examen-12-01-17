using UnityEngine;
using System.Collections;

public enum Weapon
{
    Laser,
    Beam,
    Missile
}

public enum Owner
{
    Player,
    Enemy
}

public class PlayerController : MonoBehaviour
{
    public float _baseSpeed = 1;
    public float _verticalSpeed = 1;
    public float _horizontalSpeed = 2;

    public GameObject _laserProjectilePrefab;

    private ShipWeapon _weapon;

    // Use this for initialization
    void Start()
    {
        GameManager.Instance._PlayerController = this;

        _weapon = GetComponent<ShipWeapon>();
        _weapon.Initialize(Owner.Player);
    }

    // Update is called once per frame
    void Update()
    {
        // MOVEMENT
        if (Input.GetKey(KeyCode.Q))
        {
            GameManager.Instance._BaseScrollSpeed = _baseSpeed;

            Vector3 movement = new Vector3(-this._horizontalSpeed * Time.deltaTime, 0, 0);
            this.transform.Translate(movement);
        }
        else if (Input.GetKey(KeyCode.Z))
        {
            GameManager.Instance._BaseScrollSpeed = _verticalSpeed + _baseSpeed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            GameManager.Instance._BaseScrollSpeed = _baseSpeed;

            Vector3 movement = new Vector3(this._horizontalSpeed * Time.deltaTime, 0, 0);
            this.transform.Translate(movement);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GameManager.Instance._BaseScrollSpeed = - _verticalSpeed + _baseSpeed;
        }
        else
        {
            GameManager.Instance._BaseScrollSpeed = _baseSpeed;
        }

        // SHOOTING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _weapon.Fire();
        }
    }
}
