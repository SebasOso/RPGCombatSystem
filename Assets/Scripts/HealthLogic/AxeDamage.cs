using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDamage : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    [SerializeField] private Collider myCollider;
    [SerializeField] GameObject slash01;
    [SerializeField] GameObject slash02;
    [SerializeField] GameObject slash03;
    private float axeDamage;
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other == myCollider){return;}
        if(alreadyColliderWith.Contains(other))
        {   
            slash01.SetActive(false);
            return;
        }
        alreadyColliderWith.Add(other);
        if(other.TryGetComponent<Health>(out Health health))
        {
            switch (axeDamage)
            {
                case 16.5f: 
                slash03.SetActive(true);
                slash01.SetActive(false);
                slash02.SetActive(false);
                break;
                case 16.9f: 
                slash01.SetActive(true);
                slash02.SetActive(false);
                slash03.SetActive(false);
                break;
                case 21.3f: 
                slash02.SetActive(true);
                slash03.SetActive(false);
                slash01.SetActive(false);
                break;
            }
            health.DealDamage(axeDamage);
        }
    }
    public void SetDamage(float damage)
    {
        axeDamage = damage;
    }
}
