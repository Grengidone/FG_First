using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Cinemachine;
using TMPro;

public class WormData : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public delegate void Stinker(WormData worms);
    public static event Stinker stinked;

    [SerializeField] private Image _healthBar;
    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private TextMeshProUGUI _characterName;
     public CinemachineVirtualCamera _aimCamera;
     public int health;
    [SerializeField] private int maxHealth;
     public string wormName;
     public int id;
     public int playerID;
    public bool hasTakenDamage = false;
    public Transform fireLocation;
    public Transform aimCenter;
    public Transform aimUp;

    private void Update()
    {
        hasTakenDamage = false;
    }

    public void SetID(int id)
    {
        this.id = id;
    }

    public void SetPlayerID(int playerID)
    {
        this.playerID = playerID;
        _playerName.SetText("Player: " + (playerID + 1));
    }

    public void SetData(int health, string name)
    {
        this.health = health;
        maxHealth = health;
        this.wormName = name;
        gameObject.name = wormName;

        _characterName.SetText(wormName);
    }
 
    #region Worm actions

    public void TakeDamage(int damageTaken)
    {
        if (!HasBeenHit())
        {
            health -= damageTaken;
            hasTakenDamage = true;
        }

        if (health <= 0)
        {
            HasDied();
        }
        float healthAmount = (float)health / (float)maxHealth;
        _healthBar.fillAmount = healthAmount;
    }

    public bool HasBeenHit()
    {
        return hasTakenDamage;
    }
    public void HasDied()
    {        
        stinked(this);

    }

    #endregion

}
