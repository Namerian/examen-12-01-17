using UnityEngine;
using System.Collections;

public class BlasterWeapon : MonoBehaviour, IWeapon
{
	[SerializeField]
	private float _reloadTime;

	private IShip _ship;

	private int _level = 1;
	private bool _isReloading = false;

	public EWeapon Type{ get { return EWeapon.Blaster; } }

	public int Level{ get { return _level; } }

	// Use this for initialization
	void Start ()
	{
		_ship = this.GetComponent<IShip> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
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
		if (_isReloading) {
			return false;
		}

		GameObject projectileObj;
		IProjectile projectileScript;

		switch (_level) {
		case 1:
			projectileObj = Instantiate (Resources.Load ("Prefabs/Projectiles/BlasterProjectile"), this.transform.position, Quaternion.identity) as GameObject;
			projectileScript = projectileObj.GetComponent<IProjectile> ();
			projectileScript.Initialize (_ship.Team, _ship.Direction);

			_isReloading = true;
			Invoke ("OnReloadTimeOver", _reloadTime);
			return true;
		case 2:
			projectileObj = Instantiate (Resources.Load ("Prefabs/Projectiles/BigBlasterProjectile"), this.transform.position, Quaternion.identity) as GameObject;
			projectileScript = projectileObj.GetComponent<IProjectile> ();
			projectileScript.Initialize (_ship.Team, _ship.Direction);

			_isReloading = true;
			Invoke ("OnReloadTimeOver", _reloadTime);
			return true;
		case 3:
			projectileObj = Instantiate (Resources.Load ("Prefabs/Projectiles/BigBlasterProjectile"), this.transform.position, Quaternion.identity) as GameObject;
			projectileScript = projectileObj.GetComponent<IProjectile> ();
			projectileScript.Initialize (_ship.Team, _ship.Direction);

			_isReloading = true;
			Invoke ("OnReloadTimeOver", 0.1f);
			return true;
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
		Instantiate (Resources.Load ("Prefabs/WeaponUpgrades/BlasterWeaponUpgrade"), this.transform.position, Quaternion.identity);
	}
}
