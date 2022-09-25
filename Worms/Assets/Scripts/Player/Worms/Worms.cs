using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Worms
{
    public delegate void Stinker(Worms worms);
    public static event Stinker stinked;

    private int _health;
    private string _name;
    private int _id;
    private GameObject _gameObject;
    public Worms(int id)
    {
        _id = id;
    }

    public void SetData(int health, string name, GameObject gameObject)
    {
        _health = health;
        _name = name;
        _gameObject = gameObject;
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

    public GameObject GetObject()
    {
        return _gameObject;
    }

    public void Kill()
    {
        _gameObject.SetActive(false);
        stinked(this);
    }

    public static void OnDeath(Worms deadWorm)
    {
        
    }
}
