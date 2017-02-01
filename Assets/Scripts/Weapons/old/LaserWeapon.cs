using UnityEngine;
using System.Collections;
using System;

/*public class LaserWeapon : ShipWeapon
{
    private CircleCollider2D _collider;

    protected override void OnStart()
    {
        this.Type = Weapon.Laser;

        _collider = GetComponent<CircleCollider2D>();
    }

    public override float GetReloadTime()
    {
        if (this.Level == 3)
        {
            return WeaponSettings.Instance._laserBetterReloadTime;
        }

        return WeaponSettings.Instance._laserBaseReloadTime;
    }

    protected override void FireWeapon()
    {
        GameObject projectileObj = (GameObject)Instantiate(Resources.Load("Prefabs/LaserProjectile"));
        Projectile projectileScript = projectileObj.GetComponent<Projectile>();

        Vector3 position = this.transform.position;

        if(this.Owner == Owner.Player)
        {
            position += this.transform.up * (_collider.radius * 2);
        }
        else if(this.Owner == Owner.Enemy)
        {
            position -= this.transform.up * (_collider.radius * 2);
        }
        

        projectileScript.Initialize(this.Owner, position, this.transform.eulerAngles);
    }
}*/
