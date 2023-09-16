using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Header("Stats")]
        [Range(1, 50)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        public float GetHealth()
        {
            return progression.GetHealth(characterClass, startingLevel);
        }
        public float GetAS()
        {
            return progression.GetAS(characterClass, startingLevel);
        }
    }
}
