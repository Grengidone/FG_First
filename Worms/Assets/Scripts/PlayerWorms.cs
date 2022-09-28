using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;

public class PlayerWorms
{
    WormData worms;
    List<WormData> myWorms;
    private int _playerID;
    private string _playerName;
    private int _nextWorm;
    
    public PlayerWorms(int id)
    {
         myWorms = new List<WormData>();
        _playerID = id;
        _nextWorm = 0;
    }

    public List<WormData> GetWorms()
    {
        return myWorms;
    }

    public int GetPlayerID()
    {
        return _playerID;
    }

    public void AddWorm(WormData newWorm)
    {
        myWorms.Add(newWorm);
    }

    public void RemoveWorm(WormData removeWorm)
    {
        foreach (var worm in myWorms)
        {
            if (worm == removeWorm)
            {
                myWorms.Remove(worm);
                Debug.Log("Worm removed! Worms left = " + myWorms.Count);
            }
        }
        
    }
    public WormData GetCurrentWorm()
    {
        return myWorms[_nextWorm];
    }

    public void SetNextWorm()
    {
        _nextWorm++;
        _nextWorm = _nextWorm % myWorms.Count;
    }

    public void SayHello()
    {
        Debug.Log("Hello! I am : " + _playerName + " & my ID is : " + _playerID);
    }

}
