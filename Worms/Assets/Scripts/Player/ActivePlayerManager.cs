using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerManager : MonoSingleton<ActivePlayerManager>
{
    List<PlayerWorms> playerList;
    List<int> playerOrder = new List<int>();

    [SerializeField] float _timeBetweenTurns;
    [SerializeField] float _maxTimePverTurn;
    [SerializeField] GameObject _winCanvas;
    [SerializeField] TextMeshProUGUI _playerWin;
    public ThirdPersonMovement cameraData;


    public PlayerWorms activePlayer{ get; private set; }
    public GameObject projectilePrefab;
    [SerializeField] float _force;
    private int _activePlayerID = 0;

    public List<PlayerWorms> playersLost = new List<PlayerWorms>();
    [HideInInspector] public bool gameEnded = false;
    private bool hasShot = false;
     void Awake()
    {
        base.Awake();
        WormData.stinked += RemovePlayerWorm;
        playerList = InitializeManager.instance.GetInitialPlayers();
        var tempOrder = GenerateList(playerList.Count);
        for (int i = 0; i < playerList.Count; i++)
        {
            int randomNumber = Random.Range(0, tempOrder.Count);
            playerOrder.Add(tempOrder[randomNumber]);
            tempOrder.RemoveAt(randomNumber);
        }
        activePlayer = playerList[playerOrder[_activePlayerID]];
        //Debug.LogWarning(activePlayer.GetPlayerID());
        cameraData.ChangeWormCheck();
    }

    public void CallInit()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && !gameEnded && !hasShot)
        {
            Transform fireLocation = GetActivePlayer().GetCurrentWorm().fireLocation;
            GameObject projectile = Instantiate(projectilePrefab, fireLocation.position, fireLocation.rotation);
            projectile.GetComponent<Rigidbody>().AddForce(activePlayer.GetCurrentWorm().gameObject.transform.forward * _force, ForceMode.Impulse);
            StartCoroutine(WaitTime(projectile));
        }
    }

    IEnumerator WaitTime(GameObject waitTileUnactive)
    {
        hasShot = true;
        while (waitTileUnactive.activeInHierarchy == true )
        {
            yield return new WaitForEndOfFrame();
            if (waitTileUnactive == null)
            {
                break;
            }
        }
        hasShot = false;
        ChangeTurn();
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
        _activePlayerID++;
        if (playerList.Count != 0)
        {
            _activePlayerID %= playerList.Count;
        }
        int zero = 0;

       while (playerList[playerOrder[_activePlayerID]].hasLost == true && zero < 100)
        {
            zero++;   
            _activePlayerID++;
            if (playerList.Count != 0)
            {
                _activePlayerID %= playerList.Count;
            }
        }
        activePlayer = playerList[playerOrder[_activePlayerID]];

        if (playersLost.Count >= playerList.Count - 1)
        {
            GameEnded();
            
        }
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
        PlayerWorms player = null;
        foreach(PlayerWorms playerWorms in playerList)
        {
            if (!playerWorms.hasLost)
            {
                player = playerWorms;
            }
        }
        gameEnded = true;
        _winCanvas.SetActive(true);
        switch (player)
        {
            case null:
                _playerWin.SetText("Player: " + "01100100 01101001 01100101 01100100");
                break;

            default:
                _playerWin.SetText("Player: " + (player.playerID + 1));
                break;
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
