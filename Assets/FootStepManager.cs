using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FootStepManager : MonoBehaviour
{
    [SerializeField] AudioClip mudLeft;
    [SerializeField] AudioClip mudRight;
    [SerializeField] AudioClip woodLeft;
    [SerializeField] AudioClip woodRight;
    [SerializeField] AudioClip snowLeft;
    [SerializeField] AudioClip snowRight;
    [SerializeField] AudioClip waterLeft;
    [SerializeField] AudioClip waterRight;
    private RaycastHit raycastHit;
    [SerializeField] AudioSource audioSource;
    public void LeftFootStep()
    {
        if(GetComponent<Animator>().GetFloat("speed") <= 0)
        {
            return;
        }
        Vector3 rayDirection = new Vector3(0, -1, 0);
        Ray ray = new Ray(transform.position, rayDirection);
        if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
        {
            Debug.Log("Raycast hit Layer: " + raycastHit.transform.gameObject.layer);
            if(GetComponent<Animator>().GetFloat("speed") >= 0)
            {
                //MUD LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 12)
                {
                    PlayFootStep(mudLeft);
                }
                //WOOD LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 13)
                {
                    PlayFootStep(woodLeft);
                }
                //SNOW LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 14)
                {
                    PlayFootStep(snowLeft);
                }
                //SEA LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 6)
                {
                    PlayFootStep(waterLeft);
                }
            }
        }
    }
    public void RightFootStep()
    {
        if(GetComponent<Animator>().GetFloat("speed") <= 0)
        {
            return;
        }
        Vector3 rayDirection = new Vector3(0, -1, 0);
        Ray ray = new Ray(transform.position, rayDirection);
        if(Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
        {
            Debug.Log("Raycast hit Layer: " + raycastHit.transform.gameObject.layer);
            if(GetComponent<Animator>().GetFloat("speed") >= 0)
            {
                //MUD LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 12)
                {
                    PlayFootStep(mudRight);
                }
                //WOOD LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 13)
                {
                    PlayFootStep(woodRight);
                }
                //SNOW LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 14)
                {
                    PlayFootStep(snowRight);
                }
                //SEA LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 6)
                {
                    PlayFootStep(waterRight);
                }
            }
        }
    }
    public void PlayFootStep(AudioClip audioClip)
    {
       audioSource.clip = audioClip;
       audioSource.Play();
    }
}
