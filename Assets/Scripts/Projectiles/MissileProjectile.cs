using UnityEngine;
using System.Collections;

public class MissileProjectile : MonoBehaviour, IProjectile
{
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _damage;
	[SerializeField]
	private float _range;
	[SerializeField]
	private float _lockRange;

	private Rigidbody2D _rigidbody;

	private ETeam _team;
	private Vector2 _direction;

	private bool _isTartgetSet = false;
	private IShip _target;

	private Vector3 _previousPosition;
	private float _movedDistance;

	public ETeam Team { get { return _team; } }

	// Use this for initialization
	void Start ()
	{
		_rigidbody = this.GetComponent<Rigidbody2D> ();

		this.transform.up = _direction;
		_rigidbody.velocity = _direction * _speed;

		_previousPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		_movedDistance += (this.transform.position - _previousPosition).magnitude;
		_previousPosition = this.transform.position;

		if (_movedDistance >= _range) {
			Destroy (this.gameObject);
		}

		//*************************************

		if (_isTartgetSet) {
			MonoBehaviour targetMono = _target as MonoBehaviour;

			if (targetMono != null) {
				_direction = targetMono.transform.position - this.transform.position;
				_direction.Normalize ();

				this.transform.up = _direction;
				_rigidbody.velocity = _direction * _speed;
			}
		} else {
			_target = FindTarget ();

			if (_target != null) {
				_isTartgetSet = true;
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Border")) {
			Destroy (this.gameObject);
		} else if (other.CompareTag ("Ship")) {
			IShip ship = other.GetComponent<IShip> ();

			if (ship != null && ship.Team != _team) {
				ship.ApplyDamage (_damage);
				Destroy (this.gameObject);
			}
		}
	}

	public void Initialize (ETeam team, Vector2 direction)
	{
		_team = team;
		_direction = direction;
	}

	private IShip FindTarget ()
	{
		Collider2D[] hits = Physics2D.OverlapCircleAll (new Vector2 (this.transform.position.x, this.transform.position.y), _lockRange);

		IShip nearestTarget = null;
		float shortestDistance = float.MaxValue;

		foreach (Collider2D collider in hits) {
			if (collider.CompareTag ("Ship")) {
				IShip target = collider.GetComponent<IShip> ();

				if (target != null && target.Team != _team) {
					float distance = (collider.transform.position - this.transform.position).magnitude;

					if (distance < shortestDistance) {
						nearestTarget = target;
						shortestDistance = distance;
					}
				}
			}
		}

		return nearestTarget;
	}
}
