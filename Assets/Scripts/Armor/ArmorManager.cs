using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorManager : MonoBehaviour
{
    public static ArmorManager Instance { get; private set; }  

    public float totalArmor;
    public float shoulderArmor;

    private Health playerHealth;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        playerHealth = GetComponent<Health>();
        CalculateTotalArmor();
    }
    public void EquipShoulder(Shoulder shoulderToEquip)
    {
        ShoulderArmorManager.Instance.SetShoulders(shoulderToEquip);
        shoulderArmor = shoulderToEquip.GetArmor();
        CalculateTotalArmor();
    }
    private void CalculateTotalArmor()
    {
        totalArmor = shoulderArmor;
        playerHealth.armor = totalArmor;
    }
}
