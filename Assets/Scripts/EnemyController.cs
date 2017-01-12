using UnityEngine;
using System.Collections;
using System;

public class EnemyController : ShipController
{
    public float _baseSpeed = 0.2f;
    public float _fireRate = 2f;
    public GameObject _upgradeDrop;

    private bool _hasFired = false;

    protected override void OnStart()
    {
        _owner = Owner.Enemy;
    }

    protected override void HandleMovement()
    {
        float movement = -1 * (GameManager.Instance._BaseScrollSpeed + _baseSpeed) * Time.deltaTime;
        Vector3 position = this.transform.position;
        position.y += movement;
        this.transform.position = position;

        if(this.transform.position.y - _colliderComponent.radius < -Camera.main.orthographicSize){
            Destroy(this.gameObject);
        }
    }

    protected override void HandleFiring()
    {
        if (_hasFired)
        {
            return;
        }

        _weaponComponent.Fire();
        _hasFired = true;
        Invoke("OnFiringTimerOver", _fireRate);
    }

    public override void DoDamage(float damage)
    {
        _currentHealthPoints = 0;
        Destroy(this.gameObject);

        if(UnityEngine.Random.Range(1, 20) < 6)
        {
            GameObject drop = Instantiate(_upgradeDrop);
            drop.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
        }
    }

    private void OnFiringTimerOver()
    {
        _hasFired = false;
    }
}
