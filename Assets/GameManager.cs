using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void SpawnElectron(int count, Vector3 position);
    public static SpawnElectron Event_OnSpawnElectron;

    public delegate void DestroyElectron(int count, Vector3 position);
    public static DestroyElectron Event_OnDestroyElectron;

    public delegate void SpawnNeutron(int count, Vector3 position);
    public static SpawnNeutron Event_OnSpawnNeutron;

    public delegate void DestroyNeutron(int count, Vector3 position);
    public static DestroyNeutron Event_OnDestroyNeutron;

    public delegate void SpawnProton(int count, Vector3 position);
    public static SpawnProton Event_OnSpawnProton;

    public delegate void DestroyProton(int count, Vector3 position);
    public static DestroyProton Event_OnDestroyProton;

    public delegate void StartLevel(int level);
    public static StartLevel Event_OnStartLevel;

    public delegate void GainEnergy(int total, int energyGained);
    public static GainEnergy Event_OnGainEnergy;

    public delegate void LoseEnergy(int total, int energyLost);
    public static LoseEnergy Event_OnLoseEnergy;

    public delegate void GameOver();
    public static GameOver Event_OnGameOver;

    private static bool _gameOver;
    private static int _electronCount;
    private static int _neutronCount;
    private static int _protonCount;
    private static int _energyLevel;

    public static bool GameStarted;
    private static int _level;
    private static UpgradeUI _staticUpgrade;
    [SerializeField] private UpgradeUI _upgradeUI;


    private void Awake()
    {
        _level = 1;
        _electronCount = 0;
        _neutronCount = 0;
        _protonCount = 0;
        _energyLevel = 100;
        _gameOver = false;
    }

    private void Start()
    {
        _upgradeUI.Initialise(ContinueToNextLevel);
        _staticUpgrade = _upgradeUI;
    }

    public void StartGame()
    {
        GameStarted = true;
        OnStartLevel();
        OnGainEnergy();
    }

    private void ContinueToNextLevel()
    {
        _level++;
        OnStartLevel();
    }

    private static void OnStartLevel()
    {
        if(Event_OnStartLevel != null)
        {
            Event_OnStartLevel(_level);
        }
    }

    public static void OnSpawnElectron(Vector3 position)
    {
        _electronCount++;
        if(Event_OnSpawnElectron != null)
        {
            Event_OnSpawnElectron(_electronCount, position);
        }
    }

    public static void OnSpawnNeutron(Vector3 position)
    {
        _neutronCount++;
        if (Event_OnSpawnNeutron != null)
        {
            Event_OnSpawnNeutron(_neutronCount, position);
        }
    }

    public static void OnSpawnProton(Vector3 position)
    {
        _protonCount++;
        if (Event_OnSpawnProton != null)
        {
            Event_OnSpawnProton(_protonCount, position);
        }
    }

    public static void OnDestroyElectron(Vector3 position)
    {
        _electronCount--;
        if(Event_OnDestroyElectron != null)
        {
            Event_OnDestroyElectron(_electronCount, position);
        }

        ShowUpgrades();
    }

    public static void OnDestroyNeutron(Vector3 position)
    {
        _neutronCount--;
        if (Event_OnDestroyNeutron != null)
        {
            Event_OnDestroyNeutron(_neutronCount, position);
        }

        ShowUpgrades();
    }

    public static void OnDestroyProton(Vector3 position)
    {
        _protonCount--;
        if (Event_OnDestroyProton != null)
        {
            Event_OnDestroyProton(_protonCount, position);
        }
    }

    public static void OnGainEnergy()
    {
        int gained = 2;
        _energyLevel = gained + _energyLevel;

        if (_energyLevel > 100)
        {
            gained -= (_energyLevel - 100);
            _energyLevel = 100;
        }

        if(Event_OnGainEnergy != null)
        {
            Event_OnGainEnergy(_energyLevel, gained);
        }
    }

    public static void OnLoseEnergy()
    {
        int lost = 10;
        _energyLevel = _energyLevel - lost;

        if(_energyLevel < 0)
        {
            lost += _energyLevel;
            _energyLevel = 0;
        }

        if(Event_OnLoseEnergy != null)
        {
            Event_OnLoseEnergy(_energyLevel, lost);
        }

        if(_energyLevel == 0)
        {
            OnGameOver();
        }
    }
    
    private static void ShowUpgrades()
    {
        if (_electronCount == 0 && _neutronCount == 0 && _protonCount == 0 && !_gameOver)
        {
            _staticUpgrade.Show();
        }
    }

    private static void OnGameOver()
    {
        _gameOver = true;
        GameStarted = false;
        if(Event_OnGameOver != null)
        {
            Event_OnGameOver();
        }
    }

}
