using UnityEngine;
using System.Collections;

public interface IWeapon
{
	int Level{ get; }

	void LevelUp ();

	void Reset ();

	bool Fire ();
}
