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

                    case Enumerators.AppState.APP_LAUNCH:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region


                        break;

                    default:

                        return;
            }

            OnAppStateChanged?.Invoke(index);
        }

    #endregion
    
}
