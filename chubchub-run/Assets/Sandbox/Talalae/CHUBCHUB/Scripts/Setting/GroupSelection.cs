using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupSelection : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private Button[] groupButton;

        private int inputSelected;

    #endregion

    #region Private Methods

        public void SelectInputField()
        {
            for(int i = 0; i < groupButton.Length; i++)
            {
                if(i == inputSelected)
                {
                    groupButton[i].GetComponent<Image>().color = new Color32(0, 255, 37, 255);
                }
                else
                {
                    groupButton[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                }
            }
        }

        public void OnSelectedButton(int index) 
        {
            inputSelected = index; 

            SelectInputField();
        }

        public void ContextTheGroup(string context)
        {
            AppUIManager.Instance.ReadStringGenderInput(context);
        }

    #endregion
}

