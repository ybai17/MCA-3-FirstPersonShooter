using UnityEngine;

public class CastShield : MonoBehaviour
{
    public int maxShieldCount;
    int currentShieldCount;

    public AudioClip noShieldSFX;

    public GameObject shield;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentShieldCount = maxShieldCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (!shield)
            return;
            
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (currentShieldCount > 0) {
                shield.SetActive(true);
                currentShieldCount -= 1;
                GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().UpdateShieldCount(currentShieldCount);
            } else {
                AudioSource.PlayClipAtPoint(noShieldSFX, transform.position);
            }
        }
    }
}
