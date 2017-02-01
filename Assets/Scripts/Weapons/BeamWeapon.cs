using UnityEngine;
using System.Collections;

public class BeamWeapon : MonoBehaviour, IWeapon
{
	[SerializeField]
	private float _reloadTime;

	private IShip _ship;

	private int _level = 1;
	private bool _isReloading = false;
	private bool _isFiring = false;
	private bool _wasPreviouslyFiring = false;
	private GameObject _projectile;

	public EWeapon Type{ get { return EWeapon.Beam; } }

	public int Level{ get { return _level; } }

	// Use this for initialization
	void Start ()
	{
		_ship = this.GetComponent<IShip> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (_isFiring) {
			_wasPreviouslyFiring = true;
			_isFiring = false;
		} else if (_wasPreviouslyFiring && !_isFiring) {
			Debug.Log ("BeamWeapon firing over!");
			Destroy (_projectile);
			_isReloading = true;
			Invoke ("OnReloadTimeOver", _reloadTime);
		}
	}

	public void LevelUp ()
	{
		if (_level < 3) {
			_level++;
		}
	}

	public void Reset ()
	{
		_level = 1;
	}

	public bool Fire ()
	{
		Debug.Log ("BeamWeapon fired!");

		if (_isReloading) {
			return false;
		} else if (_wasPreviouslyFiring) {
			_isFiring = true;
			return true;
		}

		switch (_level) {
		case 1:
			Vector3 position = this.transform.position + new Vector3 (_ship.Direction.x * 3f, _ship.Direction.y * 3f);
			_projectile = Instantiate (Resources.Load ("Prefabs/Projectiles/BeamProjectile"), position, Quaternion.identity) as GameObject;
			IProjectile projectileScript = _projectile.GetComponent<IProjectile> ();
			projectileScript.Initialize (_ship.Team, _ship.Direction);

			_isFiring = true;
			return true;
		case 2:
			break;
		case 3:
			break;
		}

		return false;
	}

	private void OnReloadTimeOver ()
	{
		_isReloading = false;
	}

	public void DropUpgrade ()
	{
		Instantiate (Resources.Load ("Prefabs/WeaponUpgrades/BeamWeaponUpgrade"), this.transform.position, Quaternion.identity);
	}
}
