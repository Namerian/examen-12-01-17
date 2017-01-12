using UnityEngine;
using System.Collections;


public class PlayerController : ShipController
{
    public float _baseSpeed = 1;
    public float _verticalSpeed = 1;
    public float _horizontalSpeed = 2;
    public int _lives = 3;
    public float _healthPoints = 5;

    private Behaviour _haloComponent;

    private int _currentLives;
    //private float _currentHealthPoints;
    private bool _isInvincible = false;

    protected override void OnStart()
    {
        GameManager.Instance._PlayerController = this;

        _owner = Owner.Player;

        _haloComponent = (Behaviour)this.GetComponent("Halo");
        _haloComponent.enabled = false;

        _currentLives = _lives;
        _currentHealthPoints = _healthPoints;

        InfoPanelView.Instance.SetLives(_currentLives);
        InfoPanelView.Instance.SetWeapon(_weaponComponent.Type);
        InfoPanelView.Instance.SetWeaponLevel(_weaponComponent.Level);
    }

    protected override void HandleMovement()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 movement = new Vector3(-this._horizontalSpeed * Time.deltaTime, 0, 0);
            this.transform.Translate(movement);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Vector3 movement = new Vector3(this._horizontalSpeed * Time.deltaTime, 0, 0);
            this.transform.Translate(movement);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            GameManager.Instance._BaseScrollSpeed = _verticalSpeed + _baseSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            GameManager.Instance._BaseScrollSpeed = -_verticalSpeed + _baseSpeed;
        }
        else
        {
            GameManager.Instance._BaseScrollSpeed = _baseSpeed;
        }
    }

    protected override void HandleFiring()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _weaponComponent.Fire();
        }
    }

    //========================================================================

    public override void DoDamage(float damage)
    {
        if (_isInvincible)
        {
            return;
        }

        _currentHealthPoints -= damage;

        if(_currentHealthPoints <= 0)
        {
            _currentLives--;
            InfoPanelView.Instance.SetLives(_currentLives);

            if (_currentLives == 0)
            {
                GameManager.Instance.OnShipDestroyed(this);
            }
            else
            {
                _currentHealthPoints = _healthPoints;
                _isInvincible = true;
                _haloComponent.enabled = true;
                Invoke("OnInvincibilityOver", 0.5f);
            }
        } 
    }

    public void UpgradeWeapon(Weapon type)
    {
        if(_weaponComponent.Type == type)
        {
            _weaponComponent.IncreaseLevel();
            InfoPanelView.Instance.SetWeaponLevel(_weaponComponent.Level);
        }
    }

    //===============================================================

    private void OnInvincibilityOver()
    {
        _isInvincible = false;
        _haloComponent.enabled = false;
    }
}
