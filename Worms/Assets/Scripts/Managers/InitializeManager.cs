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

    InitializeManager initialize;
 
    #region Comments
    // We have to initialize each worm at some targeted spawn locations,
    // each seperated from each other without overlapping positions
    //
    // Additionally, we need to feed information to the TurnManager
    // so that they know which player to start with
    #endregion

    

    protected override void Init()
    {
        _wormsPerPlayer = PlayerPrefs.GetInt("WormsCount");
        _playerCount = PlayerPrefs.GetInt("PlayersCount");
        int id = 0;

        for (int j = 0; j < _playerCount; j++)
        {
            print(_playerCount.ToString());
            PlayerWorms myWorms = new PlayerWorms(j);
            players.Add(myWorms);
            print(players[j]);
            for (int k = 0; k < _wormsPerPlayer; k++)
            {
                WormData worms = Instantiate(_wormPrefab).GetComponent<WormData>();
                worms.SetData(_startingHealth, wormNames[id]);
                worms.SetPlayerID(j);
                worms.SetID(k);
                players[j].SayHello();
                players[j].AddWorm(worms);
                print(id.ToString());
                id++;
                this.worms.Add(worms);
            }
        }

        print(worms.Count);
        print(_playerCount + "  and  " + _wormsPerPlayer);
        
        float i = 0f;
        float z = 0f;
        Vector3 direction = new Vector3();

        foreach (PlayerWorms player in players)
        {

            List<WormData> wormList = player.GetWorms();
            foreach (WormData worm in wormList)
            {
                direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * i), 0f, Mathf.Cos(Mathf.Deg2Rad * i)).normalized;
                worm.gameObject.transform.position = direction * 10f;
                worm.gameObject.transform.Translate(new Vector3(0f, z * 2f, 0f), Space.Self);
                i += 360f / (_playerCount * _wormsPerPlayer);

            }
            z++;
        }
        SpawnWorms();
        ///StartCoroutine(Countdown(2, 3));
        ///StartCoroutine(Countdown(4, 4));
        ///StartCoroutine(Countdown(6, 5));
    }

    IEnumerator Countdown(float time, int dead)
    {
        yield return new WaitForSeconds(time);
        worms[dead].HasDied();
    }

    private void SpawnWorms()
    {
        var list = spawnPoints;
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

    void DeathHasOccured(WormData worms)
    {
        print("The worm " + worms.wormName + " has died!" );
    }
}
