using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Info", fileName = "Wave x")]
public class WaveInfo : ScriptableObject
{
    [System.Serializable]
    public class WaveGroup
    {
        public GameObject enemy;
        public int amount;
        public float spawnRate;
    }

    public List<WaveGroup> wave = new List<WaveGroup>();
}


