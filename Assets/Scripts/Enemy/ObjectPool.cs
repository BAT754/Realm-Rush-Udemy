using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    List<GameObject> ramPool;
    WaveInfo wave;

    int totalEnemies = 0;
    int activeEnemies = 0;
    public int ActiveEnemies { get { return activeEnemies; }}

    private void Awake() 
    {
        ramPool = new List<GameObject>();
    }

    private void Start() {
        ClearEnemyList();
        // StartCoroutine(SpawnRams());
    }

    IEnumerator SpawnRams()
    {
        for (int group = 0; group < wave.wave.Count; group++)
        {
            WaveInfo.WaveGroup currGroup = wave.wave[group];
            
            GameObject enemy = currGroup.enemy;
            int amountToSpawn = currGroup.amount;
            float spawnDelay = currGroup.spawnRate;

            for (int spawned = 0; spawned < amountToSpawn; spawned++)
            {
                CreateEnemy(enemy);
                yield return new WaitForSeconds(spawnDelay);
            }
            
        }
    }

    void CreateEnemy(GameObject enemy)
    {
        GameObject newEnemy = Instantiate(enemy, transform);
        newEnemy.GetComponent<Enemy>().GetObjectPool(this);
        ramPool.Add(newEnemy);
    }

    public void ClearEnemyList()
    {
        foreach (GameObject ram in ramPool)
        {
            Destroy(ram);
        }

        ramPool.Clear();

        totalEnemies = 0;
        activeEnemies = 0;
    }

    public void StartWave()
    {
        StartCoroutine(SpawnRams());
    }

    public void GetCurrentWave(WaveInfo newWave)
    {
        ClearEnemyList();
        wave = newWave;

        // Count up the total enemies
        foreach (WaveInfo.WaveGroup group in wave.wave)
        {
            totalEnemies += group.amount;
        }

        activeEnemies = totalEnemies;
    }

    public void DecreaseActiveEnemies()
    {
        activeEnemies -= 1;
    }
}
