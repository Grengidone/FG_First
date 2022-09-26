using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : MonoSingleton<InitializeManager>
{
    [SerializeField] GameObject _wormPrefab;
    [SerializeField] int _health;
    List<Worms> worms = new List<Worms>();
    [HideInInspector] public static List<PlayerWorms> players = new List<PlayerWorms>();
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


    protected override void OnAwake()
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
                Worms worms = Instantiate(_wormPrefab).GetComponent<Worms>();
                worms.SetData(_health, wormNames[id]);
                players[j].SayHello();
                players[j].AddWorm(worms);
                //players[0].AddWorm(worms);
                print(id.ToString());
                id++;
            }
        }

        print(worms.Count);
        print(_playerCount + "  and  " + _wormsPerPlayer);
        
        float i = 0f;
        float z = 0f;
        Vector3 direction = new Vector3();

        foreach (PlayerWorms player in players)
        {

            List<Worms> wormList = player.GetWorms();
            foreach (Worms worm in wormList)
            {
                direction = new Vector3(Mathf.Sin(Mathf.Deg2Rad * i), 0f, Mathf.Cos(Mathf.Deg2Rad * i)).normalized;
                worm.gameObject.transform.position = direction * 10f;
                worm.gameObject.transform.Translate(new Vector3(0f, z * 2f, 0f), Space.Self);
                worm.gameObject.name = worm.GetName();
                i += 360f / (_playerCount * _wormsPerPlayer);

            }
            //print(worms.GetObject());
            z++;
        }
        
        
        //this.worms[1].Kill();
    }

    void DeathHasOccured(Worms worms)
    {
        print("The worm " + worms.GetName() + " has died!" );
    }

}
