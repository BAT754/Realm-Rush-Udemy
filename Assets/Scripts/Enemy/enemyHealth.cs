using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class enemyHealth : MonoBehaviour
{
    int maxHitPoints = 5;
    int currentHitPoints = 0;

    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
        maxHitPoints = enemy.EnemyInfo.maxHealth;
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void OnParticleCollision(GameObject other) 
    {
        Projectile projectile = other.gameObject.GetComponent<Projectile>();
        ProcessHit(projectile.Damage);
    }

    void ProcessHit(int damage)
    {
        currentHitPoints -= damage;

        if (currentHitPoints <= 0)
        {
            enemy.GiveReward();
            enemy.DisableEnemy(gameObject);
        }
    }
}
