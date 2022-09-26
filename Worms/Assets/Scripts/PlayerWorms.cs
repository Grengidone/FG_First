using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerWorms
{
    Worms worms;
    List<Worms> myWorms;
    private int _playerID;
    private string _playerName;
    
    public PlayerWorms(int id)
    {
         myWorms = new List<Worms>();
        _playerID = id;
    }

    public List<Worms> GetWorms()
    {
        return myWorms;
    }
    

    public void AddWorm(Worms newWorm)
    {
        myWorms.Add(newWorm);
    }

    public void SayHello()
    {
        Debug.Log("Hello");
    }

}
