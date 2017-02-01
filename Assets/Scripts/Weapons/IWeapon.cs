using UnityEngine;
using System.Collections;

public interface IWeapon
{
	EWeapon Type{ get; }

	int Level{ get; }

	void LevelUp ();

	void Reset ();

	bool Fire ();

	void DropUpgrade ();
}
