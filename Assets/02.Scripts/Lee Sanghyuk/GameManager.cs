using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData
{
    public int money;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public SaveData saveData;

    public static GameManager Instance1
    {
        get
        {
            if (Instance != null) return Instance;
            var obj = FindObjectOfType<GameManager>();
            if (obj != null)
            {
                Instance = obj;
            }
            else
            {
                var newObj = new GameObject().AddComponent<GameManager>();
                Instance = newObj;
            }
            return Instance;
        }
    }
    
    private void Awake()
    {
        var objs = FindObjectsOfType<GameManager>();
        if (objs.Length != 1)
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void Save()
    {
        var jsonData = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "save.json"), jsonData);
    }

    public void Load()
    {
        if (!File.Exists(Path.Combine(Application.persistentDataPath, "save.json")))
        {
            saveData = new SaveData();
            return;
        }
        var jsonData = File.ReadAllText(Path.Combine(Application.persistentDataPath, "save.json"));
        saveData = JsonUtility.FromJson<SaveData>(jsonData);
    }
    
    public void GetMoney(int i)
    {
        saveData.money += i;
    }
    
    public void SpendMoney(int i)
    {
        saveData.money -= i;
    }
}
