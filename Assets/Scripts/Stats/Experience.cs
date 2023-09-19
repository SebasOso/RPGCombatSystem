using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] float experiencePoints = 0;
    public void GainExperience(float experienceGained)
    {
        experiencePoints += experienceGained;
    }
}
