using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public int maxAmmoCount = 20;
    int currentAmmoCount;
    public GameObject defaultProjectile;
    public GameObject dementorProjectile;
    public GameObject crateProjectile;

    public float projectileSpeed = 100;
    public float bulletRange = 20;

    public AudioClip projectileSound;
    public AudioClip noAmmoSound;

    [Header("Crosshair Settings")]
    public Image crosshairImage;
    public float animationSpeed = 5; //how fast the reticle color changes
    //public Color targetColorDementor;
    //Color originalCrosshairColor;
    Vector3 originalCrosshairScale;

    private GameObject currentProjectile;

    Color currentCrosshairColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //originalCrosshairColor = crosshairImage.color;
        originalCrosshairScale = crosshairImage.transform.localScale;

        if (defaultProjectile)
            currentProjectile = defaultProjectile;

        UpdateCrosshairColor();

        currentAmmoCount = maxAmmoCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }

    void FixedUpdate()
    {
        if (!crosshairImage)
            return;

        CrosshairEffect();
    }

    void Shoot()
    {
        if (currentProjectile) {
            if (currentAmmoCount > 0) {
                GameObject spell = Instantiate(currentProjectile, transform.position + transform.forward, transform.rotation);

                Rigidbody rb = spell.GetComponent<Rigidbody>();

                if (rb) {
                    rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);
                }

                if (projectileSound) {
                    AudioSource.PlayClipAtPoint(projectileSound, GameObject.FindGameObjectWithTag("Player").transform.position);
                }

                spell.transform.SetParent(transform);

                currentAmmoCount -= 1;
                GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().UpdateAmmoCount(currentAmmoCount);
            } else {
                if (noAmmoSound) {
                    AudioSource.PlayClipAtPoint(noAmmoSound, GameObject.FindGameObjectWithTag("MainCamera").transform.position);
                }
            }
        }
        
    }

    // raycasting to detect if we're looking at something as well as what we're looking at
    void CrosshairEffect()
    {
        RaycastHit hit;

        //camera position, forward direction from camera
        //out hit = the variable to store the output of the Raycast() call into
        //Math.Infinity is an option for the maxDistance argument
        if (Physics.Raycast(transform.position, transform.forward, out hit, bulletRange))
        {
            // Debug.Log("Hit something: " + hit.collider.name);

            if (hit.collider.CompareTag("Dementor"))
            {
                currentProjectile = dementorProjectile;
                UpdateCrosshairColor();
                CrosshairAnimation(originalCrosshairScale / 2, currentCrosshairColor, animationSpeed);
                
            } else if (hit.collider.CompareTag("Destructable")) {
                currentProjectile = crateProjectile;
                UpdateCrosshairColor();
                CrosshairAnimation(originalCrosshairScale / 2, currentCrosshairColor, animationSpeed);
            }
        } else {
            currentProjectile = defaultProjectile;
            UpdateCrosshairColor();
            CrosshairAnimation(originalCrosshairScale, currentCrosshairColor, animationSpeed);
        }
    }

    void CrosshairAnimation(Vector3 targetScale, Color targetColor, float speed)
    {
        var step = speed * Time.deltaTime;

        //animate crosshair
        crosshairImage.color = Color.Lerp(crosshairImage.color, targetColor, step);

        crosshairImage.transform.localScale = Vector3.Lerp(crosshairImage.transform.localScale,
                                                            targetScale,
                                                            step);
    }

    void UpdateCrosshairColor()
    {
        currentCrosshairColor = currentProjectile.GetComponent<Renderer>().sharedMaterial.color;
    }

    public void AddAmmo(int amount)
    {
        currentAmmoCount += amount;
        currentAmmoCount = Mathf.Clamp(currentAmmoCount, 0, 20);
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().UpdateAmmoCount(currentAmmoCount);
    }
}
