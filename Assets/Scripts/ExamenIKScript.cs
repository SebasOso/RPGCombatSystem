using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class ExamenIKScript : MonoBehaviour
{
    [SerializeField] private Slider leftFootIK;
    [SerializeField] private Slider rightFootIK;
    [SerializeField] private TwoBoneIKConstraint ikLeft;
    [SerializeField] private TwoBoneIKConstraint ikRight;
    void Update()
    {
        ikLeft.weight = leftFootIK.value;
        ikRight.weight = rightFootIK.value;
    }
}
