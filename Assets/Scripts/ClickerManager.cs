using TMPro;
using System.IO;
using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;
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
    [SerializeField] private TextMeshProUGUI killedText;
    [SerializeField] private TextMeshProUGUI signText;
    
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

        currentEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        bloodText.text = saveData.blood.ToString();
        eyesText.text = saveData.eyes.ToString();
        killedText.text = saveData.eyes.ToString();
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
        saveData.killed++;

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
            StartCoroutine(EnemyDeath());
        }
    }

    IEnumerator EnemyDeath()
    {
        RectTransform enemyTransform = currentEnemy.GetComponent<RectTransform>();
        Image enemyImage = currentEnemy.GetComponent<Image>();
        
        Vector3 oldPos = enemyTransform.position;
        for (int frame = 0; frame < 100; frame++)
        {
            Debug.Log(enemyTransform.position.y);
            enemyTransform.position = oldPos - new Vector3(0, Mathf.Sin(frame/60f) * 1000, 0);
            yield return null;
        }
        
        enemyTransform.position = oldPos;
            
        // Sign stuff
        signText.text = RandomName();
        
        NewEnemy();
        
        for (int frame = 0; frame < 60; frame++)
        {
            enemyImage.color = new Color(enemyImage.color.r, enemyImage.color.g, enemyImage.color.b,
                Mathf.Sin((frame/60f) * (Mathf.PI / 2f)));
            yield return null;
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
