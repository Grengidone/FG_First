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

    public PlayerWorms activePlayer{ get; private set; }
    

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
        activePlayer = playerList[playerOrder[0]];
        Debug.LogWarning(activePlayer.GetPlayerID());
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
        return activePlayer;
    }

    public List<PlayerWorms> GetActivePlayers()
    {
        return playerList;
    }
    public void RemovePlayer(PlayerWorms removePlayer)
    {
        foreach (var player in playerList)
        {
            if (player == removePlayer)
            {
                playerList.Remove(player);
                Debug.Log("Player has been removed! Players left = " + playerList.Count);
                break;
            }
        }
        if (playerList.Count <= 1)
        {
            // Announce winner
        }
    }
    public void ChangeTurn()
    {

    }

    public void SetNextPlayer()
    {

    }

    public void RemovePlayerWorm(WormData worm)
    {
        int playerId = worm.playerID;
        playerList[worm.playerID].RemoveWorm(worm);
        if (playerList[playerId].GetWorms().Count <= 0)
        {
            RemovePlayer(playerId);
        }
    }

    public void RemovePlayer(int playerID)
    {

    }
}
