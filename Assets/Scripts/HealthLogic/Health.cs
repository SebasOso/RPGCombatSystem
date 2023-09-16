using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;
using UnityEngine.VFX;

public class Health : MonoBehaviour, IJsonSaveable
{
    public event Action OnDie;
    public event Action OnTakeDamage;
    [SerializeField] public float health = 100f;
    private bool isDead = false;
    private bool isInvulnerable;
    [SerializeField] private VisualEffect hit;
    private Animator animator;
    private void Start()
    {
        health = GetComponent<BaseStats>().GetHealth();
        if(health == 0)
        {
            OnDie?.Invoke();
            Die();
        }
    }
    void Update()
    {
        
    }
    public void DealDamage(float damage)
    {
        if(health <= 0){return;}
        if(isInvulnerable){return;}
        hit.Play();
        health = Mathf.Max(health - damage, 0);
        OnTakeDamage?.Invoke();
        if(health == 0)
        {
            OnDie?.Invoke();
            Die();
        }
    }
    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
    private void Die()
    {
        if(isDead) return;
        isDead = true;
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
