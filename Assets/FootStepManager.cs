using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class FootStepManager : MonoBehaviour
{
    [SerializeField] List<AudioClip> mudSteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> woodSteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> snowSteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> waterSteps = new List<AudioClip>();
    private RaycastHit raycastHit;
    [SerializeField] AudioSource audioSource;
    public void FootStep()
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
                    PlayFootStep(mudSteps);
                }
                //WOOD LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 13)
                {
                    PlayFootStep(woodSteps);
                }
                //SNOW LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 14)
                {
                    PlayFootStep(snowSteps);
                }
                //SEA LAYER MANAGER
                if(raycastHit.transform.gameObject.layer == 6)
                {
                    PlayFootStep(waterSteps);
                }
            }
        }
    }
    public void PlayFootStep(List<AudioClip> audioClips)
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
