using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ChargeJump : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private MovementScript movementScript;
    [SerializeField] private Slider slider;
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private float chargeJumpForce = 50;

    private float sliderIncrement;
    private int facing;

    //Remove when proper sprites are implemented
    [SerializeField] public GameObject eyes;
    //
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sliderIncrement = (60 / gameManager.BPM) * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.shiftKey.wasReleasedThisFrame)
        {
            movementScript.canMove = true;
            if (slider.value > 0.9f)
            {
                myRigidbody.linearVelocityY = chargeJumpForce;
                myRigidbody.linearVelocityX = chargeJumpForce * facing;
            }
            slider.value = 0;
            slider.gameObject.SetActive(false);
        }
        else if (Keyboard.current.shiftKey.isPressed && movementScript.onGround)
        {
            slider.gameObject.SetActive(true);
            movementScript.canMove = false;
            slider.value += sliderIncrement;
            if (Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
            {
                facing = -1;
                eyes.transform.position = new Vector2(transform.position.x - 0.4f, transform.position.y);
            }
            else if (Keyboard.current.dKey.isPressed && !Keyboard.current.aKey.isPressed)
            {
                facing = 1;
                eyes.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y);
            }
            else
            {
                facing = 0;
                eyes.transform.position = transform.position;
            }
        }
        if (!movementScript.onGround)
        {
            slider.gameObject.SetActive(false);
        }
    }
}
