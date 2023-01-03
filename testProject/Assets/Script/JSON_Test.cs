using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
//JSON TEST
public class JSON_Test : MonoBehaviour
{
    public Data player;
    [ContextMenu("To Json Data")]
    void SavePlayerDataToJson()
    {
        string jsonData = JsonUtility.ToJson(player, true);
        string path = Path.Combine(Application.dataPath, "player_data.json");
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")]
    void LoadPlayerDataToJson()
    {
        string path = Path.Combine(Application.dataPath, "player_data.json");
        string jsonData = File.ReadAllText(path);
        player = JsonUtility.FromJson<Data>(jsonData);
    }
}

[System.Serializable]
public class Data
{
    public string name;
    public int level;
    public int coin;
    public bool skill;
}
