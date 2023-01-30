using System;

public class AppStateManager
{
    #region Unity Declarations

        public event Action<Enumerators.AppState> OnAppStateChanged;

        public static string mapSelectedFromAppState;

        public static string recipeSelectedFromAppState;

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
                        AppUIManager.Instance.SetDataMapSelectedFromMenuVariable("FOREST");
                        KitchenGameplayManager.Instance.SetupRaycastComponent(false);
                        SoundManager.Instance.SetBackgroundAudio(0);
                        
                        break;

                    case Enumerators.AppState.APP_REGISTER:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveRegisterScreenPanel();
                        AppUIManager.Instance.CheckPlayerInput();
                        CostumeManager.Instance.ResetGenderCostume("BOY");
                        
                        break;

                    case Enumerators.AppState.APP_MAINMENU:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveMainmenuScreenPanel();
                    
                        AppUIManager.Instance.SetupMainMenuUI();
                        CostumeManager.Instance.ChangeCostumeTo(PlayerManager.Instance.CostumeData, PlayerManager.Instance.GenderData);
                        
                        break;

                    case Enumerators.AppState.APP_MAP:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveMapSelectingScreenPanel();
                        
                        break;

                    case Enumerators.AppState.APP_KITCHEN_CHOOSE:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveKitchenSelectScreenPanel();
                        AppUIManager.Instance.VerifyCookingRecipe();
                        
                        break;

                    case Enumerators.AppState.APP_KITCHEN_COOK:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveKitchenGameplayScreenPanel();  
                        KitchenStateManager.Instance.SetCurrentKitchenState(Enumerators.KichenState.KITCHEN_INIT);
                        
                        break;

                    case Enumerators.AppState.APP_KITCHEN_RESULT:

                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveKitchenResultScreenPanel();  
                        KitchenGameplayManager.Instance.ClearDataKitchenGameplay();
                        AppUIManager.Instance.FetchedResultIndexPanel();
                        
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

                    case Enumerators.AppState.APP_LAUNCH:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.CopySelectedMapData();
                        SceneControllerManager.Instance.LoadGameScene("SCENE_GAMEPLAY");

                        break;

                    case Enumerators.AppState.APP_GAMEPLAY:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        GameplayUIManager.Instance.CloseAllComponent();
                        GameplayUIManager.Instance.SetActiveGameplayScreenPanel();

                        PlayerManager.Instance.LoadPlayerHealth();
                        CostumeManager.Instance.ChangeCostumeTo(PlayerManager.Instance.CostumeData, PlayerManager.Instance.GenderData);
                        TileManager.Instance.SettingTileFactor(mapSelectedFromAppState);
                        TileManager.Instance.SetupPreparingAsset();
                        TileManager.Instance.SetupTileSpawner();
                        SoundManager.Instance.SetBackgroundAudio(0);

                        break;

                    case Enumerators.AppState.APP_ENDGAME:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        GameplayUIManager.Instance.CloseAllComponent();
                        GameplayUIManager.Instance.SetActiveEndGameScreenPanel();

                        break;

                    case Enumerators.AppState.APP_RESET:
                    
                        //Debug Region
                        DebugStateManager.Instance.DebugAppStateChanged(index); 

                        //Runtime Function Region
                        AppUIManager.Instance.CloseAllComponent();
                        AppUIManager.Instance.SetActiveResetScreenPanel();

                        break;

                    default:

                        return;
            }

            OnAppStateChanged?.Invoke(index);
        }

    #endregion
    
}
