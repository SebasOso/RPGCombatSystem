using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    private void Awake() 
    {
        experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
    }
    void Update()
    {
        if(experience == null)
        {
            GetComponent<TextMeshProUGUI>().text = "N/A";
            return;
        }
        GetComponent<TextMeshProUGUI>().text = experience.GetExperience().ToString();
    }
}
