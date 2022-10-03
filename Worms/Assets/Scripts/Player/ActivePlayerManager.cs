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
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float force;
    private int activePlayerID = 0;

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
        activePlayer = playerList[playerOrder[activePlayerID]];
        Debug.LogWarning(activePlayer.GetPlayerID());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Transform fireLocation = GetActivePlayer().GetCurrentWorm().fireLocation;
            GameObject projectile = Instantiate(projectilePrefab, fireLocation.position, fireLocation.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(activePlayer.GetCurrentWorm().gameObject.transform.forward * force, ForceMode.Impulse);
            ChangeTurn();
        }
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
    public void ChangeTurn()
    {
        SetNextPlayer();
        cameraData.ChangeWormCheck();
    }

    public void SetNextPlayer()
    {
        
        activePlayer.PrepareNextWorm();
        activePlayerID++;
        if (playerList.Count != 0)
        {
            activePlayerID %= playerList.Count;
        }
        else
        {
            // Umm, the game should be over, yeah?
        }

        if (playerList[playerOrder[activePlayerID]].hasLost == true)
        {
            activePlayerID++;
            activePlayerID %= playerList.Count;
        }else
        {
            activePlayer = playerList[playerOrder[activePlayerID]];
        }
    }

    public void RemovePlayerWorm(WormData worm)
    {
        int playerId = worm.playerID;
        if (playerId == activePlayer.playerID)
        {
            ChangeTurn();
        }
        foreach(PlayerWorms player in playerList)
        {
            if (player.playerID == playerId)
            {
                player.RemoveWorm(worm);
                if (player.GetWorms().Count <= 0)
                {
                    player.HasLost();

                }
                break;
            }
        }

    }

    public void RemovePlayer(int playerID)
    {
        foreach (PlayerWorms player in playerList)
        {
            if (player.playerID == playerID)
            {
                playerList.Remove(player);
                break;
            }
        }
        foreach (var item in playerOrder)
        {
            if (item == playerID)
            {
                playerOrder.Remove(item);
                break;
            }
        }
        if (playerList.Count == 1)
        {
            // This Player Wins
        }
        else if (playerList.Count <= 0)
        {
            // No worms alive, its a tie
        }
    }

    private void OnDisable()
    {
        WormData.stinked -= RemovePlayerWorm;

    }
    private void OnEnable()
    {
        
    }
}
