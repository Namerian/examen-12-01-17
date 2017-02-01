using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoPanelView : MonoBehaviour
{
	public static InfoPanelView Instance { get; private set; }

	private Text _scoreText;
	private Text _livesText;
	private Text _weaponText;
	private Text _weaponLevelText;

	void Awake ()
	{
		Instance = this;


	}

	void Start ()
	{
		_scoreText = this.transform.FindChild ("ScoreText").GetComponent<Text> ();
		_livesText = this.transform.FindChild ("LivesText").GetComponent<Text> ();
		_weaponText = this.transform.FindChild ("WeaponText").GetComponent<Text> ();
		_weaponLevelText = this.transform.FindChild ("WeaponLevelText").GetComponent<Text> ();
	}

	public void SetScore (int score)
	{
		_scoreText.text = "Score: " + score;
	}

	public void SetLives (int lives)
	{
		_livesText.text = "Lives: " + lives;
	}

	public void SetWeapon (EWeapon weapon)
	{
		string weaponName = "Blaster";

		if (weapon == EWeapon.Beam) {
			weaponName = "Beam";
		} else if (weapon == EWeapon.Missile) {
			weaponName = "Missile";
		}

		_weaponText.text = "Weapon: " + weaponName;
	}

	public void SetWeaponLevel (int level)
	{
		_weaponLevelText.text = "Level: " + level;
	}
}
