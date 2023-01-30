using System;

public class KitchenStateManager
{
    #region Unity Declarations

        public event Action<Enumerators.KichenState> OnKitchenStateChanged;

        public static Enumerators.KichenState CurrentKitchenState;

        public static Enumerators.KichenState NextKitchenState;

        private static readonly object Sync = new object();
        
        private static KitchenStateManager _instance;

        public static KitchenStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (Sync)
                    {
                        _instance = new KitchenStateManager();
                    }
                }

                return _instance;
            }
        }

        internal KitchenStateManager()
        {

        }

    #endregion

    #region Private Methods

        public void SetCurrentKitchenState(Enumerators.KichenState index)
        {
            switch(index)
            {
                    case Enumerators.KichenState.KITCHEN_INIT:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SetupRaycastComponent(true);

                        switch(AppStateManager.recipeSelectedFromAppState)
                        {   
                            case "CLEARSOUP":

                                SetCurrentKitchenState(Enumerators.KichenState.CLEARSOUP_SEQUENCE_1);

                                break;

                            case "STIRFRIED":

                                SetCurrentKitchenState(Enumerators.KichenState.STIRFRIED_SEQUENCE_1);

                                break;

                            case "FRIEDRICE":

                                SetCurrentKitchenState(Enumerators.KichenState.FRIEDRICE_SEQUENCE_1);

                                break;
                        }

                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_1:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(0);
                        KitchenGameplayManager.Instance.SetupScorePoint(3);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_1;
                        NextKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_2;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_2:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(1);
                        KitchenGameplayManager.Instance.SetupScorePoint(3);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_2;
                        NextKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_3;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_3:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(2);
                        KitchenGameplayManager.Instance.SetupScorePoint(4);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_3;
                        NextKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_4;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_4:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(3);
                        KitchenGameplayManager.Instance.SetupScorePoint(2);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.CLEARSOUP_SEQUENCE_4;
                        NextKitchenState = Enumerators.KichenState.NONE;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_1:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(0);
                        KitchenGameplayManager.Instance.SetupScorePoint(2);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_1;
                        NextKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_2;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_2:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(1);
                        KitchenGameplayManager.Instance.SetupScorePoint(2);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_2;
                        NextKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_3;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_3:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(2);
                        KitchenGameplayManager.Instance.SetupScorePoint(6);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_3;
                        NextKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_4;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_4:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(3);
                        KitchenGameplayManager.Instance.SetupScorePoint(1);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.STIRFRIED_SEQUENCE_4;
                        NextKitchenState = Enumerators.KichenState.NONE;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_1:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(0);
                        KitchenGameplayManager.Instance.SetupScorePoint(4);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.FRIEDRICE_SEQUENCE_1;
                        NextKitchenState = Enumerators.KichenState.FRIEDRICE_SEQUENCE_2;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_2:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(1);
                        KitchenGameplayManager.Instance.SetupScorePoint(3);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.FRIEDRICE_SEQUENCE_2;
                        NextKitchenState = Enumerators.KichenState.FRIEDRICE_SEQUENCE_3;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_3:

                        //Debug Region
                        DebugStateManager.Instance.DebugKitchenStateChanged(index); 

                        //Runtime Function Region
                        KitchenGameplayManager.Instance.SpawnPrefab(2);
                        KitchenGameplayManager.Instance.SetupScorePoint(3);
                        KitchenGameplayManager.Instance.ValidateScore();

                        CurrentKitchenState = Enumerators.KichenState.FRIEDRICE_SEQUENCE_3;
                        NextKitchenState = Enumerators.KichenState.NONE;

                        AppUIManager.Instance.ChangeDescriptionMethod();
                        
                        break;

                    default:

                        return;
            }

            OnKitchenStateChanged?.Invoke(index);
        }

    #endregion
    
}


