using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteraction : MonoBehaviour
{
    #region Unity Declarations

        [Header("Setup Item Section")]

        [SerializeField] private float turnRate = 60f;

        public Item.ItemType itemTypeSetup;

    #endregion

    #region Unity Methods

        private void OnTriggerEnter(Collider other) 
        {
            if(other.tag == "Player")
            {
                PlayerManager.Instance.AddItem(new Item {itemType = itemTypeSetup, amount = 1});

                Destroy(gameObject);
            }
        }

        private void Update() 
        {
            transform.Rotate(0, turnRate * Time.deltaTime, 0);  
        }

    #endregion
}
