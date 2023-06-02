using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyStats : ScriptableObject
{
    public int maxHealth;
    [Range(0f, 10f)] public float speed;

    public int reward;
    public int steals;
}
