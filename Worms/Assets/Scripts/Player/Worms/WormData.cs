using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class WormData : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public delegate void Stinker(WormData worms);
    public static event Stinker stinked;

    [SerializeField] private Slider _healthBar;
    [SerializeField] private int _health;
    [SerializeField] private string _name;
    [SerializeField] private int _id;
    [SerializeField] private int _playerID;


    public void SetID(int id)
    {
        _id = id;
    }

    public void SetPlayerID(int playerID)
    {
        _playerID = playerID;
    }

    public void SetData(int health, string name)
    {
        _health = health;
        _name = name;
    }
 
    public int GetHealth()
    {
        return _health;
    }
    public string GetName()
    {
        return _name;
    }

    public int GetID()
    {
        return _id;
    }

    public int GetPlayerID()
    {
        return _playerID;
    }

    #region Worm actions

    public void TakeDamage(int damageTaken)
    {
        _health -= damageTaken;
        if (_health <= 0)
        {
            HasDied();
        }
    }

    public void HasDied()
    {
        gameObject.SetActive(false);
        stinked(this);
    }

    #endregion

}
