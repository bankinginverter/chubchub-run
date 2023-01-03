using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayerGameplay(PlayerManager playerManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.ItemData";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerItemData data = new PlayerItemData(playerManager);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static void SavePlayerHealthData(PlayerManager playerManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/player.HealthData";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerHealthData data = new PlayerHealthData(playerManager);

        formatter.Serialize(stream, data);

        stream.Close();
    }

    public static PlayerItemData LoadPlayerGameplay()
    {
        string path = Application.persistentDataPath + "/player.ItemData";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerItemData data = formatter.Deserialize(stream) as PlayerItemData;

            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static PlayerHealthData LoadPlayerHealth()
    {
        string path = Application.persistentDataPath + "/player.HealthData";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerHealthData data = formatter.Deserialize(stream) as PlayerHealthData;

            stream.Close();

            MainUnityLifeCycle.Instance.SetNewPlayerState(false);

            return data;
        }
        else
        {
            MainUnityLifeCycle.Instance.SetNewPlayerState(true);

            return null;
        }
    }
}