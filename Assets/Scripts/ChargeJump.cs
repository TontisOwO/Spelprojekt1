using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChargeJump : MonoBehaviour
{
    [SerializeField] private MovementScript movementScript;
    [SerializeField] private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            movementScript.canMove = true;
        }
        else if (Keyboard.current.shiftKey.isPressed)
        {

        }

    }
}
