using UnityEngine;
using System.Collections;

public enum Weapon
{
    Laser,
    Beam,
    Missile
}

public abstract class ShipWeapon : MonoBehaviour
{
    protected ShipController _shipController;

    private Weapon _type;
    private int _level = 1;
    private bool _isReloading = false;

    // Use this for initialization
    void Start()
    {
        _shipController = this.GetComponent<ShipController>();

        OnStart();
    }

    // Update is called once per frame
    /*void Update()
    {

    }*/

    //============================================================

    public Weapon Type
    {
        get { return _type; }
        protected set { _type = value; }
    }

    public int Level
    {
        get { return _level; }
    }

    public Owner Owner
    {
        get { return _shipController.Owner; }
    }

    //============================================================

    /*public void Initialize(Owner owner)
    {
        _owner = owner;
    }*/

    public void IncreaseLevel()
    {
        _level++;

        if (_level > 3)
        {
            _level = 3;
        }
    }

    public void Fire()
    {
        if (_isReloading)
        {
            return;
        }

        FireWeapon();

        _isReloading = true;
        Invoke("OnReloadingDone", GetReloadTime());
    }

    //============================================================

    protected abstract void OnStart();

    public abstract float GetReloadTime();

    protected abstract void FireWeapon();

    private void OnReloadingDone()
    {
        _isReloading = false;
    }
}
