using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Unity Declarations

        public static PlayerManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [HideInInspector] public List<Item> GAME_itemList;

        private List<Item> MATCH_itemList;
        
        [HideInInspector] public List<MatchMaking> matchList;

        [HideInInspector] public string PlayerNameData;

        [HideInInspector] public string GenderData;

        [HideInInspector] public string WeightData;

        [HideInInspector] public string HeightData;

        [HideInInspector] public string AgeData;

        [HideInInspector] public int CostumeData;
        
        private bool initialFileValid;

    #endregion

    #region Unity Methods

        private void Start() 
        {
            GAME_itemList = new List<Item>();

            MATCH_itemList = new List<Item>();

            matchList = new List<MatchMaking>();
        }

    #endregion

    #region Private Methods

        public void AddItem(Item item)
        {
            bool itemAlreadyInInventory = false;

            foreach(Item inventoryItem in GAME_itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;

                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
            {
                GAME_itemList.Add(item);
            }
        } 

        public void AddMatchMaking(MatchMaking matchMaking)
        {
            matchList.Add(matchMaking);
        }

        public int MatchFetchingData()
        {
            PlayerMatchData matchdata = SaveSystem.LoadPlayerMatch();

            if(matchdata != null)
            {
                return matchdata.matchListData.Count;
            }
            else
            {
                return 0;
            }
        }

        public void MatchItemFecth()
        {
            MATCH_itemList = GAME_itemList;
        }

        public int ItemFetchingData()
        {
            PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

            if(itemdata != null)
            {
                return itemdata.itemListData.Count;
            }
            else
            {
                return 0;
            }
        }

        public string SpecificItemData_NAME(int index)
        {
            PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

            return itemdata.itemListData[index].itemType.ToString();
        }

        public int SpecificItemData_AMOUNT(int index)
        {
            PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

            return itemdata.itemListData[index].amount;
        }

        public void SaveGameAfterEndGame()
        {
            #region LoadGameData

                PlayerItemData itemdata = SaveSystem.LoadPlayerGameplay();

                PlayerMatchData matchdata = SaveSystem.LoadPlayerMatch();

            #endregion

            #region ImplementNewData

                GameplayUIManager.Instance.EndGameResultUI(matchList[0].matchmaking_time, matchList[0].matchmaking_count);

                if(initialFileValid)
                {
                    SaveSystem.SavePlayerGameplay(this);

                    SaveSystem.SavePlayerMatchData(this);

                    return;
                }

                List<MatchMaking> matchListToSave = matchdata.matchListData.Union<MatchMaking>(matchList).ToList<MatchMaking>();

                List<Item> itemListToSave = GAME_itemList.Union<Item>(itemdata.itemListData).ToList<Item>();

                for(int i = 0; i < itemListToSave.Count; i++)
                {
                    for(int j = i + 1; j < itemListToSave.Count; j++)
                    {
                        if(itemListToSave[i].itemType == itemListToSave[j].itemType)
                        {
                            itemListToSave[i].amount += itemListToSave[j].amount;

                            itemListToSave.Remove(itemListToSave[j]);
                        }
                    }
                }

                GAME_itemList = itemListToSave;

                matchList = matchListToSave;

                for(int i = 0; i < GAME_itemList.Count; i++)
                {
                    Debug.Log(GAME_itemList[i].itemType + " amount " + GAME_itemList[i].amount);
                }

            #endregion

            #region SaveGameData

                SaveSystem.SavePlayerGameplay(this);

                SaveSystem.SavePlayerMatchData(this);

            #endregion
        }

        public void OverwriteSaveData(PlayerItemData index)
        {
            GAME_itemList = index.itemListData;

            SavePlayerGameplay();
        }

        public void InitialFileValidSetup(bool index)
        {
            initialFileValid = index;
        }

        public void StartHealthDataRetrieve(PlayerHealthData data)
        {
            PlayerNameData = data.player_Name;

            WeightData = data.player_Weight;

            HeightData = data.player_Height;

            AgeData = data.player_Age;
            
            GenderData = data.player_Gender;

            CostumeData = data.player_Costume;
        }

        public void SyncHealthDataFromRegister(string nameIndex, string weightIndex, string heightIndex, string ageIndex, string genderIndex, int costumeIndex)
        {
            PlayerNameData = nameIndex;

            WeightData = weightIndex;

            HeightData = heightIndex;

            AgeData = ageIndex;
            
            GenderData = genderIndex;

            CostumeData = costumeIndex;

            SavePlayerHealthData();
        }

    #endregion

    #region Saving Methods

        public void SavePlayerGameplay()
        {
            SaveSystem.SavePlayerGameplay(this);
        }

        public void SavePlayerHealthData()
        {
            SaveSystem.SavePlayerHealthData(this);
        }

        public void SavePlayerMatchData()
        {
            SaveSystem.SavePlayerMatchData(this);
        }
    
    #endregion

    #region Loading Methods

        public void LoadPlayer()
        {
            PlayerItemData data = SaveSystem.LoadPlayerGameplay();

            for(int i = 0; i < data.itemListData.Count; i++)
            {
                Debug.Log(data.itemListData[i].itemType + " amount : " + data.itemListData[i].amount);
            }
        }

        public void LoadPlayerHealth()
        {
            PlayerHealthData data = SaveSystem.LoadPlayerHealth();

            if(data != null)
            {
                StartHealthDataRetrieve(data);
            }
        }

        public void LoadPlayerMatch(int index, out int timeRound, out float caloriesRound, out int day, out int month, out int year)
        {
            PlayerMatchData data = SaveSystem.LoadPlayerMatch();

            timeRound = data.matchListData[index].matchmaking_time;

            caloriesRound = data.matchListData[index].matchmaking_calories;

            day = data.matchListData[index].matchmaking_played_date;

            month = data.matchListData[index].matchmaking_played_month;

            year = data.matchListData[index].matchmaking_played_year;
        }
    
    #endregion
}
