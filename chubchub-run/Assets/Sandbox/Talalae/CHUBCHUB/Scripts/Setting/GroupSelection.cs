using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupSelection : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private Button[] groupButton;

        [SerializeField] private Sprite[] imageButton;

        private int inputSelected;

    #endregion

    #region Unity Methods

        private void Start() 
        {
            OnSelectedButton(0);

            ContextTheGroup("BOY");
        }

    #endregion

    #region Private Methods

        public void SelectInputField()
        {
            for(int i = 0; i < groupButton.Length; i++)
            {
                if(i == inputSelected)
                {
                    groupButton[i].GetComponent<Image>().sprite = imageButton[1];
                }
                else
                {
                    groupButton[i].GetComponent<Image>().sprite = imageButton[0];
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

