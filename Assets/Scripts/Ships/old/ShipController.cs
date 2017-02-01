using UnityEngine;
using System.Collections;

/*public enum Owner
{
    Player,
    Enemy
}

public abstract class ShipController : MonoBehaviour
{
    protected Owner _owner;
    protected float _currentHealthPoints = 1;

    protected ShipWeapon _weaponComponent;
    protected CircleCollider2D _colliderComponent;

    // Use this for initialization
    void Start()
    {
        _weaponComponent = this.GetComponent<ShipWeapon>();
        _colliderComponent = this.GetComponent<CircleCollider2D>();

        OnStart();
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();

        HandleFiring();
    }

    void OnDestroy()
    {
        GameManager.Instance.OnShipDestroyed(this);
    }

    protected abstract void OnStart();

    protected abstract void HandleMovement();

    protected abstract void HandleFiring();

    public abstract void DoDamage(float damage);

    public Owner Owner { get { return _owner; } }

    public float HealthPoints { get { return _currentHealthPoints; } }
}*/
