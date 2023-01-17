using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerHealthData
{
    public string player_Name;

    public string player_Gender;
    
    public string player_Weight;

    public string player_Height;

    public string player_Age;

    public int player_Costume;
    
    public PlayerHealthData (PlayerManager playerManager)    
    {
        player_Name = PlayerManager.Instance.PlayerNameData;

        player_Gender = PlayerManager.Instance.GenderData;

        player_Weight = PlayerManager.Instance.WeightData;

        player_Height = PlayerManager.Instance.HeightData;

        player_Age = PlayerManager.Instance.AgeData;

        player_Costume = PlayerManager.Instance.CostumeData;
    }
}
