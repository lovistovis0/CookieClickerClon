using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonPersistent<MonoBehaviour>
{
    private float blood;
    private int eyes;
    private int killed;
    
    public float Blood
    {
        get
        {
            return blood;
        }
        set
        {
            blood = value;
        }
    }
    
    public int Eyes
    {
        get
        {
            return eyes;
        }
        set
        {
            int diff = value - eyes;
            if (diff > 0)
            {
                killed += diff;
            }
            eyes = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Load();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("blood", blood);
        PlayerPrefs.SetInt("eyes", eyes);
        PlayerPrefs.SetInt("killed", killed);
    }
    
    private void Load()
    {
        blood = PlayerPrefs.GetFloat("blood");
        eyes = PlayerPrefs.GetInt("eyes");
        killed = PlayerPrefs.GetInt("killed");
    }

    private void OnApplicationQuit()
    {
        Save();
    }
}
