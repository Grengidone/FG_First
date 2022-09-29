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

    [SerializeField] private Image _healthBar;
    [HideInInspector] public int health;
    [HideInInspector] public string wormName;
    [HideInInspector] public int id;
    [HideInInspector] public int playerID;
    [SerializeField] private Transform fireLocation;


    public void SetID(int id)
    {
        this.id = id;
    }

    public void SetPlayerID(int playerID)
    {
        this.playerID = playerID;
    }

    public void SetData(int health, string name)
    {
        this.health = health;
        this.wormName = name;
        gameObject.name = wormName;
    }
 
    #region Worm actions

    public void TakeDamage(int damageTaken)
    {
        health -= damageTaken;
        if (health <= 0)
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
