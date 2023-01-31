using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

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

        [SerializeField] private GameObject kitchenSelectScreenComponent;

        [SerializeField] private GameObject kitchenGameplayComponent;

        [SerializeField] private GameObject kitchenResultComponent;

        [SerializeField] private GameObject resetComponent;

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

        [Header("Kitchen Select Screen Section")]

        [SerializeField] private GameObject ChooseRecipeComponent;

        [SerializeField] private GameObject[] RecipeComponent;

        private int[] recipeValue;

        [Header("Kitchen Gameplay Screen Section")]

        [SerializeField] GameObject NextSequenceButton;

        [SerializeField] TMP_Text ItemIndicator;

        [SerializeField] TMP_Text methodDescript;

        [Header("Kitchen Result Screen Section")]

        [SerializeField] GameObject[] ResultIndexPanel;

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

                kitchenSelectScreenComponent.SetActive(false);

                kitchenGameplayComponent.SetActive(false);

                kitchenResultComponent.SetActive(false);

                resetComponent.SetActive(false);

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

        public void SetActiveResetScreenPanel()
        {
            resetComponent.SetActive(true);
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

        public void SetActiveKitchenSelectScreenPanel()
        {
            kitchenSelectScreenComponent.SetActive(true);
        }

        public void SetActiveKitchenGameplayScreenPanel()
        {
            kitchenGameplayComponent.SetActive(true);
        }

        public void SetActiveKitchenResultScreenPanel()
        {
            kitchenResultComponent.SetActive(true);
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

        public void NextSequenceButtonSetting(bool index)
        {
            NextSequenceButton.SetActive(index);
        }

        public void SetItemText(string index)
        {
            ItemIndicator.text = index;
        }

        public void VerifyCookingRecipe()
        {
            PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

            if(itemdata == null)
            {
                for(int i = 0; i < 3; i++)
                {
                    RecipeComponent[i].transform.GetChild(1).gameObject.SetActive(false);

                    RecipeComponent[i].transform.GetChild(2).gameObject.SetActive(true);
                }
                
                Debug.Log("No match Found");
                
                return;
            }

            recipeValue = new int[3];

            for(int i = 0; i < 3; i++)
            {
                recipeValue[i] = 0;
            }

            for(int i = 0; i < itemdata.itemListData.Count; i++)
            {
                switch(itemdata.itemListData[i].itemType)
                {
                    case Item.ItemType.INGREDIANT_BASIL:
                    
                        if(itemdata.itemListData[i].amount >= 1)
                        {
                            recipeValue[1]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CABBAGE:
                    
                        if(itemdata.itemListData[i].amount >= 30)
                        {
                            recipeValue[0]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CARROT:
                    
                        if(itemdata.itemListData[i].amount >= 5)
                        {
                            recipeValue[0]++;

                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CELERY:
                    
                        if(itemdata.itemListData[i].amount >= 7)
                        {
                            recipeValue[0]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CHICKEN:
                    
                        if(itemdata.itemListData[i].amount >= 10)
                        {
                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CHILI:
                    
                        if(itemdata.itemListData[i].amount >= 1)
                        {
                            recipeValue[1]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CORN:
                    
                        if(itemdata.itemListData[i].amount >= 5)
                        {
                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CUCUMBER:
                    
                        if(itemdata.itemListData[i].amount >= 5)
                        {
                            recipeValue[1]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_EGG:
                    
                        if(itemdata.itemListData[i].amount >= 1)
                        {
                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_GALIC:
                    
                        if(itemdata.itemListData[i].amount >= 1)
                        {
                            recipeValue[1]++;

                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_PEAS:
                    
                        if(itemdata.itemListData[i].amount >= 5)
                        {
                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_PORK:
                    
                        if(itemdata.itemListData[i].amount >= 5)
                        {
                            recipeValue[0]++;
                        }

                        if(itemdata.itemListData[i].amount >= 20)
                        {
                            recipeValue[1]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_RICE:
                    
                        if(itemdata.itemListData[i].amount >= 8)
                        {
                            recipeValue[0]++;

                            recipeValue[1]++;
                        }

                        if(itemdata.itemListData[i].amount >= 15)
                        {
                            recipeValue[2]++;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_TOFU:
                    
                        if(itemdata.itemListData[i].amount >= 1)
                        {
                            recipeValue[0]++;
                        }

                        break;
                }
            }

            VerifyAmountRecipe();
        }
        
        public void EndCookingSync()
        {
            PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

            for(int i = 0; i < itemdata.itemListData.Count; i++)
            {
                switch(itemdata.itemListData[i].itemType)
                {
                    case Item.ItemType.INGREDIANT_BASIL:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CABBAGE:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "CLEARSOUP")
                        {
                            itemdata.itemListData[i].amount -= 30;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CARROT:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 5;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CELERY:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "CLEARSOUP")
                        {
                            itemdata.itemListData[i].amount -= 7;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CHICKEN:

                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 10;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CHILI:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CORN:

                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 5;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_CUCUMBER:

                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 5;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_EGG:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_GALIC:
                        
                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_PEAS:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 5;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_PORK:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "CLEARSOUP")
                        {
                            itemdata.itemListData[i].amount -= 5;
                        }

                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 20;
                        }

                        break;

                    case Item.ItemType.INGREDIANT_RICE:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "CLEARSOUP")
                        {
                            itemdata.itemListData[i].amount -= 8;
                        }

                        if(AppStateManager.recipeSelectedFromAppState == "STIRFRIED")
                        {
                            itemdata.itemListData[i].amount -= 8;
                        }

                        if(AppStateManager.recipeSelectedFromAppState == "FRIEDRICE")
                        {
                            itemdata.itemListData[i].amount -= 15;
                        }
                       
                        break;

                    case Item.ItemType.INGREDIANT_TOFU:
                    
                        if(AppStateManager.recipeSelectedFromAppState == "CLEARSOUP")
                        {
                            itemdata.itemListData[i].amount -= 1;
                        }

                        break;
                }
            }

            PlayerManager.Instance.OverwriteSaveData(itemdata);
        }

        public void DeleteSaveFile()
        {
            File.Delete (Application.persistentDataPath + "/player.HealthData");

            File.Delete (Application.persistentDataPath + "/player.ItemData");          

            File.Delete (Application.persistentDataPath + "/player.MatchData");

            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_INIT);
        }

        public void VerifyAmountRecipe()
        {
            if(recipeValue[0] == 6)
            {
                RecipeComponent[0].transform.GetChild(1).gameObject.SetActive(true);

                RecipeComponent[0].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                RecipeComponent[0].transform.GetChild(1).gameObject.SetActive(false);

                RecipeComponent[0].transform.GetChild(2).gameObject.SetActive(true);
            }

            if(recipeValue[1] == 6)
            {
                RecipeComponent[1].transform.GetChild(1).gameObject.SetActive(true);

                RecipeComponent[1].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                RecipeComponent[1].transform.GetChild(1).gameObject.SetActive(false);

                RecipeComponent[1].transform.GetChild(2).gameObject.SetActive(true);
            }

            if(recipeValue[2] == 7)
            {
                RecipeComponent[2].transform.GetChild(1).gameObject.SetActive(true);

                RecipeComponent[2].transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                RecipeComponent[2].transform.GetChild(1).gameObject.SetActive(false);

                RecipeComponent[2].transform.GetChild(2).gameObject.SetActive(true);
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

        public void ChangeDescriptionMethod()
        {
            switch(KitchenStateManager.CurrentKitchenState)
            {
                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_1:

                        methodDescript.text = "1. ผสมหมูสับกับพริกไทยขาวป่น และซีอิ๊วขาว คลุกเคล้าให้เข้ากัน";
                        
                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_2:

                        methodDescript.text = "2. ต้มน้ำเปล่า ใส่กระเทียม เมื่อน้ำเดือดปั้นหมูที่หมักไว้ ให้เป็นก้อนกลม ก่อนใส่หม้อลงไปต้มให้สุก";

                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_3:

                        methodDescript.text = "3. ปรุงรสด้วยซีอิ๊วขาว น้ำตาล แล้วใส่ผักกาดขาว กับ แครอทลองไปต้มจนสุก.";
                        
                        break;

                    case Enumerators.KichenState.CLEARSOUP_SEQUENCE_4:

                        methodDescript.text = "4. ใส่เต้าหู้หลอดลงไป แล้วโรยด้วยผักคื่นฉ่าย จากนั้นปิดไฟแล้วตักใส่จานเสิร์ฟ";
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_1:

                        methodDescript.text = "1. โขลกกระเทียมและพริกให้หยาบ";

                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_2:

                        methodDescript.text = "2. นำกระทะตั้งไฟให้ร้อนและใส่น้ำมันพืชลงไปเมื่อน้ำมันร้อนใส่กระเทียมและพริกที่โขลกลงไปผัดจนหอ.";
                        
                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_3:

                        methodDescript.text = "3. ใส่หมูลงไปผัดให้สุก แล้วใส่น้ำเปล่าลงไปเล็กน้อย ปรุงด้วย ซอสหอยนางรม  ซอสถั่วเหลือง ซีอิ๊วดำ และ น้ำตาล";

                        break;

                    case Enumerators.KichenState.STIRFRIED_SEQUENCE_4:
                    
                        methodDescript.text = "4. ปิดไฟใส่ใบกะเพรา แล้วตักใส่จานเสิร์ฟ";
                        
                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_1:

                        methodDescript.text = "1. นำกระทะตั้งไฟให้ร้อนและใส่น้ำมัีนพืชลงไป เมื่อน้ำมันร้อน ใส่กระเทียมสับลงไปผัดจนหอม แล้วใส่อกไก้กับไข่ลงไป";
                        
                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_2:

                        methodDescript.text = "2. พอไข่เริ่มสุกแล้วให้เติมข้าวลงไปผัดให้เข้ากันและปรุงรสด้วยซอสถั่วเหลือง และ น้ำตาล";

                        break;

                    case Enumerators.KichenState.FRIEDRICE_SEQUENCE_3:

                        methodDescript.text = "3. ใส่ข้าวโพด ถั่วลันเตา และ แครอท ลงไปผัดให้เข้ากันจนสุก จากนั้นตักใส่จานเสิร์ฟ";

                        return;
            }
        }

        public void SendRecipeToState(string index)
        {
            AppStateManager.recipeSelectedFromAppState = index;
            
            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_KITCHEN_COOK);
        }

        public void FetchedResultIndexPanel()
        {
            for(int i = 0; i < 3; i++)
            {
                ResultIndexPanel[i].SetActive(false);
            }

            switch(AppStateManager.recipeSelectedFromAppState)
            {
                case "CLEARSOUP":

                    ResultIndexPanel[0].SetActive(true);

                    break;

                case "STIRFRIED":

                    ResultIndexPanel[1].SetActive(true);

                    break;

                case "FRIEDRICE":

                    ResultIndexPanel[2].SetActive(true);
                    
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
                case "INGREDIANT_BASIL":

                    imageIndex = 0;

                    break;

                case "INGREDIANT_CABBAGE":

                    imageIndex = 1;

                    break;

                case "INGREDIANT_CARROT":

                    imageIndex = 2;

                    break;
                
                case "INGREDIANT_CELERY":

                    imageIndex = 3;

                    break;

                case "INGREDIANT_CHICKEN":

                    imageIndex = 4;

                    break;

                case "INGREDIANT_CHILI":

                    imageIndex = 5;

                    break;

                case "INGREDIANT_CORN":

                    imageIndex = 6;

                    break;

                case "INGREDIANT_CUCUMBER":

                    imageIndex = 7;

                    break;

                case "INGREDIANT_EGG":

                    imageIndex = 8;

                    break;

                case "INGREDIANT_GALIC":

                    imageIndex = 9;

                    break;

                case "INGREDIANT_PEAS":

                    imageIndex = 10;

                    break;

                case "INGREDIANT_PORK":

                    imageIndex = 11;

                    break;

                case "INGREDIANT_RICE":

                    imageIndex = 12;

                    break;

                case "INGREDIANT_TOFU":

                    imageIndex = 13;

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

