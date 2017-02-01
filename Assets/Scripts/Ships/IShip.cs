using UnityEngine;
using System.Collections;

public interface IShip
{
	ETeam Team{ get; }

	Vector2 Direction{ get; }

	void ApplyDamage (float damage);
}
