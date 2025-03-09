using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float ballSpeed = 10;
    public int maxTargets = 5;

    Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = PickTarget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Transform PickTarget()
    {
        GameObject[] allDementors = GameObject.FindGameObjectsWithTag("Dementor");

        if (allDementors.Length == 0) {
            return null;
        }

        int rollTargetIndex = Random.Range(0, allDementors.Length);

        return allDementors[rollTargetIndex].transform;
    }
}
