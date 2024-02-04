using System;
using UnityEngine;

[Serializable]
public class Upgrade
{
    [Header("Info")]
    [Tooltip("The name of the upgrade.")] public string name;
    [Tooltip("The tooltip shown on hover.")] public string tooltip;
	
    [Header("Cost")]
    public int bloodCost;
    public int eyeCost;
	
    [Header("Effect")]
    [Tooltip("The increase to the damage multiplier which starts at 1 (additive).")] public float damageMultiplierIncrease = 0;
    [Tooltip("If the weapon will be modified.")] public bool changeWeapon = false;
    [Tooltip("The weapon to change to.")] public int weapon = 0;
    [Tooltip("The multiplier applied to the max blood limit (0/1 for none).")] public float bloodLimitMultiplier = 1;
    [Tooltip("The multiplier applied to the attack speed (0/1 for none).")] public float attackSpeedMultiplier = 1;
}

[Serializable]
public class Weapon
{
	[Header("Info")]
	public string name;
	
	[Header("Visuals")]
	public GameObject prefab;

	[Header("Effect")]
	public float baseDamage;
	public float baseAttackSpeed;
}

[Serializable]
public class Enemy
{
	[Header("Visuals")]
	public GameObject prefab;

	public float minHealth;
}

[CreateAssetMenu(fileName = "Config", menuName = "ScriptableObjects/ConfigScriptableObject", order = 1)]
[Serializable]
public class ConfigScriptableObject : ScriptableObject
{
	public Weapon[] weapons;
    public Upgrade[] upgrades;
}
