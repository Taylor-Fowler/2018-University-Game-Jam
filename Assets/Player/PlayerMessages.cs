using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessages : MonoBehaviour
{
    public string[] EmpoweringMessages;
    public string[] SpawnProtonMessages;
    public string[] SpawnNeutronMessages;
    public float MinimumTimeBetweenMessages = 5f;

    [SerializeField] private MessageBubble _messageBubble;
    private int _lastEmpoweringIndex = 0;
    private int _lastProtonIndex = 0;
    private int _lastNeutronIndex = 0;
    private float _lastMessage;

    private void Awake()
    {
        _lastMessage = -MinimumTimeBetweenMessages;
        GameManager.Event_OnSpawnProton += OnSpawnProton;
        GameManager.Event_OnSpawnNeutron += OnSpawnNeutron;
    }

    private void Update()
    {
        if(Time.time > _lastMessage + (2 * MinimumTimeBetweenMessages))
        {
            PlayEmpoweringMessage();
        }
    }

    private void OnDestroy()
    {
        GameManager.Event_OnSpawnProton -= OnSpawnProton;
        GameManager.Event_OnSpawnNeutron -= OnSpawnNeutron;
    }

    private void OnSpawnProton(int count, Vector3 position)
    {
        PlayProtonMessage();
    }

    private void OnSpawnNeutron(int count, Vector3 position)
    {
        PlayNeutronMessage();
    }

    private void PlayProtonMessage()
    {
        if (!CanPlay())
        {
            return;
        }

        if (_messageBubble != null)
        {
            _lastProtonIndex = DifferentToLast(SpawnProtonMessages.Length, _lastProtonIndex);
            _lastMessage = Time.time;
            _messageBubble.DisplayMessage(SpawnProtonMessages[_lastProtonIndex], transform);
        }
    }

    private void PlayNeutronMessage()
    {
        if (!CanPlay())
        {
            return;
        }

        if (_messageBubble != null)
        {
            _lastNeutronIndex = DifferentToLast(SpawnNeutronMessages.Length, _lastNeutronIndex);
            _lastMessage = Time.time;
            _messageBubble.DisplayMessage(SpawnNeutronMessages[_lastNeutronIndex], transform);
        }
    }

    private void PlayEmpoweringMessage()
    {  
        if(!CanPlay())
        {
            return;
        }

        if(_messageBubble != null)
        {
            _lastEmpoweringIndex = DifferentToLast(EmpoweringMessages.Length, _lastEmpoweringIndex);
            _lastMessage = Time.time;
            _messageBubble.DisplayMessage(EmpoweringMessages[_lastEmpoweringIndex], transform);
        }
    }

    private int DifferentToLast(int maximumExclusive, int last)
    {
        int index = Random.Range(0, maximumExclusive);

        while(index == last)
        {
            index = Random.Range(0, maximumExclusive);
        }

        return index;
    }


    private bool CanPlay()
    {
        return Time.time >= _lastMessage + MinimumTimeBetweenMessages;
    }
}
