using System.IO;
using UnityEngine;

public class GameManager : SingletonPersistent<GameManager>
{
    private string savePath;

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
            ClickerManager.Instance.saveData = JsonUtility.FromJson<SaveData>(json);
        }
        else
        {
            ClickerManager.Instance.InitSaveData();
        }
    }

    private void Save()
    {
        string json = JsonUtility.ToJson(ClickerManager.Instance.saveData);
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
