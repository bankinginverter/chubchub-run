using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class PlayerMatchData
{
    public List<MatchMaking> matchListData;
    
    public PlayerMatchData (PlayerManager playerManager)    
    {
        matchListData = PlayerManager.Instance.matchList;
    }
}
