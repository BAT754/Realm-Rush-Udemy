using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] RangeFinder range;
    [SerializeField] TowerStats towerInfo;
    public TowerStats TowerInfo { get { return towerInfo; }}

    private void OnMouseDown() {
        range.ToggleMeshVisability();
    }
}
