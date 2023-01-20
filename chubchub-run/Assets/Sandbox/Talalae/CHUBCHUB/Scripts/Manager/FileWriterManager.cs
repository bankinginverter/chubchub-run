using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileWriterManager : MonoBehaviour
{
    #region Unity Declarations

        public static FileWriterManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        ReadWriteFile FileWriter;

    #endregion 

    #region Private Methods

        public void ExportDataFileToCSV()
        {
            FileWriter = new ReadWriteFile("_" + PlayerManager.Instance.PlayerNameData);

            FileWriter.CreateFile();

            FileWriter.WriteFile();
        }

    #endregion 
}
