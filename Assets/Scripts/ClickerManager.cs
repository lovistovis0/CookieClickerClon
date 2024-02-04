using TMPro;
using System.IO;
using UnityEngine;

public class Names
{
    public string[] names;
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
    private string[] names;

    // Start is called before the first frame update
    void Start()
    {
        NewEnemy();
        GetNames();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        float maxHealth = CalculateEnemyHealth(GameManager.Instance.saveData.killed);
        
        currentEnemyData = new EnemyData(maxHealth);
    }

    private void Damage()
    {
        // Do visual stuff
        
        
        // Actual stuff
        float damage = configScriptableObject.weapons[GameManager.Instance.saveData.weapon].baseDamage * GameManager.Instance.saveData.damageMultiplier;

        currentEnemyData.health -= damage;
        
        if (currentEnemyData.health <= 0)
        {
            currentEnemyData.health = 0;
            // TODO: Kill the enemy
        }
    }

    private float CalculateEnemyHealth(int killed)
    {
        return baseEnemyHealth + Mathf.Pow(enemyHealthIncrease, killed);
    }
}
