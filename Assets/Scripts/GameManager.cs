using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.IO;
using UnityEngine;


[Serializable]
public class SaveData
{
    public float blood;
    public int eyes;
    public int killed;
}


public class GameManager : SingletonPersistent<MonoBehaviour>
{
    [SerializeField] private TextMeshProUGUI eyesText;

    private string savePath;
    private SaveData saveData;
    
    
    // Start is called before the first frame update
    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "saveData.json");
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        saveData.eyes += 1;
        eyesText.text = saveData.eyes.ToString();

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
            saveData = new SaveData();
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
