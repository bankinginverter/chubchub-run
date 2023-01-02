using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUnityLifeCycle : MonoBehaviour
{
    #region Unity Declarations

        public static MainUnityLifeCycle Instance;

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

        private Coroutine LoadingGameDataCoroutine;

        private bool newPlayer;

    #endregion

    #region Unity Methods
    
        private void Start() 
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_INIT);
        }

    #endregion

    #region Private Methods

        public void LaunchGameState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_LAUNCH);
        }

        public void LoadGameScene(string index)
        {
            SceneManager.LoadScene(index);
        }

        public void LoadInitialGameData()
        {
            LoadingGameDataCoroutine = StartCoroutine(LoadingGameData());
        }

        public void SetNewPlayerState(bool index)
        {
            newPlayer = index;
        }

        IEnumerator LoadingGameData()
        {
            AppUIManager.Instance.SetLoadingProgression(0f);

            Debug.Log("Enter loading coroutine (progress 0%)");

            yield return new WaitForSeconds(1f);

            AppUIManager.Instance.SetLoadingProgression(12f);

            Debug.Log("Ready to Retrieve player data (progress 12%)");

            yield return new WaitForSeconds(1f);

            AppUIManager.Instance.SetLoadingProgression(69f);

            Debug.Log("Retrieve player data (progress 69%)");

            PlayerManager.Instance.LoadPlayerHealth();

            yield return new WaitForSeconds(1f);
    
            AppUIManager.Instance.SetLoadingProgression(100f);

            Debug.Log("Retrieved and send valid state (progress 100%)");

            if(newPlayer)
            {
                AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_REGISTER);
            }
            else
            {
                AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_MAINMENU);
            }
        
            StopCoroutine(LoadingGameDataCoroutine);
        }

    #endregion
}
