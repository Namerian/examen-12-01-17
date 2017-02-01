using UnityEngine;
using System.Collections;
using System;

public class LaserProjectile : Projectile
{
    

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movement = this.transform.up * WeaponSettings.Instance._laserSpeed * Time.deltaTime;

        if(this._owner == Owner.Enemy)
        {
            movement *= -1;
        }

        this.transform.Translate(movement);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (this._owner == Owner.Player && collider.gameObject.tag == "Enemy")
        {
            EnemyController enemyScript = collider.gameObject.GetComponent<EnemyController>();

            enemyScript.DoDamage(WeaponSettings.Instance._laserDamage);

            Destroy(this.gameObject);
        }
        else if (this.Owner == Owner.Enemy && collider.gameObject.tag == "Player")
        {
            PlayerController playerScript = collider.gameObject.GetComponent<PlayerController>();

            playerScript.DoDamage(WeaponSettings.Instance._laserDamage);

            Destroy(this.gameObject);
        }
    }

    public override void Initialize(Owner owner, Vector3 position, Vector3 eulerAngles)
    {
        _owner = owner;

        this.transform.position = position;
        this.transform.eulerAngles = eulerAngles;
    }

    public override Weapon GetWeaponType()
    {
        return Weapon.Laser;
    }
}
