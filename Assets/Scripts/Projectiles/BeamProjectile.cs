using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeamProjectile : MonoBehaviour, IProjectile
{
	[SerializeField]
	private float _damagePerSecond = 0.5f;

	private Rigidbody2D _rigidbody;

	private ETeam _team;
	private Vector2 _direction;
	private List<IShip> _targets = new List<IShip> ();
	private bool _isAlive;

	public ETeam Team { get { return _team; } }

	// Use this for initialization
	void Start ()
	{
		_rigidbody = this.GetComponent<Rigidbody2D> ();

		_isAlive = true;

		Invoke ("InflictDamage", 1f);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDestroy ()
	{
		_isAlive = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Ship")) {
			IShip ship = other.GetComponent<IShip> ();

			if (ship != null && ship.Team != _team && !_targets.Contains (ship)) {
				_targets.Add (ship);
			}
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (other.CompareTag ("Ship")) {
			IShip ship = other.GetComponent<IShip> ();

			if (ship != null && ship.Team != _team && !_targets.Contains (ship)) {
				_targets.Add (ship);
			}
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.CompareTag ("Ship")) {
			IShip ship = other.GetComponent<IShip> ();

			if (ship != null && ship.Team != _team) {
				_targets.Remove (ship);
			}
		}
	}

	public void Initialize (ETeam team, Vector2 direction)
	{
		_team = team;
		_direction = direction;
	}

	private void InflictDamage ()
	{
		if (!_isAlive) {
			return;
		}

		foreach (IShip target in _targets) {
			if (target != null) {
				target.ApplyDamage (_damagePerSecond);
			}
		}

		Invoke ("InflictDamage", 1f);
	}
}
