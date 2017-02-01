using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	[SerializeField]
	private GameObject[] _waves;

	public PlayerShip _PlayerShip { get; set; }

	public GameOverPanelView _GameOverPanelView { get; set; }

	public float _BaseScrollSpeed { get; set; }

	private List<IShip> _currentWave = new List<IShip> ();
	private int _score = 0;

	//=======================================================================

	void Awake ()
	{
		if (Instance == null) {
			Instance = this;
		} else {
			Destroy (this);
		}
	}

	// Use this for initialization
	void Start ()
	{
		SpawnWave ();

		InfoPanelView.Instance.SetScore (_score);
	}

	// Update is called once per frame
	void Update ()
	{
		if (_currentWave.Count == 0) {
			SpawnWave ();
		}
	}

	//=======================================================================

	public void OnShipDestroyed (IShip ship, bool destroyedByPlayer = false)
	{
		if (ship.Team == ETeam.Enemies) {
			_currentWave.Remove (ship);

			if (destroyedByPlayer) {
				_score += 1;
			}

			InfoPanelView.Instance.SetScore (_score);
		} else if (ship.Team == ETeam.Player) {
			Time.timeScale = 0;
			_GameOverPanelView.SetVisible (true);
			_GameOverPanelView.SetScore (_score);
		}
	}

	//=======================================================================

	private void SpawnWave ()
	{
		_currentWave.Clear ();
		int index = UnityEngine.Random.Range (0, _waves.Length);

		GameObject waveObj = Instantiate (_waves [index]);

		foreach (IShip ship in waveObj.GetComponentsInChildren<IShip>()) {
			_currentWave.Add (ship);
		}
	}
}
