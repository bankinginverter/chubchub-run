using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AppUIManager : MonoBehaviour
{
    #region Unity Declarations

        public static AppUIManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(this);
        }

        [Header("Game Screen Section")]

        [SerializeField] private GameObject loadingScreenComponent;
        
        [SerializeField] private GameObject registerScreenComponent;

        [SerializeField] private GameObject mainmenuScreenComponent;

        [SerializeField] private GameObject preparingGameplayScreenComponent;

        [SerializeField] private GameObject matchHistoryScreenComponent;

        [SerializeField] private GameObject inventoryScreenComponent;

        [SerializeField] private GameObject mapSelectingScreenComponent;

        [Header("Progress Loading Section")]

        [SerializeField] private Slider progressLoadingBar;

        [SerializeField] private TMP_Text progressLoadingText;

        [Header("Register Screen Section")]

        private Coroutine checkingPlayerInputCoroutine;

        private string nameRetrieved;

        private string weightRetrieved;

        private string heightRetrieved;

        private string ageRetrieved;

        private string genderRetrieved;

        private int costumeRetrieved;

        [SerializeField] private GameObject confirmDataButton;

        [Header("MainMenu Screen Section")]

        [SerializeField] private TMP_Text playerName;

        [SerializeField] private TMP_Text bmiValue;

        [SerializeField] private TMP_Text playRound;

        [SerializeField] private GameObject[] gameObjectElement;

        private int mapIndex;

        [Header("Preparing Screen Section")]

        [SerializeField] private TMP_Text timer_Minute;

        [SerializeField] private TMP_Text timer_Second;

        [Header("Match History Screen Section")]

        [SerializeField] private GameObject matchComponentTemplate;

        [SerializeField] private Slider[] graphDataIndex;
        
        private int first_day;

        private int current_week;

        private float[] data_chart = new float[12];
        
        private List<GameObject> allMatchComponent = new List<GameObject>();

        [Header("Inventory Screen Section")]

        [SerializeField] private GameObject itemComponentTemplate;

        [SerializeField] private Sprite[] itemImage;
        
        // ============ Image Code ============
        //  Imageindex [0] is INGREDIANT_BREAD
        //  Imageindex [1] is INGREDIANT_HAM
        //  Imageindex [2] is INGREDIANT_FISH
        // ====================================

        private List<GameObject> allItemComponent = new List<GameObject>();

        private int imageIndex;

        [Header("Select Map Screen Section")]

        [HideInInspector] public string mapSelectedFromMenu;

        [SerializeField] private GameObject selectedUI;

        [SerializeField] private Transform[] mapSelectPosition;

        [Header("Transform Section")]

        [SerializeField] private Transform spawnpoint_mainmenu;

        [SerializeField] private Transform spawnpoint_customize;

    #endregion

    #region Application UI Management

        public void CloseAllComponent()
        {
            #region UI Management

                loadingScreenComponent.SetActive(false);

                registerScreenComponent.SetActive(false);

                mainmenuScreenComponent.SetActive(false);

                preparingGameplayScreenComponent.SetActive(false);

                matchHistoryScreenComponent.SetActive(false);

                inventoryScreenComponent.SetActive(false);

                mapSelectingScreenComponent.SetActive(false);

            #endregion

            #region GameObject Management

                for(int i = 0; i < gameObjectElement.Length; i++)
                {
                    gameObjectElement[i].SetActive(false);
                }

                CostumeManager.Instance.SetActivePlayerModel(false);

                CostumeManager.Instance.SetActiveMalePlayer(false);

                CostumeManager.Instance.SetActiveFemalePlayer(false);

            #endregion
        }

    #endregion

    #region Specific UI Implements
 
        public void SetActiveLoadingScreenPanel()
        {
            loadingScreenComponent.SetActive(true);
        }

        public void SetActiveRegisterScreenPanel()
        {
            registerScreenComponent.SetActive(true);

            CostumeManager.Instance.SetActivePlayerModel(true);

            CostumeManager.Instance.TransformPlayerTo(spawnpoint_customize);
        }

        public void SetActiveMainmenuScreenPanel()
        {
            mainmenuScreenComponent.SetActive(true);

            CostumeManager.Instance.SetActivePlayerModel(true);

            CostumeManager.Instance.TransformPlayerTo(spawnpoint_mainmenu);

            gameObjectElement[mapIndex].SetActive(true);
        }

        public void SetActivePreparingGameplayScreenPanel()
        {
            preparingGameplayScreenComponent.SetActive(true);
        }

        public void SetActiveMatchHistoryScreenPanel()
        {
            matchHistoryScreenComponent.SetActive(true);
        }

        public void SetActiveInventoryScreenPanel()
        {
            inventoryScreenComponent.SetActive(true);
        }

        public void SetActiveMapSelectingScreenPanel()
        {
            mapSelectingScreenComponent.SetActive(true);
        }

        public void SetLoadingProgression(float index)
        {
            progressLoadingBar.value = index / 100;

            progressLoadingText.text = index.ToString() + "%";
        }

        public void CheckPlayerInput()
        {
            checkingPlayerInputCoroutine = StartCoroutine(checkingPlayerInput());
        }

        public void ConfirmPlayerData()
        {
            StopCoroutine(checkingPlayerInputCoroutine);
            
            PlayerManager.Instance.SyncHealthDataFromRegister(nameRetrieved, weightRetrieved, heightRetrieved, ageRetrieved, genderRetrieved, costumeRetrieved);

            MainUnityLifeCycle.Instance.APPSTATE_MainMenuDisplayState();
        }

        public void SetupMainMenuUI()
        {
            playerName.text = PlayerManager.Instance.PlayerNameData;

            bmiValue.text = string.Format("{0:#0.00}",(float.Parse(PlayerManager.Instance.WeightData) / ((float.Parse(PlayerManager.Instance.HeightData)/100)*(float.Parse(PlayerManager.Instance.HeightData)/100))));
        
            playRound.text =  PlayerManager.Instance.MatchFetchingData().ToString();
        }

        public void CopySelectedMapData()
        {
            AppStateManager.mapSelectedFromAppState = mapSelectedFromMenu;
        }

        public void SetDataMapSelectedFromMenuVariable(string index)
        {
            mapSelectedFromMenu = index;

            switch(mapSelectedFromMenu)
            {
                case "FOREST":

                    mapIndex = 0;    

                    break;

                case "BEACH":

                    mapIndex = 1;

                    break;

                case "CITY":

                    mapIndex = 2;

                    break;
            }
        }

        public void SyncTimerTextWithGUI()
        {
            timer_Minute.text = string.Format("{0:#00}",((TimerManager.Instance.setupTimer)/60));

            timer_Second.text = string.Format("{0:#00}",((TimerManager.Instance.setupTimer)%60));
        }

        public void FetchInventoryToGUI()
        {
            foreach(GameObject _itemComponent in allItemComponent)
            {
                Destroy(_itemComponent);
            }

            allItemComponent.Clear();

            itemComponentTemplate.gameObject.SetActive(false);

            for(int i = 0; i < PlayerManager.Instance.ItemFetchingData(); i++)
            {
                GameObject newItemComponent = Instantiate(itemComponentTemplate, itemComponentTemplate.transform.parent);
                
                newItemComponent.gameObject.SetActive(true);

                newItemComponent.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = itemImage[GetImageIndexFromData(i)];

                newItemComponent.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = PlayerManager.Instance.SpecificItemData_AMOUNT(i).ToString();
            
                allItemComponent.Add(newItemComponent);
            }
        }

        public void GraphDataUIReset()
        {
            for(int i = 0; i < graphDataIndex.Length; i++)
            {
                graphDataIndex[i].value = 0;
            }
        }

        public void FetchPositionOfSelectedMap()
        {
            switch(mapSelectedFromMenu)
            {
                case "FOREST":

                    selectedUI.transform.position = mapSelectPosition[0].transform.position;

                    break;

                case "BEACH":

                    selectedUI.transform.position = mapSelectPosition[1].transform.position;

                    break;

                case "CITY":

                    selectedUI.transform.position = mapSelectPosition[2].transform.position;

                    break;
            }
        }

        public void FetchMatchHistoryToGUI()
        {
            foreach(GameObject _matchComponent in allMatchComponent)
            {
                Destroy(_matchComponent);
            }

            allMatchComponent.Clear();

            GraphDataUIReset();

            matchComponentTemplate.gameObject.SetActive(false);

            for(int i = 0; i < PlayerManager.Instance.MatchFetchingData(); i++)
            {
                GameObject newMatchComponent = Instantiate(matchComponentTemplate, matchComponentTemplate.transform.parent);
                
                newMatchComponent.gameObject.SetActive(true);

                int fetchedTimeRound;

                float fetchedCaloriesRound;

                int fetchedDayRound;

                int fetchedMonthRound;

                int fetchedYearRound;

                PlayerManager.Instance.LoadPlayerMatch(i, out fetchedTimeRound, out fetchedCaloriesRound, out fetchedDayRound, out fetchedMonthRound, out fetchedYearRound);

                if(i == 0)
                {
                    first_day = fetchedDayRound + CalculateMonthToDay(fetchedMonthRound, fetchedYearRound);
                }

                current_week = ((fetchedDayRound + CalculateMonthToDay(fetchedMonthRound, fetchedYearRound) - first_day) / 7);
                    
                data_chart[current_week] += ((float)fetchedTimeRound/3600);

                newMatchComponent.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = string.Format("{0:#00}" + "/" +"{1:#00}" + "/" +"{2:#00}", fetchedDayRound, fetchedMonthRound, fetchedYearRound);

                newMatchComponent.transform.GetChild(2).gameObject.GetComponent<TMP_Text>().text = string.Format("{0:#00}" + " : " +"{1:#00}", fetchedTimeRound/60, fetchedTimeRound%60);

                newMatchComponent.transform.GetChild(3).gameObject.GetComponent<TMP_Text>().text = string.Format("{0:#0.00}", fetchedCaloriesRound);
            
                allMatchComponent.Add(newMatchComponent);
            }

            FetchDataOnGraph(1);
        }

        public void FetchDataOnGraph(int index)
        {
            switch(index)
            {
                case 1:

                    for(int i = 0; i < 4; i++)
                    {
                        graphDataIndex[i].value = data_chart[i];

                        graphDataIndex[i].transform.GetChild(1).GetComponent<TMP_Text>().text = "Week " + (i + 1).ToString();
                    }

                    break;

                case 2:

                    for(int i = 4; i < 8; i++)
                    {
                        graphDataIndex[i - 4].value = data_chart[i];

                        graphDataIndex[i - 4].transform.GetChild(1).GetComponent<TMP_Text>().text = "Week " + (i + 1).ToString();
                    }

                    break;

                case 3:

                    for(int i = 8; i < 12; i++)
                    {
                        graphDataIndex[i - 8].value = data_chart[i];

                        graphDataIndex[i - 8].transform.GetChild(1).GetComponent<TMP_Text>().text = "Week " + (i + 1).ToString();
                    }

                    break;
            }
        }

        private int CalculateMonthToDay(int index_month, int index_year)
        {
            int calculated_monthtoday = 0;

            for(int i = 1; i <= index_month; i++)
            {
                calculated_monthtoday += System.DateTime.DaysInMonth(index_year, i);
            }

            return calculated_monthtoday;
        }

        private int GetImageIndexFromData(int index)
        {

            switch(PlayerManager.Instance.SpecificItemData_NAME(index))
            {
                case "INGREDIANT_BREAD":

                    imageIndex = 0;

                    break;

                case "INGREDIANT_HAM":

                    imageIndex = 1;

                    break;

                case "INGREDIANT_FISH":

                    imageIndex = 2;

                    break;
            }

            return imageIndex;
        }

        IEnumerator checkingPlayerInput()
        {
            while(true)
            {
                if(nameRetrieved != null && weightRetrieved != null && heightRetrieved != null && ageRetrieved != null && genderRetrieved != null)
                {
                    confirmDataButton.SetActive(true);
                }
                else
                {
                    confirmDataButton.SetActive(false);
                }

                yield return new WaitForSeconds(.1f);
            }
        }

    #endregion

    #region Data SendRetrieve UI

        public void ReadStringNameInput(string index)
        {
            nameRetrieved = index;
        }

        public void ReadStringWeightInput(string index)
        {
            weightRetrieved = index;
        }

        public void ReadStringHeightInput(string index)
        {
            heightRetrieved = index;
        }

        public void ReadStringAgeInput(string index)
        {
            ageRetrieved = index;
        }

        public void ReadStringGenderInput(string index)
        {
            genderRetrieved = index;
        }

        public void ReadIntCostumeInput(int index)
        {
            costumeRetrieved = index;
        }

        public string SendStringGenderInput()
        {
            return genderRetrieved;
        }
 
    #endregion
}

