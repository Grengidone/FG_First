using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivity : MonoBehaviour
{
    ActivePlayerManager activePlayerManager = ActivePlayerManager.instance;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            activePlayerManager.activePlayer.GetCurrentWorm().GetComponent<WormWeaponData>().ShootNow();
        }
    }
}
