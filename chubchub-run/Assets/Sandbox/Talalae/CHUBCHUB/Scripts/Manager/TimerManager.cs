using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    
    #region Unity Declarations

        public static TimerManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        [HideInInspector] public int setupTimer;

    #endregion

    #region Private Methods

        public void SetupPreparingTimer()
        {
            setupTimer = 90;
        }

        public void PlusOneSecond()
        {
            setupTimer += 1;

            if(setupTimer > 3600)
            {
                setupTimer = 3600;
            }

            AppUIManager.Instance.SyncTimerTextWithGUI();
        }

        public void MinusOneSecond()
        {
            setupTimer -= 1;

            if(setupTimer < 1)
            {
                setupTimer = 1;
            }

            AppUIManager.Instance.SyncTimerTextWithGUI();
        }

        public void PlusOneMinute()
        {
            setupTimer += 60;

            if(setupTimer > 3600)
            {
                setupTimer = 3600;
            }

            AppUIManager.Instance.SyncTimerTextWithGUI();
        }

        public void MinusOneMinute()
        {
            setupTimer -= 60;

            if(setupTimer < 1)
            {
                setupTimer = 1;
            }

            AppUIManager.Instance.SyncTimerTextWithGUI();
        }

    #endregion
}
