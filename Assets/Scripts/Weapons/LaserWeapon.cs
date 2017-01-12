using UnityEngine;
using System.Collections;
using System;

public class LaserWeapon : ShipWeapon
{
    private const float _reloadTime = 1;
    private const float _level3reloadTime = 0.5f;

    private CircleCollider2D _collider;

    void Awake()
    {
        this.Type = Weapon.Laser;

        _collider = GetComponent<CircleCollider2D>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override float GetReloadTime()
    {
        if(this.Level == 3)
        {
            return _level3reloadTime;
        }

        return _reloadTime;
    }

    protected override void FireWeapon()
    {
        GameObject projectileObj = (GameObject)Instantiate(Resources.Load("Prefabs/LaserProjectile"));
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();

        Vector3 position = this.transform.position;
        position += this.transform.up * _collider.radius;

        projectileScript.Initialize(this.Level, this.Owner, position, this.transform.eulerAngles);
    }
}
