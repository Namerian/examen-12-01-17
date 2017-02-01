using UnityEngine;
using System.Collections;

public class BlasterWeapon : MonoBehaviour, IWeapon
{
	[SerializeField]
	private float _reloadTime;

	private IShip _ship;

	private int _level = 1;
	private bool _isReloading = false;

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
		switch (_level) {
		case 1:
			GameObject projectileObj = Instantiate (Resources.Load ("Prefabs/Projectiles/BlasterProjectile"), this.transform.position, Quaternion.identity) as GameObject;
			IProjectile projectileScript = projectileObj.GetComponent<IProjectile> ();
			projectileScript.Initialize (_ship.Team, _ship.Direction);

			_isReloading = true;
			Invoke ("OnReloadTimerOver", _reloadTime);
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
}
