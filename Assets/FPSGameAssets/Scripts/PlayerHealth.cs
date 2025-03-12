using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    private int currentHealth;

    public static bool IsAlive {get; private set;}

    public TMP_Text losingText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = startingHealth;
        IsAlive = true;

        losingText.enabled = false;
        UpdateHealthSlider();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        UpdateHealthSlider();

        Debug.Log("OW..." + currentHealth + " HP left");

        if (currentHealth <= 0 && IsAlive)
        {
            //player dies
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, 100);

        UpdateHealthSlider();

        Debug.Log("yum, healed: " + currentHealth + " HP left");
    }

    void Die()
    {
        IsAlive = false;

        Debug.Log("Player DIES");

        var audioSource = GetComponent<AudioSource>();

        if (audioSource)
        {
            audioSource.Play();
        }

        transform.Rotate(0, 0, -90, Space.Self);

        losingText.text = "YOU DIED";
        losingText.color = Color.red;
        losingText.enabled = true;

        Debug.Log("VICTORY TEXT: " + losingText.enabled);

        Invoke("ReloadSameScene", 5);
    }

    void UpdateHealthSlider()
    {
        GameObject.FindGameObjectWithTag("UI").GetComponent<UIManager>().UpdateHealthSlider(currentHealth);
    }

    void ReloadSameScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
