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

        [HideInInspector] public List<Item> itemList;

        [HideInInspector] public string PlayerNameData;

        [HideInInspector] public string GenderData;

        [HideInInspector] public string WeightData;

        [HideInInspector] public string HeightData;

        [HideInInspector] public string AgeData;

    #endregion

    #region Unity Methods

        private void Start() 
        {
            itemList = new List<Item>();
        }

    #endregion

    #region Private Methods

        public void AddItem(Item item)
        {
            bool itemAlreadyInInventory = false;

            foreach(Item inventoryItem in itemList)
            {
                if(inventoryItem.itemType == item.itemType)
                {
                    inventoryItem.amount += item.amount;

                    itemAlreadyInInventory = true;
                }
            }
            if(!itemAlreadyInInventory)
            {
                itemList.Add(item);
            }
        }

        public void SavePlayerGameplay()
        {
            SaveSystem.SavePlayerGameplay(this);
        }

        public void LoadPlayer()
        {
            PlayerItemData data = SaveSystem.LoadPlayerGameplay();

            for(int i = 0; i < data.itemListData.Count; i++)
            {
                Debug.Log(data.itemListData[i].itemType + " amount : " + data.itemListData[i].amount);
            }
        }

        public void SaveGameAfterEndGame()
        {
            #region LoadGameData

                PlayerItemData data = SaveSystem.LoadPlayerGameplay();

            #endregion

            #region ImplementNewData

                List<Item> itemListToSave = itemList.Union<Item>(data.itemListData).ToList<Item>();

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
                itemList = itemListToSave;

                for(int i = 0; i < itemList.Count; i++)
                {
                    Debug.Log(itemList[i].itemType + " amount " + itemList[i].amount);
                }

            #endregion

            #region SaveGameData

                SaveSystem.SavePlayerGameplay(this);

            #endregion
        }

        public void SavePlayerHealthData()
        {
            SaveSystem.SavePlayerHealthData(this);
        }

        public void LoadPlayerHealth()
        {
            PlayerHealthData data = SaveSystem.LoadPlayerHealth();

            if(data != null)
            {
                StartDataRetrieve(data);
            }
        }

        public void StartDataRetrieve(PlayerHealthData data)
        {
            PlayerNameData = data.player_Name;

            WeightData = data.player_Weight;

            HeightData = data.player_Height;

            AgeData = data.player_Age;
            
            GenderData = data.player_Gender;
        }

        public void SyncHealthDataFromRegister(string nameIndex, string weightIndex, string heightIndex, string ageIndex, string genderIndex)
        {
            PlayerNameData = nameIndex;

            WeightData = weightIndex;

            HeightData = heightIndex;

            AgeData = ageIndex;
            
            GenderData = genderIndex;

            SavePlayerHealthData();
        }

    #endregion


            

}
