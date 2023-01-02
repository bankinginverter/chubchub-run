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
        }

        [Header("Game Screen Section")]

        [SerializeField] private GameObject loadingScreenComponent;
        
        [SerializeField] private GameObject registerScreenComponent;

        [SerializeField] private GameObject mainmenuScreenComponent;

        [Header("Retrieve Data Section")]

        private Coroutine checkingPlayerInputCoroutine;

        private string nameRetrieved;

        private string weightRetrieved;

        private string heightRetrieved;

        private string ageRetrieved;

        private string genderRetrieved;

        [SerializeField] private GameObject confirmDataButton;

        [Header("Progress Loading Section")]

        [SerializeField] private Slider progressLoadingBar;

        [SerializeField] private TMP_Text progressLoadingText;

        [Header("MainMenu Screen Section")]

        [SerializeField] private TMP_Text playerName;

    #endregion

    #region Application UI Management

        public void CloseAllComponent()
        {
            loadingScreenComponent.SetActive(false);

            registerScreenComponent.SetActive(false);

            mainmenuScreenComponent.SetActive(false);
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
        }

        public void SetActiveMainmenuScreenPanel()
        {
            mainmenuScreenComponent.SetActive(true);
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
            
            PlayerManager.Instance.SyncHealthDataFromRegister(nameRetrieved, weightRetrieved, heightRetrieved, ageRetrieved, genderRetrieved);

            AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_MAINMENU);
        }

        public void SetupMainMenuUI()
        {
            playerName.text = PlayerManager.Instance.PlayerNameData;
        }

        public void PlayRunGame()
        {
            MainUnityLifeCycle.Instance.LoadGameScene("SCENE_GAMEPLAY");
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

    #region Data Retrieve UI

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
 
    #endregion
}

