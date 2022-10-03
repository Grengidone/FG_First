using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting.FullSerializer;
using Unity.VisualScripting;

public class PlayerWorms
{
    WormData worms;
    List<WormData> myWorms;
    public int playerID;
    public string playerName;
    public int nextWorm;
    public bool hasLost = false;
    public PlayerWorms(int id)
    {
         myWorms = new List<WormData>();
        playerID = id;
        nextWorm = 0;
        hasLost = false;
    }

    public List<WormData> GetWorms()
    {
        return myWorms;
    }

    public int GetPlayerID()
    {
        return playerID;
    }

    public void AddWorm(WormData newWorm)
    {
        myWorms.Add(newWorm);
    }

    public void RemoveWorm(WormData removeWorm)
    {
        //ActivePlayerManager.instance.RemovePlayerWorm(removeWorm);

        foreach (var worm in myWorms)
        {
            if (worm == removeWorm)
            {
                myWorms.Remove(worm);
                if (myWorms.Count != 0)
                {
                    nextWorm %= myWorms.Count;

                }
                Debug.Log("Worm removed! Player ID " + playerID + " Worms left = " + myWorms.Count);
                break;
            }
        }
        if (myWorms.Count <= 0)
        {
            HasLost();
            //ActivePlayerManager.instance.RemovePlayer(this);
        }
        
    }
    public WormData GetCurrentWorm()
    {
        return myWorms[nextWorm];
    }

    public void HasLost()
    {
        hasLost = true;
    }
    public void PrepareNextWorm()
    {
        nextWorm++;
        if (myWorms.Count != 0)
        {
        nextWorm %= myWorms.Count;
        }
    }

    public void SayHello()
    {
        Debug.Log("Hello! I am : " + playerName + " & my ID is : " + playerID);
    }

}
