using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerManager : MonoSingleton<ActivePlayerManager>
{
    List<PlayerWorms> playerList;
    List<int> playerOrder = new List<int>();

    [SerializeField] float timeBetweenTurns;
    [SerializeField] float maxTimePerTurn;
    [SerializeField] Image clock;
    [SerializeField] TextMeshProUGUI clockTime;
    [SerializeField] ThirdPersonMovement cameraData;

    private PlayerWorms _activePlayer;
    

    protected override void Init()
    {
        WormData.stinked += RemovePlayerWorm;
        playerList = InitializeManager.instance.GetInitialPlayers();
        var tempOrder = GenerateList(playerList.Count);
        for (int i = 0; i < playerList.Count; i++)
        {
            int randomNumber = Random.Range(0, tempOrder.Count);
            playerOrder.Add(tempOrder[randomNumber]);
            tempOrder.RemoveAt(randomNumber);
        }
        _activePlayer = playerList[playerOrder[0]];
        Debug.LogWarning(_activePlayer.GetPlayerID());
    }

    List<int> GenerateList(int count)
    {
        List<int> tempList = new List<int>();
        for (int i = 0; i < count; i++)
        {
            tempList.Add(i);
        }
        return tempList;
    }

    public PlayerWorms GetActivePlayer()
    {
        return _activePlayer;
    }

    public List<PlayerWorms> GetActivePlayers()
    {
        return playerList;
    }

    public void SetNextPlayer()
    {

    }

    public void RemovePlayerWorm(WormData worm)
    {
        int playerId = worm.GetPlayerID();
        playerList[worm.GetPlayerID()].RemoveWorm(worm);
        if (playerList[playerId].GetWorms().Count <= 0)
        {
            RemovePlayer(playerId);
        }
    }

    public void RemovePlayer(int playerID)
    {

    }
}
