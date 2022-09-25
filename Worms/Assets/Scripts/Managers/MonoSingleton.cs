using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    #region Public Accessors

    /// <summary>
    /// Static instance of PersistentGameObjectSingleton which allows it to be accessed by any other script.
    /// </summary>
    public static MonoSingleton<T> Instance { get; private set; }

    #endregion

    #region Unity methods

    /// <summary>
    /// Things to do as soon as the scene starts up
    /// </summary>

    #endregion
    protected virtual void Awake()
    {
        Debug.Log(this.GetType().Name + " - Awoken. Initializing Singleton pattern. Instance Id : " + gameObject.GetInstanceID());

        if (Instance == null)
        {
            Debug.Log(this.GetType().Name + " - Setting first instance. Instance Id : " + gameObject.GetInstanceID());

            //if not, set instance to this
            Instance = this;

            //Sets this to not be destroyed when reloading scene
            //DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Debug.LogWarning(this.GetType().Name + " - Destroying secondary instance. Instance Id : " + gameObject.GetInstanceID());

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GlobalManager.
            DestroyImmediate(gameObject.GetComponent<T>());
            

            return;
        }
    }
    protected virtual void OnDestroy()
    {
        if (Instance == GetComponent<T>())
        {
            Instance = null;
        }
    }
}
