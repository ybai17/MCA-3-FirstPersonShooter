using UnityEngine;

public class SpellLootBehavior : MonoBehaviour
{
    public int chargeAmount = 5;

    public AudioClip lootGetSFX;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);

        if (transform.position.y < Random.Range(0.1f, 1.0f))
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
            var playerAmmo = other.transform.GetChild(0).GetComponent<ShootProjectile>();

            if (playerAmmo) {
                if (lootGetSFX)
                    AudioSource.PlayClipAtPoint(lootGetSFX, transform.position);
                    
                playerAmmo.addAmmo(chargeAmount);
                Destroy(gameObject);
            }
        }
    }
}
