using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using UnityEngine;
using UnityEngine.VFX;

public class Health : MonoBehaviour, IJsonSaveable
{
    public event Action OnTakeDamage;
    [SerializeField] public float health = 100f;
    private bool isDead = false;
    [SerializeField] private VisualEffect hit;
    private Animator animator;
    private void Start()
    {
        if(health == 0)
        {
            Die();
        }
    }
    void Update()
    {
        
    }
    public void DealDamage(float damage)
    {
        if(health <= 0){return;}
        hit.Play();
        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();
        if(health == 0)
        {
            Die();
        }
    }
    private void Die()
    {
        if(isDead) return;
        isDead = true;
        //Set state to dead.
    }
    public bool IsDead()
    {
        return isDead;
    }

    public JToken CaptureAsJToken()
    {
        return JToken.FromObject(health);
    }

    public void RestoreFromJToken(JToken state)
    {
        health = state.ToObject<float>();
        if(health == 0)
        {
            Die();
        }
        else
        {
            isDead = false;
        }
    }
}
