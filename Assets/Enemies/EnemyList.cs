using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    [SerializeField] private int _initialEnemiesCount = 10;
    [SerializeField] private int _incrementalEnemies = 5;
    [SerializeField] private float _baseValue = 1.5f;
    [SerializeField] private GameObject _electronPrefab;
    [SerializeField] private GameObject _neutronPrefab;
    [SerializeField] private GameObject _protonPrefab;
    private List<Enemy> _enemies;

    private void Awake()
    {
        _enemies = new List<Enemy>();
        GameManager.Event_OnStartLevel += OnStartLevel;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnStartLevel -= OnStartLevel;
    }

    private void OnStartLevel(int level)
    {
        int enemiesToSpawn = _initialEnemiesCount + (_incrementalEnemies * (int)Mathf.Pow(_baseValue, level));
        int i = 0;
        while(enemiesToSpawn > 0)
        {
            if(enemiesToSpawn > 10)
            {
                SpawnRandomElectrons(Vector3.zero, 10, 0f, 359f, Vector3.right, 10f + (i * 10f), 20f + (i * 10f));
                i++;
                enemiesToSpawn -= 10;
            }
            else
            {
                SpawnRandomElectrons(Vector3.zero, enemiesToSpawn, 0f, 359f, Vector3.right, 10f + (i * 10f), 20f + (i * 10f));
                enemiesToSpawn = 0;
            }
        }

        foreach(var enemy in _enemies)
        {
            ElectronController ec = enemy as ElectronController;
            ec.GivesEnergy = true;
        }
    }


    public void Remove(Enemy enemyToRemove)
    {
        _enemies.Remove(enemyToRemove);
    }

    public void SpawnNeutron(Transform ElectronTransform, bool givesEnergy)
    {
        GameObject spawnedNeutron = Instantiate(_neutronPrefab, transform);
        spawnedNeutron.transform.SetPositionAndRotation(ElectronTransform.position, ElectronTransform.rotation);
        spawnedNeutron.transform.localScale = ElectronTransform.localScale;

        NeutronController neutronController = spawnedNeutron.GetComponent<NeutronController>();
        neutronController.GivesEnergy = givesEnergy;
        neutronController.Initialise(this);

        _enemies.Add(neutronController);

        GameManager.OnSpawnNeutron(spawnedNeutron.transform.position);
    }

    public void SpawnProton(Transform ElectronTransform, List<GameObject> charges)
    {
        GameObject spawnedProton = Instantiate(_protonPrefab, transform);
        spawnedProton.transform.SetPositionAndRotation(ElectronTransform.position, ElectronTransform.rotation);
        spawnedProton.transform.localScale = ElectronTransform.localScale;

        ProtonController proton = spawnedProton.GetComponent<ProtonController>();
        proton.Initialise(this);
        proton.AddCharges(charges);

        _enemies.Add(proton);

        GameManager.OnSpawnProton(spawnedProton.transform.position);
    }

    public void SpawnRandomElectrons(Vector3 origin, int count, float minAngle, float maxAngle, Vector3 axis, float minimumDistance, float maximumDistance)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 direction = Quaternion.Euler(0, 0, i * Random.Range(minAngle, maxAngle)) * axis;

            SpawnElectron(origin + (direction * Random.Range(minimumDistance, maximumDistance)));
        }
    }

    public void SpawnRandomElectrons(Vector3 origin, int count, float minAngle, float maxAngle, Vector3 axis)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 direction = Quaternion.Euler(0, 0, i * Random.Range(minAngle, maxAngle)) * axis;

            SpawnElectron(origin + (direction * Random.Range(10, 20)));
        }
    }

    private void SpawnElectron(Vector3 position)
    {
        GameObject spawnedEnemy = Instantiate(_electronPrefab, transform);
        spawnedEnemy.transform.position = position;
        spawnedEnemy.transform.localScale = spawnedEnemy.transform.localScale * Random.Range(0.7f, 1f);

        Enemy enemy = spawnedEnemy.GetComponent<Enemy>();
        enemy.Initialise(this);

        _enemies.Add(enemy);

        GameManager.OnSpawnElectron(spawnedEnemy.transform.position);
    }
}
