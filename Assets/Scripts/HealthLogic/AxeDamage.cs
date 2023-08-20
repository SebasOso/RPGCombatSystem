using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    [SerializeField] private Collider myCollider;
    [SerializeField] GameObject slash01;
    private float axeDamage;
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other == myCollider){return;}
        if(alreadyColliderWith.Contains(other)){return;}
        alreadyColliderWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(axeDamage);
        }
    }
    public void SetDamage(float damage)
    {
        axeDamage = damage;
    }
}
