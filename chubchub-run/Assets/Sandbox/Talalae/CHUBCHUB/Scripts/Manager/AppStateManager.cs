using System;

public class AppStateManager
{
    #region Unity Declarations

        public event Action<Enumerators.AppState> OnAppStateChanged;

        private static readonly object Sync = new object();
        
        private static AppStateManager _instance;

        public static AppStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        _instance = new AppStateManager();
                    }
                }

                return _instance;
            }
        }

        internal AppStateManager()
        {

        }

    #endregion

    #region Private Methods

        public void SetCurrentAppState(Enumerators.AppState index)
        {
            switch(index)
            {
                    case Enumerators.AppState.APP_INIT:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveLoadingScreenPanel();
                        MainUnityLifeCycle.Instance.LoadInitialGameData();
                        
                        break;

                    case Enumerators.AppState.APP_REGISTER:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveRegisterScreenPanel();
                        AppUIManager.Instance.CheckPlayerInput();
                        
                        
                        break;

                    case Enumerators.AppState.APP_MAINMENU:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveMainmenuScreenPanel();
                    
                        AppUIManager.Instance.SetupMainMenuUI();
                        
                        break;

                    case Enumerators.AppState.APP_PREPARING:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActivePreparingGameplayScreenPanel();
                        TimerManager.Instance.SetupPreparingTimer();
                        AppUIManager.Instance.SyncTimerTextWithGUI();
                        
                        break;

                    case Enumerators.AppState.APP_MATCH:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveMatchHistoryScreenPanel();
                        AppUIManager.Instance.FetchMatchHistoryToGUI();
                        
                        break;

                    case Enumerators.AppState.APP_INVENTORY:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveInventoryScreenPanel();
                        AppUIManager.Instance.FetchInventoryToGUI();
                        
                        break;

                    case Enumerators.AppState.APP_COSTUME:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveCostumeScreenPanel();
                        
                        break;

                    case Enumerators.AppState.APP_LAUNCH:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        SceneControllerManager.Instance.LoadGameScene("SCENE_GAMEPLAY");

                        break;

                    case Enumerators.AppState.APP_GAMEPLAY:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        GameplayUIManager.Instance.CloseAllComponent();
                        GameplayUIManager.Instance.SetActiveGameplayScreenPanel();

                        break;

                    case Enumerators.AppState.APP_ENDGAME:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        GameplayUIManager.Instance.CloseAllComponent();
                        GameplayUIManager.Instance.SetActiveEndGameScreenPanel();

                        break;

                    default:

                        return;
            }

            OnAppStateChanged?.Invoke(index);
        }

    #endregion
    
}
