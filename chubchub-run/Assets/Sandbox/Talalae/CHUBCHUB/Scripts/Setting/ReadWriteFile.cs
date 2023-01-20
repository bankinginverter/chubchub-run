using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class ReadWriteFile
{
    #region Unity Declarations

        private string _path = Application.dataPath;
        
        private string _filename = "";

        public ReadWriteFile(string filename)
        {
            _filename = filename;
        }

    #endregion 

    #region Private Methods

    public void CreateFile()
    {
        string createText = "Match NO. , Date (Day/Month/Year) , MatchLength (Minute:Second) , Calories (kCal)";

        File.WriteAllText(_path + _filename + ".csv", createText, Encoding.UTF8);
    }

    public void WriteFile()
    {
        PlayerMatchData myPlayerList = SaveSystem.LoadPlayerMatch();

        for (int i = 0; i < PlayerManager.Instance.MatchFetchingData(); i++)
        {
           File.AppendAllText(_path + _filename + ".csv", Environment.NewLine   + myPlayerList.matchListData[i].matchmaking_count 
                                                                                + "," + myPlayerList.matchListData[i].matchmaking_played_date + "/" + myPlayerList.matchListData[i].matchmaking_played_month + "/" + myPlayerList.matchListData[i].matchmaking_played_year 
                                                                                + "," + (myPlayerList.matchListData[i].matchmaking_time / 60) + ":" + (myPlayerList.matchListData[i].matchmaking_time % 60)
                                                                                + "," + myPlayerList.matchListData[i].matchmaking_calories
                                                                                , Encoding.UTF8);
        }
    }

    #endregion
}
