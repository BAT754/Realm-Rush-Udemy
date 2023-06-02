using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RangeFinder : MonoBehaviour
{
    float range = 30f;
    MeshRenderer mesh;
    TargetLocator targetLocator;

    Tower tower;

    private void Start() {
        tower = GetComponentInParent<Tower>();
        targetLocator = GetComponentInParent<TargetLocator>();
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = false;

        range = tower.TowerInfo.range;
        ResizeRange();
    }

    private void Update() {

        // Only do this stuff if the game isn't actually running
        if (!Application.isPlaying)
        {
            ResizeRange();
        }
    }

    void ResizeRange()
    {
        transform.localScale = new Vector3(range, 0.1f, range);
    }

    private void OnTriggerEnter(Collider other) {
        // If the collision dection picks up something that's not an enemy, ignore it (AKA the ground)
        if (other.gameObject.GetComponent<Enemy>() == null)
            return;

        // We didn't load in the targetLocator yet. Shouldn't be an issue, but just want to be safe.
        if (targetLocator == null)
            return;

        Enemy target = other.gameObject.GetComponent<Enemy>();

        targetLocator.AddToTargetList(target);
    }

    private void OnTriggerExit(Collider other) {
        // If the collision dection picks up something that's not an enemy, ignore it (AKA the ground)
        if (other.gameObject.GetComponent<Enemy>() == null)
            return;

        // We didn't load in the targetLocator yet. Shouldn't be an issue, but just want to be safe.
        if (targetLocator == null)
            return;

        Enemy target = other.gameObject.GetComponent<Enemy>();

        targetLocator.RemoveFromTargetList(target);
    }

    public void ToggleMeshVisability()
    {
        mesh.enabled = !mesh.enabled;
    }
}
