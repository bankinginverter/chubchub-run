using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugStateManager : MonoBehaviour
{
    public static DebugStateManager Instance;

    #region Unity Declarations

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

    #endregion

    #region Private Methods

        public void DebugAppStateChanged(Enumerators.AppState CurrentAppState)
        {
            Debug.Log("Current App State : <color=green>" + CurrentAppState + "</color>");
        }

        public void DebugKitchenStateChanged(Enumerators.KichenState CurrentAppState)
        {
            Debug.Log("Current App State : <color=pink>" + CurrentAppState + "</color>");
        }

    #endregion
}
