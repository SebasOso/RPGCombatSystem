using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    private List<Collider> alreadyColliderWith = new List<Collider>();
    [SerializeField] float speed = 10;
    float damage = 800;
    [SerializeField] Vector3 goVector;
    private void Start() 
    {
        goVector = GameObject.FindGameObjectWithTag("Player").gameObject.transform.forward;
    }

    void Update()
    {
        transform.Translate(goVector * speed * Time.deltaTime);
    }
    private void OnEnable() 
    {
        alreadyColliderWith.Clear();
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player")){return;}
        if(alreadyColliderWith.Contains(other))
        {   
            return;
        }
        alreadyColliderWith.Add(other);
        //speed = 0f;
        if(other.TryGetComponent<Health>(out Health health))
        {
            health.DealDamage(damage);
        }
        //Destroy(gameObject);
    }
}
