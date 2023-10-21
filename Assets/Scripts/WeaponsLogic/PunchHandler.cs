using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHandler : MonoBehaviour
{
    [SerializeField] private GameObject punchLogic;
    [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;
    public void EnablePunch()
    {
        punchLogic.SetActive(true);
    }
    public void DisablePunch()
    {
        punchLogic.SetActive(false);
    }
    public void PlayRandomSoundPunch()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);
            audioSource.clip = audioClips[randomIndex];
            audioSource.Play();
        }
    }
}
