using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour, IShip
{
	[SerializeField]
	private float _baseSpeed = 1;
	[SerializeField]
	private float _verticalSpeed = 1;
	[SerializeField]
	private float _horizontalSpeed = 2;
	[SerializeField]
	private int _maxLives = 3;
	[SerializeField]
	private float _maxHealthPoints = 5;
	[SerializeField]
	private EWeapon _startWeapon = EWeapon.Blaster;

	private Behaviour _haloComponent;
	private Rigidbody2D _rigidbody;

	private int _lives;
	private float _healthPoints;
	private bool _isInvincible;

	private IWeapon _blasterWeapon;
	private IWeapon _beamWeapon;
	private IWeapon _missileWeapon;
	private IWeapon _currentWeapon;

	//=====================================================================
	//
	//=====================================================================

	public ETeam Team { get { return ETeam.Player; } }

	public Vector2 Direction { get { return Vector2.up; } }

	//=====================================================================
	//
	//=====================================================================

	// Use this for initialization
	void Start ()
	{
		_haloComponent = (Behaviour)this.GetComponent ("Halo");
		_haloComponent.enabled = false;

		_rigidbody = this.GetComponent<Rigidbody2D> ();

		_lives = _maxLives;
		_healthPoints = _maxHealthPoints;

		foreach (IWeapon weapon in this.GetComponents<IWeapon>()) {
			switch (weapon.Type) {
			case EWeapon.Blaster:
				_blasterWeapon = weapon;
				break;
			case EWeapon.Beam:
				_beamWeapon = weapon;
				break;
			case EWeapon.Missile:
				_missileWeapon = weapon;
				break;
			}
		}

		switch (_startWeapon) {
		case EWeapon.Blaster:
			_currentWeapon = _blasterWeapon;
			break;
		case EWeapon.Beam:
			_currentWeapon = _beamWeapon;
			break;
		case EWeapon.Missile:
			_currentWeapon = _missileWeapon;
			break;
		}
		InfoPanelView.Instance.SetWeapon (_currentWeapon.Type);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//************************************************
		// vertical movement
		if (Input.GetKey (KeyCode.Q)) {
			Vector3 movement = new Vector3 (-this._horizontalSpeed, 0, 0);
			_rigidbody.velocity = movement;
		} else if (Input.GetKey (KeyCode.D)) {
			Vector3 movement = new Vector3 (this._horizontalSpeed, 0, 0);
			_rigidbody.velocity = movement;
		} else {
			_rigidbody.velocity = Vector2.zero;
		}

		//************************************************
		// horizontal movement
		if (Input.GetKey (KeyCode.Z)) {
			GameManager.Instance._BaseScrollSpeed = _verticalSpeed + _baseSpeed;
		} else if (Input.GetKey (KeyCode.S)) {
			GameManager.Instance._BaseScrollSpeed = -_verticalSpeed + _baseSpeed;
		} else {
			GameManager.Instance._BaseScrollSpeed = _baseSpeed;
		}

		//************************************************
		// firing
		if (Input.GetKey (KeyCode.Space)) {
			_currentWeapon.Fire ();
		}
	}

	void OnDestroy ()
	{
		GameManager.Instance.OnShipDestroyed (this);
	}

	void OnTriggerEnter2D (Collider2D collider)
	{
		if (collider.gameObject.tag == "WeaponUpgrade") {
			Debug.Log ("Player collided with upgrade!");
			WeaponUpgrade upgrade = collider.GetComponent<WeaponUpgrade> ();

			if (upgrade != null) {
				if (upgrade.Type != _currentWeapon.Type) {
					switch (_currentWeapon.Type) {
					case EWeapon.Blaster:
						_currentWeapon = _blasterWeapon;

						break;
					case EWeapon.Beam:
						_currentWeapon = _beamWeapon;
						break;
					case EWeapon.Missile:
						_currentWeapon = _missileWeapon;
						break;
					}

					_currentWeapon.Reset ();
					InfoPanelView.Instance.SetWeapon (_currentWeapon.Type);
				} else {
					_currentWeapon.LevelUp ();
					InfoPanelView.Instance.SetWeaponLevel (_currentWeapon.Level);
				}

				Destroy (collider.gameObject);
			}
		}
	}

	//=====================================================================
	//
	//=====================================================================

	public void ApplyDamage (float damage)
	{
		if (_isInvincible) {
			return;
		}

		_healthPoints -= damage;

		if (_healthPoints <= 0) {
			_lives--;
			InfoPanelView.Instance.SetLives (_lives);

			if (_lives == 0) {
				GameManager.Instance.OnShipDestroyed (this);
			} else {
				_healthPoints = _maxHealthPoints;
				_isInvincible = true;
				_haloComponent.enabled = true;
				Invoke ("OnInvincibilityOver", 0.5f);
			}
		} 
	}

	private void OnInvincibilityOver ()
	{
		_isInvincible = false;
		_haloComponent.enabled = false;
	}
}
