using UnityEngine;
using System.Collections;

public class SimpleEnemy : MonoBehaviour, IShip
{
	[SerializeField]
	private float _baseSpeed = 0.2f;
	[SerializeField]
	private float _maxHealthPoints = 5;

	private Rigidbody2D _rigidbody;

	private float _healthPoints;
	private IWeapon _weapon;

	//=====================================================================
	//
	//=====================================================================

	public ETeam Team { get { return ETeam.Enemies; } }

	public Vector2 Direction { get { return -Vector2.up; } }

	//=====================================================================
	//
	//=====================================================================

	// Use this for initialization
	void Start ()
	{
		_rigidbody = this.GetComponent<Rigidbody2D> ();

		_healthPoints = _maxHealthPoints;
		_weapon = this.GetComponent<IWeapon> ();

		Vector2 movement = this.transform.up * (GameManager.Instance._BaseScrollSpeed + _baseSpeed);
		//Vector2 movement = -Vector2.up * (GameManager.Instance._BaseScrollSpeed + _baseSpeed);
		_rigidbody.velocity = movement;
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector2 movement = this.transform.up * (GameManager.Instance._BaseScrollSpeed + _baseSpeed);
		//Vector2 movement = -Vector2.up * (GameManager.Instance._BaseScrollSpeed /*+ _baseSpeed*/);
		_rigidbody.velocity = movement;

		if (_weapon.Fire ()) {
			
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.collider.CompareTag ("Border")) {
			GameManager.Instance.OnShipDestroyed (this, false);
			Destroy (this.gameObject);
		} else if (collision.collider.CompareTag ("Ship")) {
			IShip ship = collision.collider.GetComponent<IShip> ();

			if (ship != null && ship.Team != Team) {
				ship.ApplyDamage (2f);
				GameManager.Instance.OnShipDestroyed (this, true);
				Destroy (this.gameObject);
			}
		}
	}

	//=====================================================================
	//
	//=====================================================================

	public void ApplyDamage (float damage)
	{
		_healthPoints -= damage;

		if (_healthPoints <= 0) {
			GameManager.Instance.OnShipDestroyed (this, true);

			if (UnityEngine.Random.Range (1, 20) < 6) {
				_weapon.DropUpgrade ();
			}

			Destroy (this.gameObject);
		} 
	}
}
