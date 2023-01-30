using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameplayManager : MonoBehaviour
{
    #region Unity Declarations

        public static KitchenGameplayManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        [Header("Clearsoup Data Section")]

        [SerializeField] private GameObject[] clearSoupPrefabs;

        [Header("Stirfried Data Section")]

        [SerializeField] private GameObject[] stirFriedPrefabs;

        [Header("Friedrice Data Section")]

        [SerializeField] private GameObject[] friedRicePrefabs;
        
        [Header("Spawner Control Section")]
        
        [SerializeField] private Transform spawningTarget;

        [SerializeField] private GameObject mainCamera;

        private GameObject recentPrefabSpawn;

        private int targetScore;

        private int currentScore;

    #endregion

    #region Private Methods

        public void ClearDataKitchenGameplay()
        {
            if(recentPrefabSpawn != null)
            {
                Destroy(recentPrefabSpawn);

                recentPrefabSpawn = null;
            }
        }

        public void SpawnPrefab(int index)
        {
            if(recentPrefabSpawn != null)
            {
                Destroy(recentPrefabSpawn);

                recentPrefabSpawn = null;
            }

            switch(AppStateManager.recipeSelectedFromAppState)
            {
                case "CLEARSOUP":

                    recentPrefabSpawn = Instantiate(clearSoupPrefabs[index], spawningTarget.transform.position, Quaternion.identity) as GameObject;

                    recentPrefabSpawn.transform.SetParent(spawningTarget.transform);

                    break;

                case "STIRFRIED":

                    recentPrefabSpawn = Instantiate(stirFriedPrefabs[index], spawningTarget.transform.position, Quaternion.identity) as GameObject;

                    recentPrefabSpawn.transform.SetParent(spawningTarget.transform);

                    break;

                case "FRIEDRICE":

                    recentPrefabSpawn = Instantiate(friedRicePrefabs[index], spawningTarget.transform.position, Quaternion.identity) as GameObject;

                    recentPrefabSpawn.transform.SetParent(spawningTarget.transform);

                    break;
            }
        }

        public void SetupRaycastComponent(bool index)
        {
            mainCamera.GetComponent<RayCastingToIngredients>().enabled = index;
        }

        public void SetupScorePoint(int targetScoreIndex)
        {
            currentScore = 0;

            targetScore = targetScoreIndex;
        }

        public void ValidateScore()
        {
            if(currentScore == targetScore)
            {
                AppUIManager.Instance.NextSequenceButtonSetting(true);
            }
            else
            {
                AppUIManager.Instance.NextSequenceButtonSetting(false);
            }
        }

        public void ResetState()
        {
            KitchenStateManager.Instance.SetCurrentKitchenState(KitchenStateManager.CurrentKitchenState);
        }

        public void NextState()
        {
            if(KitchenStateManager.NextKitchenState == Enumerators.KichenState.NONE)
            {
                AppStateManager.Instance.SetCurrentAppState(Enumerators.AppState.APP_KITCHEN_RESULT);

                return;
            }
            KitchenStateManager.Instance.SetCurrentKitchenState(KitchenStateManager.NextKitchenState);
        }

        public void PlusCurrentScore()
        {
            currentScore++;
        }

    #endregion
}
