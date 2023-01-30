using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            APPSTATE_ApplicationInitialState();
        }

    #endregion

    #region Private Methods

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
                APPSTATE_RegisterDataState();
            }
            else
            {
                APPSTATE_MainMenuDisplayState();
            }
        
            StopCoroutine(LoadingGameDataCoroutine);
        }

    #endregion

    #region Appstate Control Methods

        public void APPSTATE_ApplicationInitialState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_INIT);
        }

        public void APPSTATE_RegisterDataState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_REGISTER);
        }

        public void APPSTATE_MainMenuDisplayState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_MAINMENU);
        }

        public void APPSTATE_PreparingGameState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_PREPARING);
        }

        public void APPSTATE_MatchHistoryState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_MATCH);
        }

        public void APPSTATE_InventoryState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_INVENTORY);
        }

        public void APPSTATE_SelectMapState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_MAP);
        }

        public void APPSTATE_KitchenChooseState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_KITCHEN_CHOOSE);
        }
        
        public void APPSTATE_LaunchGameState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_LAUNCH);
        }

        public void APPSTATE_GameplayState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_GAMEPLAY);
        }

        public void APPSTATE_EndgameState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_ENDGAME);
        }

        public void APPSTATE_ResetState()
        {
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_RESET);
        }

    #endregion
}
