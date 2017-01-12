using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController _PlayerController { get; set; }
    public GameOverPanelView _GameOverPanelView { get; set; }
    public float _BaseScrollSpeed { get; set; }

    private List<ShipController> _currentWave = new List<ShipController>();
    private int _score = 0;

    //=======================================================================

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        SpawnWave();

        InfoPanelView.Instance.SetScore(_score);
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentWave.Count == 0)
        {
            SpawnWave();
        }
    }

    //=======================================================================

    public void OnShipDestroyed(ShipController ship)
    {
        if (ship.Owner == Owner.Enemy)
        {
            _currentWave.Remove(ship);

            if (ship.HealthPoints == 0)
            {
                _score += 1;
            }


            InfoPanelView.Instance.SetScore(_score);
        }
        else if (ship.Owner == Owner.Player)
        {
            Time.timeScale = 0;
            _GameOverPanelView.SetVisible(true);
            _GameOverPanelView.SetScore(_score);
        }
    }

    //=======================================================================

    private void SpawnWave()
    {
        _currentWave.Clear();
        int index = UnityEngine.Random.Range(1, 1);

        switch (index)
        {
            case 1:
                _currentWave.Add(SpawnEnemy(-1, 4, Weapon.Laser));
                _currentWave.Add(SpawnEnemy(1, 4, Weapon.Laser));
                _currentWave.Add(SpawnEnemy(3, 4, Weapon.Laser));
                _currentWave.Add(SpawnEnemy(0, 5, Weapon.Laser));
                _currentWave.Add(SpawnEnemy(2, 5, Weapon.Laser));
                break;
        }
    }

    private ShipController SpawnEnemy(float xPos, float yPos, Weapon type)
    {
        GameObject enemyObj;

        if (type == Weapon.Beam)
        {
            enemyObj = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/BeamEnemy"), this.transform);
        }
        else if (type == Weapon.Missile)
        {
            enemyObj = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/MissileEnemy"), this.transform);
        }
        else
        {
            enemyObj = (GameObject)Instantiate(Resources.Load("Prefabs/Enemies/LaserEnemy"), this.transform);
        }

        Vector3 position = enemyObj.transform.position;
        position.x = xPos;
        position.y = yPos;
        enemyObj.transform.position = position;

        return enemyObj.GetComponent<ShipController>();
    }

    //=======================================================================

}
