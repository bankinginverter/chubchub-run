using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneControllerManager : MonoBehaviour
{
    #region Unity Declarations

        public static SceneControllerManager Instance;

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

    #endregion

    #region Private Methods

        public void LoadGameScene(string index)
        {
            SceneManager.LoadScene(index);
        }

    #endregion
}
