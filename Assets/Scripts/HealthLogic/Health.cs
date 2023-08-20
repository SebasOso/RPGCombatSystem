using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    private float health;
    private void Start()
    {
        health = maxHealth;
    }
    void Update()
    {
        
    }
    public void DealDamage(float damage)
    {
        if(health <= 0){return;}

        health = Mathf.Max(health - damage, 0);

        Debug.Log(health);
    }
}
