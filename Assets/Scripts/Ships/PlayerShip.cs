using UnityEngine;
using System.Collections;

public class PlayerShip : MonoBehaviour, IShip
{
	public ETeam Team { get { return ETeam.Player; } }

	public Vector2 Direction { get { return Vector2.up; } }

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnDestroy ()
	{
		GameManager.Instance.OnShipDestroyed (this);
	}

	public void ApplyDamage (float damage)
	{
		throw new System.NotImplementedException ();
	}
}
