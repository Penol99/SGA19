using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
public static class Scr_save_data
{
    public static void SavePlayer(Scr_living_stats player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.ssf";
        FileStream stream = new FileStream(path, FileMode.Create);

        Scr_data_player data = new Scr_data_player(player);

        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static Scr_data_player LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.ssf";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Scr_data_player data = formatter.Deserialize(stream) as Scr_data_player;
            stream.Close();

            return data;
        } else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
