using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveData
{
    public int money;
    public int days;
    public bool[] boughtGoods;
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public SaveData saveData;
    public float time;
    public int money;

    [Header("Goods")]
    [SerializeField] GameObject[] goods;
    public bool[] boughtGoods;      //구매한 굳즈들 인덱스로 정리


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

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        this.money = saveData.money;
        //this.days = saveData.days;
        this.boughtGoods = saveData.boughtGoods;
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
    
    public void CalculateMoney(int i)
    {
        saveData.money += i;
        Save();
    }

    #region Scene

    //새로운 씬을 추가
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; 
    }

    //새로운 씬 안에 아래 내용을 새로 호출
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "GameScene")
        {
            CheckItem();
        }
    }

    //게임 종료시
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion

    private void CheckItem()
    {
        for (int i = 0; i < boughtGoods.Length; i++)
        {

        }
    }
}
