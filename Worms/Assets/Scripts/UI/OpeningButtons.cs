using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Xml.Serialization;
using TMPro;

public class OpeningButtons : MonoBehaviour
{
    [SerializeField] GameObject _openingPanel;
    [SerializeField] GameObject _startGamePanel;
    private int _playerCount = 2;
    private int _wormCount = 1;
    [SerializeField] TextMeshProUGUI _playerCountText;
    [SerializeField] TextMeshProUGUI _wormsCountText;

    private void Awake()
    {
        _playerCount = PlayerPrefs.GetInt("PlayersCount");
        _wormCount = PlayerPrefs.GetInt("WormsCount");
        

    }

    private void Update()
    {
        _playerCountText.SetText(_playerCount.ToString());
        _wormsCountText.SetText(_wormCount.ToString());
    }
    public void NewGameButton()
    {
        _openingPanel.SetActive(false);
        _startGamePanel.SetActive(true);
    }

     

    public void StartGameButton()
    {
        //Save variables for the next scene (playing scene)
        //Load next scene
        /*Player */
        PlayerPrefs.SetInt("PlayersCount", _playerCount);
        PlayerPrefs.SetInt("WormsCount", _wormCount);
        SceneManager.LoadScene(1);
    }

    public void IncreaseCountPlayers()
    {
        //Increase player count
        //Clamp between 2-4
        _playerCount++;
        _playerCount = Mathf.Clamp(_playerCount, 2, 4);
    }

    public void DecreaseCountPlayers()
    {
        //Decrease player count
        _playerCount--;
        _playerCount = Mathf.Clamp(_playerCount, 2, 4);
    }

    public void IncreaseCountWorms()
    {
        //Increase playable worms
        _wormCount++;
        _wormCount = Mathf.Clamp(_wormCount, 1, 4);

    }

    public void DecreaseCountWorms()
    {
        //Decrease playable worms
        _wormCount--;
        _wormCount = Mathf.Clamp(_wormCount, 1, 4);

    }
}
