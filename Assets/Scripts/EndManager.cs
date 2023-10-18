using System;
using System.Collections;
using System.Collections.Generic;
using RPG.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

public class EndManager : MonoBehaviour
{
    [SerializeField] private GameObject ui;
    [SerializeField] private AudioClip music;
    public event Action OnEndGame;
    public int deadEnemies = 0;
    public void EnemyKilled()
    {
        deadEnemies++;
        if(deadEnemies >= 7)
        {
            End();
        }
    }
    public void End()
    {
        OnEndGame();
        GetComponent<AudioSource>().PlayOneShot(music);
        ui.SetActive(false);
        
        GetComponent<InputReader>().Disable();
        GetComponent<ChangeCharacters>().Jonh();
        GetComponent<ChangeCharacters>().canChange =  false;
        GetComponent<Animator>().CrossFadeInFixedTime("Dance", 0.1f);
    }
}
