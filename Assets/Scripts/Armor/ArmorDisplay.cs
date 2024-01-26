using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArmorDisplay : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = ArmorManager.Instance.GetArmor().ToString();
    }
    private void OnEnable()
    {
        GetComponent<TextMeshProUGUI>().text = ArmorManager.Instance.GetArmor().ToString();
    }
}
