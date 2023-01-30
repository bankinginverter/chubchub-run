using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenIngredient : MonoBehaviour
{
    #region Unity Declarations

       [Header("Setup Ingredient Section")]

       [SerializeField] private Transform moveToKitchen;

       [SerializeField] private bool useGravity;

       [SerializeField] private string ItemIndicatorText;

    #endregion

    #region private Methods

        public void TransformItem()
        {
            KitchenGameplayManager.Instance.PlusCurrentScore();

            transform.position = moveToKitchen.position;

            transform.rotation = moveToKitchen.rotation;

            KitchenGameplayManager.Instance.ValidateScore();

            if(useGravity)
            {
                transform.GetComponent<Rigidbody>().useGravity = true;
            }
            else
            {
                Destroy(gameObject, 2f);
            }
        }

        public string GetItemIndicatorString()
        {
            return ItemIndicatorText;
        }

    #endregion
}
