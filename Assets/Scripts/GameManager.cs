using System;
using System.IO;
using UnityEngine;

[Serializable]
public class SaveData
{
    public float blood;
    public float damageMultiplier;
    public float attackSpeedMultiplier;
    public int eyes;
    public int killed;
    public int weapon;
    public bool[] purchasedUpgrades;

    public SaveData(bool[] _purchasedUpgrades)
    {
        purchasedUpgrades = _purchasedUpgrades;
    }
}

public class GameManager : SingletonPersistent<GameManager>
{
    private string savePath;
    public SaveData saveData;

    // Start is called before the first frame update
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    private void Load()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            saveData = new SaveData(new bool[ClickerManager.Instance.ConfigScriptableObject.upgrades.Length]);
        }
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(savePath, json);
    }

    private void Quit()
    {
        Application.Quit();
    }
    
    private void OnApplicationQuit()
    {
        Save();
    }
}
