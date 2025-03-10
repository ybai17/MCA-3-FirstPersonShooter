using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public float ballSpeed = 10;
    public int maxTargets = 5;

    //tracks how many more dementors this ball can hit
    int chargesLeft;

    Transform target;
    Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = PickTarget();

        Debug.Log("Found target: " + target);

        rb = GetComponent<Rigidbody>();
        chargesLeft = maxTargets;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (chargesLeft > 0) {
            if (target) {
                Vector3 targetDirection = Vector3.MoveTowards(transform.position, target.position, ballSpeed).normalized;
                rb.AddForce(targetDirection, ForceMode.VelocityChange);
                //transform.position = targetDirection;
            } else {
                target = PickTarget();

                if (!target)
                    Destroy(gameObject);
            }
        } else {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dementor")) {
            Debug.Log("HIT DEMENTOR");
            chargesLeft--;
            Debug.Log("charges left: " + chargesLeft);
        }
    }

    //TODO: two bludgers always pick the same target... need to fix
    Transform PickTarget()
    {
        GameObject[] allDementors = GameObject.FindGameObjectsWithTag("Dementor");

        if (allDementors.Length == 0) {
            return null;
        }

        int rollTargetIndex = Random.Range(0, allDementors.Length);

        Debug.Log(gameObject.name + " is targeting " + rollTargetIndex);

        return allDementors[rollTargetIndex].transform;
    }
}
