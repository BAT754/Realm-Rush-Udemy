using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] List<Waypoint> path = new List<Waypoint>();
    float movementSpeed = 1f;

    Enemy enemy;

    private void Start() {
        enemy = GetComponent<Enemy>();
        movementSpeed = enemy.EnemyInfo.speed;
    }

    // Start is called before the first frame update
    private void OnEnable() 
    {
        FindPath();
        PlaceAtStart();
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        foreach(Waypoint waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;

            transform.LookAt(endPos);

            if (Vector3.Distance(transform.position, waypoint.transform.position) < Mathf.Epsilon)
                travelPercent = 1;

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    void FindPath()
    {
        path.Clear();

        GameObject activePath = GameObject.FindGameObjectWithTag("Path");

        foreach (Transform child in activePath.transform)
        {
            Waypoint waypoint = child.GetComponent<Waypoint>();

            if (waypoint != null)
            {
                path.Add(waypoint);
            }
        }
    }

    void PlaceAtStart()
    {
        transform.position = path[0].transform.position;
    }

    void FinishPath()
    {
        enemy.StealGold();
        enemy.DisableEnemy(gameObject);
        // gameObject.SetActive(false);
    }
}
