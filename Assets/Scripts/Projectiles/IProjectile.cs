using UnityEngine;
using System.Collections;

public interface IProjectile
{
	ETeam Team{ get; }

	void Initialize (ETeam team, Vector2 direction);
}
