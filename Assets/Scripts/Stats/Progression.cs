using UnityEngine;
namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/Progression", order = 0)]
    public class Progression : ScriptableObject 
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                if(progressionCharacterClass.characterClass == characterClass)
                {
                    return progressionCharacterClass.health[level - 1];
                }
            }
            return 0;
        }
        public float GetAS(CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionCharacterClass in characterClasses)
            {
                if(progressionCharacterClass.characterClass == characterClass)
                {
                    return progressionCharacterClass.attackSpeed[level - 1];
                }
            }
            return 1.0f;
        }
        [System.Serializable]
        class ProgressionCharacterClass
        {
             public CharacterClass characterClass;
             public float[] health;
             public float[] attackSpeed;
        }
    }
}

