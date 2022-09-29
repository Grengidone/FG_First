using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponBehaviour : MonoBehaviour
{
    public bool hasProectile;
    public WeaponBehaviour()
    {
        
    }
    public abstract void Shoot();

    public abstract void GetProjectile();

}
