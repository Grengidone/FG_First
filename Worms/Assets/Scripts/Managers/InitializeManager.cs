using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : MonoSingleton<InitializeManager>
{
    [SerializeField] GameObject _wormPrefab;
    [SerializeField] int _startingHealth;
    List<WormData> worms = new List<WormData>();
    private List<PlayerWorms> players = new List<PlayerWorms>();
    [SerializeField] List<string> wormNames = new List<string>(16);
    [SerializeField] List<GameObject> spawnPoints = new List<GameObject>();

    private int _wormsPerPlayer;
    private int _playerCount;


 
    #region Comments
    // We have to initialize each worm at some targeted spawn locations,
    // each seperated from each other without overlapping positions
    //
    // Additionally, we need to feed information to the TurnManager
    // so that they know which player to start with
    #endregion

    

     void Awake()
    {
        base.Awake();
        _wormsPerPlayer = PlayerPrefs.GetInt("WormsCount");
        _playerCount = PlayerPrefs.GetInt("PlayersCount");
        int id = 0;

        for (int j = 0; j < _playerCount; j++)
        {
            Debug.Log(_playerCount.ToString());
            PlayerWorms myWorms = new PlayerWorms(j);
            players.Add(myWorms);
            Debug.Log(players[j]);
            for (int k = 0; k < _wormsPerPlayer; k++)
            {
                WormData worms = Instantiate(_wormPrefab).GetComponent<WormData>();
                worms.SetData(_startingHealth, wormNames[id]);
                worms.SetPlayerID(j);
                worms.SetID(k);
                players[j].SayHello();
                players[j].AddWorm(worms);
                Debug.Log(id.ToString());
                id++;
                this.worms.Add(worms);
            }
        }

        Debug.Log(worms.Count);
        Debug.Log(_playerCount + "  and  " + _wormsPerPlayer);


        SpawnWorms();

    }

 

    private void SpawnWorms()
    {
        List<GameObject> list = spawnPoints;
        int randomNumber;
        foreach (WormData worm in worms)
        {
            randomNumber = Random.Range(0, list.Count);
            worm.gameObject.transform.position = list[randomNumber].transform.position;
            list.RemoveAt(randomNumber);
        }
    }

    public List<PlayerWorms> GetInitialPlayers()
    {
        return players;
    } 


}
