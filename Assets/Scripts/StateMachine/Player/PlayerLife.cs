using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using RPG.Saving;
using Newtonsoft.Json.Linq;

public class PlayerLife : MonoBehaviour, IJsonSaveable
{
    private bool isDied = false; 
    [Header("Player Health")]
    public float health;
    


    [Header("UI Elements")]
    public Image frontHealth;
    public Image backHealth;
    public float lerpTimer; 
    public float chipSpeed = 2f;
    private Color32 DamageHealthColor = new Color32 (219, 49, 49, 255);
    
    public static bool isAlive = true;



    private Animator anim;
    
    public static PlayerLife Instance { get; private set; }
    


    public void Awake()
    {
        isAlive = true;
        Instance = this;
        health = GetComponent<Health>().health;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        HealthBarColor();
        health = Mathf.Clamp(health, 0, GetComponent<Health>().health);
        UpdateHealthUI();
        if (health <= 0 && !isDied)
        {
            DeathAnimation();
        }

    }

    private void DeathAnimation()
    {
        isDied = true; 
        anim.SetTrigger("dead");
        isAlive = false;
        SavingWrapper wrapper = FindAnyObjectByType<SavingWrapper>();
        wrapper.Respawn();
        isAlive = true;
        isDied = false;
    }

    public void PlayerDie()
    {
        Time.timeScale = 0f;
    }
    
    private void HealthBarColor()
    {
        if (health <= 100.0f && health >= 60.0f) 
        {
            frontHealth.color = GetColorFromString("2BFF00");
        }
        
        if (health <= 50.0f && health >= 30.0f) 
        {
            frontHealth.color = GetColorFromString("FFE312");
        }
        
        if (health <= 20.0f && health >= 0f) 
        {
            frontHealth.color = GetColorFromString("FF2613");
        }
        
    }
    public void UpdateHealthUI()
    {
        float fillF = frontHealth.fillAmount;
        float fillB = backHealth.fillAmount;
        float hFraction = health / 100;
        if (fillB > hFraction)
        {
            frontHealth.fillAmount = hFraction;
            backHealth.color = DamageHealthColor;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; 
            backHealth.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealth.color = Color.white;
            backHealth.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete; 
            frontHealth.fillAmount = Mathf.Lerp(fillF, backHealth.fillAmount, percentComplete);
        }
    }

    public static Color GetColorFromString(string color) 
    {
        float red = Hex_to_Dec01(color.Substring(0,2));
        float green = Hex_to_Dec01(color.Substring(2,2));
        float blue = Hex_to_Dec01(color.Substring(4,2));
        float alpha = 1f;
        if (color.Length >= 8) {
            // Color string contains alpha
            alpha = Hex_to_Dec01(color.Substring(6,2));
        }
        return new Color(red, green, blue, alpha);
    }
    public static int Hex_to_Dec(string hex) 
    {
        return Convert.ToInt32(hex, 16);
    }
    public static float Hex_to_Dec01(string hex) 
    {
        return Hex_to_Dec(hex)/255f;
    }
    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(health);
    }

    public void RestoreFromJToken(JToken state)
    {
        health = state.ToObject<float>();
    }
}
