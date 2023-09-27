using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class EnemyAxeDamage : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    [SerializeField] private Collider myCollider;
    [SerializeField] private Weapon enemyAxe;
    public float damage;
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Enemy"){return;}
        if(other == myCollider){return;}
        if(alreadyColliderWith.Contains(other))
        {   
            return;
        }
        alreadyColliderWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            damage = enemyAxe.GetWeaponDamage();
            health.DealDamage(damage);
            if(health.tag == "Player")
            {
                PlayerLife.Instance.lerpTimer = 0f;
            }
        }
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * enemyAxe.GetWeaponKnokcback());
        }
    }
}