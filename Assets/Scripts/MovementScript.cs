using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [Header("Collision")]

    [SerializeField] ContactFilter2D[] contactFilters = new ContactFilter2D[3];

    private bool onGround, onLeftWall, onRightWall;

    [Header("Movement")]

    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementSpeedMax, movementDeceletation;

    [Header("Jumping")]

    [SerializeField] float jumpForce, jumpReduceMulti = 9.81f;
    float jumpTime;
    bool jumping;

    //[Header("WallJumping")]

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            contactFilters[i].SetNormalAngle(90 * i - 45, 90 * i + 45);
            contactFilters[i].SetLayerMask(LayerMask.GetMask("Ground"));
            contactFilters[i].useTriggers = false;
        }
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        onGround = myRigidbody.IsTouching(contactFilters[1]);
        onLeftWall = myRigidbody.IsTouching(contactFilters[0]);
        onRightWall = myRigidbody.IsTouching(contactFilters[2]);

        WASDHandling();

        if (Keyboard.current.spaceKey.isPressed && onGround)
        {
            myRigidbody.linearVelocityY = 0;
            myRigidbody.linearVelocityY += jumpForce;
            onGround = false;
            jumping = true;
        }
        if (jumping)
        {
            jumpTime += Time.deltaTime;
        }
        if (Keyboard.current.spaceKey.wasReleasedThisFrame && jumping)
        {
            jumping = false;
            myRigidbody.linearVelocityY -= jumpForce - jumpTime * myRigidbody.gravityScale * jumpReduceMulti;
            jumpTime = 0;
        }
        if (jumpForce - jumpTime * myRigidbody.gravityScale * 9.81f <= 0)
        {
            jumping = false;
            jumpTime = 0;
        }
    }

    private void WASDHandling()
    {
        if (Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
        {
            if (myRigidbody.linearVelocityX >= -movementSpeedMax)
            {
                myRigidbody.linearVelocityX -= movementSpeed * Time.deltaTime;
            }
            else
            {
                myRigidbody.linearVelocityX = -movementSpeedMax;
            }
        }

        if (Keyboard.current.dKey.isPressed && !Keyboard.current.aKey.isPressed)
        {
            if (myRigidbody.linearVelocityX <= movementSpeedMax)
            {
                myRigidbody.linearVelocityX += movementSpeed * Time.deltaTime;
            }
            else
            {
                myRigidbody.linearVelocityX = movementSpeedMax;
            }
        }

        if (!(Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            if (myRigidbody.linearVelocityX > 0 + movementDeceletation * Time.deltaTime)
            {
                myRigidbody.linearVelocityX -= movementDeceletation * Time.deltaTime;

            }
            else if (myRigidbody.linearVelocityX < 0 - movementDeceletation * Time.deltaTime)
            {
                myRigidbody.linearVelocityX += movementDeceletation * Time.deltaTime;

            }
            else
            {
                myRigidbody.linearVelocityX = 0;
            }
        }

        if (Keyboard.current.aKey.isPressed && Keyboard.current.dKey.isPressed)
        {
            if (myRigidbody.linearVelocityX > 0 + movementSpeed * Time.deltaTime)
            {
                myRigidbody.linearVelocityX -= movementSpeed * Time.deltaTime;

            }
            else if (myRigidbody.linearVelocityX < 0 - movementSpeed * Time.deltaTime)
            {
                myRigidbody.linearVelocityX += movementSpeed * Time.deltaTime;

            }
            else
            {
                myRigidbody.linearVelocityX = 0;
            }
        }
    }
}