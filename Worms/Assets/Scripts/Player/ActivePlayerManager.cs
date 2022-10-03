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

    public List<PlayerWorms> playersLost = new List<PlayerWorms>();
    [HideInInspector] public bool gameEnded = false;

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
        if (Input.GetKeyDown(KeyCode.V) && !gameEnded)
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
        if (!gameEnded)
        {
            cameraData.ChangeWormCheck();

        }
    }

    public void SetNextPlayer()
    {
        
        activePlayer.PrepareNextWorm();
        activePlayerID++;
        if (playerList.Count != 0)
        {
            activePlayerID %= playerList.Count;
        }
        

       while (playerList[playerOrder[activePlayerID]].hasLost == true)
        {
            if (playersLost.Count >= playerList.Count - 1)
            {
                GameEnded();
                break;
            }
            activePlayerID++;
            if (playerList.Count != 0)
            {
                activePlayerID %= playerList.Count;
            }
        }
        activePlayer = playerList[playerOrder[activePlayerID]];


    }

    public void RemovePlayerWorm(WormData worm)
    {
        worm.gameObject.SetActive(false);

        int playerId = worm.playerID;
       
        foreach(PlayerWorms player in playerList)
        {
            if (player.playerID == playerId)
            {
                
                player.RemoveWorm(worm);
                if (playerId == activePlayer.playerID && activePlayer.GetCurrentWorm()?.id == worm.id)
                {
                    ChangeTurn();
                } else if (activePlayer.GetCurrentWorm() == null)
                {
                    ChangeTurn();

                }
                break;
            }
        }
        

    }


    public void GameEnded()
    {
        Debug.LogWarning("ARARARARAR");
        gameEnded = true;
    }

    private void OnDisable()
    {
        WormData.stinked -= RemovePlayerWorm;

    }
    private void OnEnable()
    {
        
    }
}
