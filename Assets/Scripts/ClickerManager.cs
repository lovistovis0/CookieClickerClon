using TMPro;
using System.IO;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Names
{
    public string[] names;
}

[Serializable]
public class SaveData
{
    public float blood;
    public float damageMultiplier = 1;
    public float bloodLimitMultiplier = 1;
    public float attackSpeedMultiplier = 1;
    public int eyes;
    public int killed;
    public int weapon;
    public bool[] purchasedUpgrades;

    public SaveData(bool[] _purchasedUpgrades)
    {
        purchasedUpgrades = _purchasedUpgrades;
    }
}

public class EnemyData
{
    public float totalHealth;
    public float health;

    public EnemyData(float _totalHealth)
    {
        totalHealth = _totalHealth;
        health = _totalHealth;
    }
}

public class ClickerManager : SingletonPersistent<ClickerManager>
{
    // Options
    [Header("Base Values")]
    [SerializeField] private float baseBloodLimit;
    [SerializeField] private float baseEnemyHealth;

    [Header("Options")]
    [SerializeField] private float enemyHealthIncrease;
    [SerializeField] private TextAsset namesJson;

    [Header("ScriptableObjects")]
    [SerializeField] private ConfigScriptableObject configScriptableObject;
    
    [Header("References")]
    [SerializeField] private TextMeshProUGUI bloodText;
    [SerializeField] private TextMeshProUGUI eyesText;
    
    // Properties
    public ConfigScriptableObject ConfigScriptableObject
    {
        private set => configScriptableObject = value;
        get => configScriptableObject;
    }
    
    // Private variables
    private EnemyData currentEnemyData;
    private GameObject currentEnemy;
    public SaveData saveData;
    private string[] names;

    // Start is called before the first frame update
    void Start()
    {
        NewEnemy();
        GetNames();
        saveData.blood = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bloodText.text = saveData.blood.ToString();
        eyesText.text = saveData.eyes.ToString();
    }
    
    public void InitSaveData()
    {
        saveData = new SaveData(new bool[configScriptableObject.upgrades.Length]);
    }
    
    private void GetNames()
    {
        names = JsonUtility.FromJson<Names>(namesJson.text).names;
    }
    
    private string RandomName() {
        return names[Random.Range(0, names.Length)];
    }

    private void NewEnemy()
    {
        // Actually spawn a prefab
        
        // The sign stuff
        
        
        // Internal
        float maxHealth = CalculateEnemyHealth(saveData.killed);
        
        currentEnemyData = new EnemyData(maxHealth);
    }

    public void Damage()
    {
        // Do visual stuff
        
        
        // Actual stuff
        float damage = configScriptableObject.weapons[saveData.weapon].baseDamage * saveData.damageMultiplier;

        float prevHealth = currentEnemyData.health;
        currentEnemyData.health -= damage;

        float newHealth = Mathf.Clamp(currentEnemyData.health, 0, currentEnemyData.totalHealth);
        
        // The actual inflicted damage is the gained blood
        GainBlood(prevHealth - newHealth);
        
        Debug.Log(currentEnemyData.health);
        
        if (currentEnemyData.health <= 0)
        {
            // TODO: Kill the enemy
            
            // Sign stuff
            
            NewEnemy();
        }
    }

    private void GainBlood(float amount)
    {
        saveData.blood = Mathf.Clamp(saveData.blood + amount, 0, baseBloodLimit * saveData.bloodLimitMultiplier);
    }

    private float CalculateEnemyHealth(int killed)
    {
        return baseEnemyHealth + Mathf.Pow(enemyHealthIncrease, killed);
    }
}
