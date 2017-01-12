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
        Vector3 movement = this.transform.up * 2 * Time.deltaTime;
        this.transform.Translate(movement);
    }

    public override void Initialize(int level, Owner owner, Vector3 position, Vector3 eulerAngles)
    {
        _owner = owner;

        this.transform.position = position;
        this.transform.eulerAngles = eulerAngles;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

    }
}
