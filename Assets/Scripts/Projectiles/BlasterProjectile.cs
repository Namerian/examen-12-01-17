using UnityEngine;
using System.Collections;

public class BlasterProjectile : MonoBehaviour, IProjectile
{
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _damage;

	private Rigidbody2D _rigidbody;

	private ETeam _team;
	private Vector2 _direction;

	public ETeam Team { get { return _team; } }

	// Use this for initialization
	void Start ()
	{
		_rigidbody = this.GetComponent<Rigidbody2D> ();

		this.transform.up = _direction;
		_rigidbody.velocity = _direction * _speed;
	}
	
	// Update is called once per frame
	void Update ()
	{
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
}
