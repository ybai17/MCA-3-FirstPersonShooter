using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text ammoCount;
    public TMP_Text shieldCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthSlider(int newHealthAmt)
    {
        if (healthSlider)
        {
            healthSlider.value = newHealthAmt;
        }
    }

    public void UpdateAmmoCount(int newAmmoCount)
    {
        if (ammoCount)
        {
            ammoCount.text = "" + newAmmoCount;
        }
    }

    public void UpdateShieldCount(int newShieldCount)
    {
        if (shieldCount)
        {
            shieldCount.text = "" + newShieldCount;
        }
    }
}
