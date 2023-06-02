using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyStats enemyInfo;
    public EnemyStats EnemyInfo { get { return enemyInfo; }}

    int goldReward;
    int goldPenalty;

    Bank bank;
    ObjectPool pool;

    // Start is called before the first frame update
    void Start()
    {
        bank = FindObjectOfType<Bank>();
        goldReward = enemyInfo.reward;
        goldPenalty = enemyInfo.steals;
    }

    public void GiveReward()
    {
        if (bank == null)
            return;

        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null)
            return;
            
        bank.Withdraw(goldPenalty);
    }

    public void DisableEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        pool.DecreaseActiveEnemies();
    }

    // Lets the pool tell the enemy who it's pool is (in case I have two pools/spawners in the future stages)
    public void GetObjectPool(ObjectPool value)
    {
        pool = value;
    }
}
