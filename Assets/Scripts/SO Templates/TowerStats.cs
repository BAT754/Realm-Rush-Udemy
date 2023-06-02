using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class TowerStats : ScriptableObject
{
    [Header("Instance Info")]
    [Range(0f, 100f)] public float range;
    public int projectileDamage;
    public float projectileSpeed;

    [Header("Store Info")]
    public int price;
    public string towerName;

}
