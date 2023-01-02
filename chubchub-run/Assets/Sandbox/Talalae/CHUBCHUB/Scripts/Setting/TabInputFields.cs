using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabInputFields : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private TMP_InputField[] inputFieldActived;

        private int inputSelected;

    #endregion


    #region Unity Methods

        private void Update() 
        {
            if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKeyDown(KeyCode.LeftShift))
            {
                inputSelected--;

                if(inputSelected < 0) 
                {
                    inputSelected = inputFieldActived.Length - 1;
                }

                SelectInputField();
            }
            else if(Input.GetKeyDown(KeyCode.Tab))
            {
                inputSelected++;

                if(inputSelected > inputFieldActived.Length - 1)
                {
                    inputSelected = 0;
                }

                SelectInputField();
            }

            void SelectInputField()
            {
                inputFieldActived[inputSelected].Select();
            }
        }
    
    #endregion


    #region Private Methods

        public void OnSelectedInputField(int index) => inputSelected = index;
        
    #endregion
}
