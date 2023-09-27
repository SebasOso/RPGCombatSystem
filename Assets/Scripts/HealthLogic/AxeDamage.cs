using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    [SerializeField] private Collider myCollider;
    private Armory armory;
    public bool runeDamage = false;
    public float damage;
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        armory = myCollider.GetComponent<Armory>();
        if(other == myCollider){return;}
        if(alreadyColliderWith.Contains(other))
        {   
            return;
        }
        alreadyColliderWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            if(runeDamage)
            {
                damage = armory.damage + 10f;
                health.DealDamage(damage);
            }
            else
            {
                damage = armory.damage;
                health.DealDamage(damage);
            }
            if(health.tag == "Player")
            {
                PlayerLife.Instance.lerpTimer = 0f;
            }
        }
        if(other.TryGetComponent<ForceReceiver>(out ForceReceiver force))
        {
            Vector3 direction = (other.transform.position - myCollider.transform.position).normalized;
            force.AddForce(direction * armory.currentWeapon.GetWeaponKnokcback());
        }
    }
}
