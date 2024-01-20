using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using RPG.Saving;
using RPG.Stats;
using RPG.Utils;
using UnityEngine;
using UnityEngine.VFX;

public class Health : MonoBehaviour, IJsonSaveable
{
    [Header("Heal Settings")]
    [SerializeField] float regenerationHealth = 80;
    [SerializeField] float healRate = 6.5f;

    //Events
    public event Action OnDie;
    public event Action OnTakeDamage;

    //Health Main Variable
    public LazyValue<float> health;

    //Booleans
    private bool isDead = false;
    private bool isInvulnerable;
    public bool isFullHealth;

    //Effects
    [SerializeField] private VisualEffect hit;
    [SerializeField] private ParticleSystem healingEffect;

    //Variables
    private Animator animator;
    private void Awake() 
    {
        health = new LazyValue<float>(GetInitalHealth);
    }
    private float GetInitalHealth()
    {
        return GetComponent<BaseStats>().GetStat(Stat.Health);
    }
    private void Start()
    {
        health.ForceInit();
        if(health.value == 0)
        {
            OnDie?.Invoke();
            Die();
        }
    }
    private void OnEnable() 
    {
        GetComponent<BaseStats>().OnLevelUP += RegenerateHealth;
    }
    private void OnDisable() 
    {
        GetComponent<BaseStats>().OnLevelUP -= RegenerateHealth;
    }
    void Update()
    {
        
    }
    public bool GetFullHealth()
    {
        return health.value >= 1 * GetComponent<BaseStats>().GetStat(Stat.Health);
    }
    private void RegenerateHealth()
    {
        if(health.value <= 0.3 * GetComponent<BaseStats>().GetStat(Stat.Health))
        {
            health.value += regenerationHealth;
        }
    }
    public void Heal()
    {
        StartCoroutine(Regeneration(healRate));
    }
    private IEnumerator Regeneration(float regenerationRate)
    {
        healingEffect.Stop();
        var main = healingEffect.main;
        main.duration = 6f;
        healingEffect.Play();
        float healingTime = 0f;
        while (healingTime < 6f)
        {
            float healthToAdd = regenerationRate * Time.deltaTime;
            health.value += healthToAdd;
            healingTime += Time.deltaTime;
            if (health.value >= GetComponent<BaseStats>().GetStat(Stat.Health))
            {
                health.value = GetComponent<BaseStats>().GetStat(Stat.Health);
                healingEffect.Stop();
            }
            yield return null;
        }
    }
    public void DealDamage(float damage)
    {
        if(health.value <= 0){return;}
        if(isInvulnerable){return;}
        hit.Play();
        if(GetComponent<EnemyLife>())
        {
            GetComponent<EnemyLife>().lerpTimer = 0f;
        }
        health.value = Mathf.Max(health.value - damage, 0);
        OnTakeDamage?.Invoke();
        if(health.value == 0)
        {
            OnDie?.Invoke();
            Die();
            AwardExperience();
        }
    }
    public void SetInvulnerable(bool isInvulnerable)
    {
        this.isInvulnerable = isInvulnerable;
    }
    private void AwardExperience()
    {
        Experience experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        if (experience == null) return;
        experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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
        return JToken.FromObject(health.value);
    }

    public void RestoreFromJToken(JToken state)
    {
        health.value = state.ToObject<float>();
        if(health.value == 0)
        {
            gameObject.SetActive(false);
            Die();
        }
        else
        {
            isDead = false;
        }
    }
}
