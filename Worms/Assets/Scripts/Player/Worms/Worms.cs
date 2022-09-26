using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Worms : MonoBehaviour
{
    private void Awake()
    {
        
    }
    public delegate void Stinker(Worms worms);
    public static event Stinker stinked;

    private int _health;
    private string _name;
    private int _id;
    public Worms(int id)
    {
        _id = id;
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

    

    public void OnDeath()
    {
        gameObject.SetActive(false);
        stinked(this);
    }

}
