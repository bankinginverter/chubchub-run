using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastingToIngredients : MonoBehaviour
{
    #region Unity Declarations

        [SerializeField] private Camera mainCamera;

    #endregion

    #region Private Methods

        private void Update() 
        {
            Ray raycast = mainCamera.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(raycast, out RaycastHit hit))
            {
                if(hit.transform.tag == "Ingredient")
                {
                    AppUIManager.Instance.SetItemText(hit.transform.gameObject.GetComponent<KitchenIngredient>().GetItemIndicatorString());

                    if(Input.GetMouseButtonDown(0))
                    {
                        hit.transform.gameObject.GetComponent<KitchenIngredient>().TransformItem();

                        hit.transform.tag = "Untagged";

                        SoundManager.Instance.SetFXAudio(0);
                    }
                }
            }
            else
            {
                AppUIManager.Instance.SetItemText("No Ingredient selected");
            }
        }

    #endregion
}
