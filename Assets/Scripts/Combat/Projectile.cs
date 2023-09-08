using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    Health target = null;
    private Collider targetCollider;
    [SerializeField] bool isHoming = true;
    [SerializeField] float speed = 1;
    float damage = 0;
    private void Start() 
    {
        transform.LookAt(GetAimLocation());
    }
    void Update()
    {
        if(target == null){return;}
        if(isHoming && !target.IsDead())
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    
    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }
    private Vector3 GetAimLocation() 
    {
        if (!targetCollider) {
            targetCollider = target.GetComponent<Collider>();
        }
    
        if (!targetCollider) {
            return target.transform.position;
        }
        return targetCollider.bounds.center;
    }
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.GetComponent<Health>() != target){return;}
        if(alreadyColliderWith.Contains(other))
        {   
            return;
        }
        alreadyColliderWith.Add(other);
        if(target.IsDead()){return;}
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
            if(health.tag == "Player")
            {
                PlayerLife.Instance.lerpTimer = 0f;
            }
        }
        Destroy(gameObject);
    }
}
