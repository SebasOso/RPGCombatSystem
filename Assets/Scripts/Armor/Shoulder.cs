using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Armor", menuName = "Armors/Make New Shoulder", order = 0)]
public class Shoulder : ScriptableObject
{
    [Header("Settings")]
    public int ShoulderIndex;
    public float ShoulderArmor;

    public float GetArmor()
    {
        return ShoulderArmor;
    }
    public int GetIndex()
    {
        return ShoulderIndex;
    }
}
