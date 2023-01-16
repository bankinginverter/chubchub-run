using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayUIManager : MonoBehaviour
{
    #region Unity Declarations

        public static GameplayUIManager Instance;

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

        [SerializeField] private GameObject gameplayScreenComponent;

        [SerializeField] private GameObject endGameScreenComponent;

        [Header("Gameplay Screen Section")]

        [SerializeField] private TMP_Text timeEstimateText;

        [SerializeField] private TMP_Text roundPlayedText;

        [SerializeField] private TMP_Text caloriesText;

        [Header("Endgame Screen Section")]

        [SerializeField] private TMP_Text result_timeEstimateText;

        [SerializeField] private TMP_Text result_roundPlayedText;

        [SerializeField] private TMP_Text result_caloriesText;

        private float caloriesFacetor = 0.1f;

    #endregion

    #region Application UI Management

        public void CloseAllComponent()
        {
            gameplayScreenComponent.SetActive(false);

            endGameScreenComponent.SetActive(false);
        }

    #endregion

    #region Specific UI Implements

        public void SetActiveGameplayScreenPanel()
        {
            gameplayScreenComponent.SetActive(true);
 
            roundPlayedText.text = (PlayerManager.Instance.MatchFetchingData() + 1).ToString();
        }

        public void SetActiveEndGameScreenPanel()
        {
            endGameScreenComponent.SetActive(true);
        }

        public void SyncDataTimer(int index)
        {
            int minuteTime = (index/60);

            int secondTime = (index%60);

            timeEstimateText.text = string.Format("{0:#00} : {1:#00}", minuteTime, secondTime);

            float caloriesCalculate = CalculateCaloriesWithFactor(index);

            caloriesText.text = string.Format("{0:#000.0}",caloriesCalculate);
        }

        public void EndGameResultUI(int indexTimeRec, int indexRoundRec)
        {
            int minuteTime = (indexTimeRec/60);

            int secondTime = (indexTimeRec%60);

            result_timeEstimateText.text = string.Format("{0:#00} : {1:#00}", minuteTime, secondTime);

            float caloriesCalculate = CalculateCaloriesWithFactor(indexTimeRec);

            result_caloriesText.text = string.Format("{0:#000.0}",caloriesCalculate);

            result_roundPlayedText.text = indexRoundRec.ToString(); 
        }

        public float CalculateCaloriesWithFactor(int indexTime)
        {
            return caloriesFacetor * indexTime;
        }

    #endregion
}
