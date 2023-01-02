using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerItemData
{
    public List<Item> itemListData;
    
    public PlayerItemData (PlayerManager playerManager)    
    {
        itemListData = PlayerManager.Instance.itemList;
    }
}
