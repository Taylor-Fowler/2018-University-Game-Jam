using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _spawnNeutronSound;
    // https://freesound.org/people/themusicalnomad/sounds/253887/
    // 
    [SerializeField] private AudioSource _spawnProtonSound;
    // https://freesound.org/people/waterboy920/sounds/191012/

    [SerializeField] private AudioSource _gainEnergySound;
    // https://freesound.org/people/Wakerone/sounds/398486/
    [SerializeField] private AudioSource _nextLevelSound;
    // https://freesound.org/people/newagesoup/sounds/339343/

    [SerializeField] private AudioSource _backgroundAudio;
    // https://freesound.org/people/ShiftKun/sounds/435782/
    [SerializeField] private AudioSource _loseEnergySound;
    // https://freesound.org/people/themusicalnomad/sounds/253886/

    [SerializeField] private AudioSource _introLoop;
    //https://freesound.org/people/DragonTrance/sounds/380628/

    [SerializeField] private AudioSource _gameOverSound;
    // https://freesound.org/people/HerbertBoland/sounds/128551/

    private void Awake()
    {
        GameManager.Event_OnSpawnNeutron += OnSpawnNeutron;
        GameManager.Event_OnSpawnProton += OnSpawnProton;
        GameManager.Event_OnGainEnergy += OnGainEnergy;
        GameManager.Event_OnLoseEnergy += OnLoseEnergy;
        GameManager.Event_OnStartLevel += OnStartLevel;
        GameManager.Event_OnGameOver += OnGameOver;
    }

    private void OnDestroy()
    {
        GameManager.Event_OnSpawnNeutron -= OnSpawnNeutron;
        GameManager.Event_OnSpawnProton -= OnSpawnProton;
        GameManager.Event_OnGainEnergy -= OnGainEnergy;
        GameManager.Event_OnLoseEnergy -= OnLoseEnergy;
        GameManager.Event_OnStartLevel -= OnStartLevel;
        GameManager.Event_OnGameOver -= OnGameOver;
    }

    private void OnSpawnNeutron(int count, Vector3 position)
    {
        _spawnNeutronSound.Play();
    }

    private void OnSpawnProton(int count, Vector3 position)
    {
        _spawnProtonSound.Play();
    }

    private void OnGainEnergy(int total, int energyGained)
    {
        if(energyGained != 0)
        {
            _gainEnergySound.Play();
        }
    }

    private void OnLoseEnergy(int total, int energyGained)
    {
        if(total != 0)
        {
            _loseEnergySound.Play();
        }
    }

    private void OnStartLevel(int level)
    {
        if(level == 1)
        {
            _introLoop.Stop();
            _backgroundAudio.Play();
        }
        StartCoroutine(PlayLevelSound(level));
    }

    private void OnGameOver()
    {
        _backgroundAudio.Stop();
        _gameOverSound.Play();
    }

    private IEnumerator PlayLevelSound(int numberOfTimes)
    {
        while(numberOfTimes > 0)
        {
            _nextLevelSound.Play();
            yield return new WaitForSeconds(0.5f);

            numberOfTimes--;
        }
    }
}
