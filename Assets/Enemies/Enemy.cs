using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    protected EnemyList _owner;

    public void Initialise(EnemyList enemyList)
    {
        _owner = enemyList;
        GameManager.Event_OnGameOver += OnGameOver;
    }

    public void OnDestroy()
    {
        _owner.Remove(this);
        GameManager.Event_OnGameOver -= OnGameOver;
    }

    private void OnGameOver()
    {
        Destroy(this);
    }
}
