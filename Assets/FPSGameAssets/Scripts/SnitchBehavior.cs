using UnityEngine;

public class SnitchBehavior : MonoBehaviour
{
    //how high it will rise
    public int maxHeight;

    //how fast the snitch orbits the player
    public int orbitingSpeed;

    //how fast the snitch approaches the player once it's finished rising
    public int approachingSpeed;

    public int stoppingDistance;

    GameObject[] dementors;
    Rigidbody rb;
    Transform target;

    Vector3 risenTargetPosition;
    Vector3 approachingTargetPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("CALLING START");

        rb = GetComponent<Rigidbody>();
        //rb.angularVelocity = Vector3.zero;
        //rb.linearVelocity = Vector3.zero;

        transform.rotation = transform.parent.gameObject.transform.rotation;

        target = GameObject.FindGameObjectWithTag("Player").transform;

        Debug.Log("SNITCH Target: " + target);
    }

    // Update is called once per frame
    void Update()
    {
        dementors = GameObject.FindGameObjectsWithTag("Dementor");
        //Debug.Log("dementors length = " + dementors.Length);

        if (transform.position.y < maxHeight) {
            Vector3 moveUp = transform.position;
            moveUp.y = maxHeight;

            transform.position = Vector3.Lerp(transform.position, moveUp, orbitingSpeed * Time.deltaTime);
            return;
        }

        if (dementors.Length != 0) {
            Debug.Log("ORBITING");
            Orbit();
        } else {
            Debug.Log("SNITCH DONE ORBITING");
            ApproachPlayer();
        }
    }

    void Orbit()
    {
        risenTargetPosition = target.position;
        risenTargetPosition.y = maxHeight;

        transform.RotateAround(risenTargetPosition, Vector3.up, orbitingSpeed * Time.deltaTime);

        /*
        risenTargetPosition = target.position;
        risenTargetPosition.y = maxHeight;
        Vector3 targetDirection = Vector3.MoveTowards(risenTargetPosition, target.position, orbitingSpeed * Time.deltaTime);
        rb.AddForce(targetDirection * orbitingSpeed, ForceMode.VelocityChange);
        */
    }

    void ApproachPlayer()
    {
        approachingTargetPosition = target.position;
        approachingTargetPosition.y = -5f;

        Debug.Log("APPROACHING: " + approachingTargetPosition);

        if (Vector3.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, approachingTargetPosition, approachingSpeed * Time.deltaTime);
        }
        
    }
}
