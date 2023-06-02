using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] Tower tower;
    ParticleSystem projectile;

    int damage = 1;
    public int Damage { get { return damage; } }

    float speed;

    private void Start() {
        projectile = GetComponent<ParticleSystem>();

        damage = tower.TowerInfo.projectileDamage;
        speed = tower.TowerInfo.projectileSpeed;

        SetProjectileSpeed();
    }

    void SetProjectileSpeed()
    {
        var projStats = projectile.main;
        projStats.startSpeed = speed;
    }
}
