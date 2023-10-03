using System.Collections;
using System.Collections.Generic;
using RPG.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    BaseStats baseStats;
    [SerializeField] private Slider slider;
    private void Awake() 
    {
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
    }
    private void Start() 
    {
        slider.maxValue = baseStats.GetStat(Stat.ExperienceToLevelUp);
        experience.OnExperienceGained += SetValueExp;    
        baseStats.OnLevelUp += SetExp;
    }
    private void SetValueExp()
    {
        slider.value = experience.GetExperience();
    }
    private void SetExp()
    {
        slider.maxValue = baseStats.GetStat(Stat.ExperienceToLevelUp);
        slider.minValue = baseStats.GetBaseStat(Stat.ExperienceToLevelUp, 1);
        slider.value = experience.GetExperience();
    }
}
