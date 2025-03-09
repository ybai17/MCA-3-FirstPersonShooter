using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collided with something");

        if (collision.gameObject.CompareTag("Projectile") && collision.gameObject.name.Equals("DefaultProjectile(Clone)")) {
            animator.SetTrigger("open");
            //transform.parent.GetChild(1).gameObject.SetActive(true);
            
            //debugging atm
            transform.parent.GetChild(1).GetChild(1).gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>().StopAllCoroutines();
        }
    }
}
