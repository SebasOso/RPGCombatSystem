using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioClip[] combatAudioClips;
    public AudioClip[] musicAudioClips;
    private AudioSource audioSource;
    public enum PlayerState { Neutral, Combat }
    private PlayerState currentPlayerState = PlayerState.Neutral;
    private float fadeDuration = 1.0f; // Duración del crossfade en segundos (ajusta según sea necesario)
    private bool isCrossfading = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayRandomMusicAudioClip();
    }

    public void SetPlayerState(PlayerState newState)
    {
        if (currentPlayerState != newState && !isCrossfading)
        {
            // Cambia el estado del jugador y comienza un crossfade.
            currentPlayerState = newState;

            if (currentPlayerState == PlayerState.Neutral)
            {
                StartCoroutine(CrossfadeToMusic());
            }
            else if (currentPlayerState == PlayerState.Combat)
            {
                StartCoroutine(CrossfadeToCombat());
            }
        }
    }

    IEnumerator CrossfadeToCombat()
    {
        isCrossfading = true;

        // Reduzca gradualmente el volumen al 0 durante la duración del crossfade
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;

        // Cambia el clip y restablece el volumen
        PlayRandomCombatAudioClip();
        audioSource.volume = startVolume;

        isCrossfading = false;
    }

    IEnumerator CrossfadeToMusic()
    {
        isCrossfading = true;

        // Reduzca gradualmente el volumen al 0 durante la duración del crossfade
        float startVolume = audioSource.volume;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }
        audioSource.volume = 0f;

        // Cambia el clip y restablece el volumen
        PlayRandomMusicAudioClip();
        audioSource.volume = startVolume;

        isCrossfading = false;
    }

    public void PlayRandomCombatAudioClip()
    {
        int randomIndex = Random.Range(0, combatAudioClips.Length);

        audioSource.clip = combatAudioClips[randomIndex];
        audioSource.Play();
    }

    public void PlayRandomMusicAudioClip()
    {
        int randomIndex = Random.Range(0, musicAudioClips.Length);

        audioSource.clip = musicAudioClips[randomIndex];
        audioSource.Play();
    }
}
