using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIBehaviour : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject gamepadButttom;
    [SerializeField] GameObject keyboardButtom;
    public GameObject buttom;
    void Update()
    {
        if(playerInput.currentControlScheme == "GamePad")
        {
            print("GAMEPAAAAAAAAAD");
            buttom = gamepadButttom;
        }
        if(playerInput.currentControlScheme == "Keyboard & Mouse")
        {
            print("KEYBOAAAAAAAAAARD");
            buttom = keyboardButtom;
        }
    }
}
