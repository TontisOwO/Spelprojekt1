using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [Header("Collision")]

    [SerializeField] ContactFilter2D[] contactFilters = new ContactFilter2D[4];

    public bool onGround, onLeftWall, onRightWall;

    [Header("Movement")]

    public bool canMove;

    [SerializeField] private float movementSpeed, movementSpeedMax, movementDeceleration;

    [Header("Jumping")]

    [SerializeField] float jumpForce;
    [SerializeField] float jumpReduceMulti, jumpReduceBase;
    float jumpTime;
    bool jumping;

    //[Header("WallJumping")]

    private void Awake()
    {
        for (int i = 0; i < 4; i++)
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
        if (myRigidbody.IsTouching(contactFilters[3]))
        {
            jumping = false;
            jumpTime = 0;
        }
        if (canMove)
        {
            MovementHandling();
        }
    }

    private void MovementHandling()
    {
        WASDHandling();
        JumpHandling();
        WallJump();
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

        if ((Keyboard.current.aKey.isPressed && Keyboard.current.dKey.isPressed) || !(Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed))
        {
            if (myRigidbody.linearVelocityX > 0 + movementDeceleration * Time.deltaTime)
            {
                myRigidbody.linearVelocityX -= movementDeceleration * Time.deltaTime;

            }
            else if (myRigidbody.linearVelocityX < 0 - movementDeceleration * Time.deltaTime)
            {
                myRigidbody.linearVelocityX += movementDeceleration * Time.deltaTime;

            }
            else
            {
                myRigidbody.linearVelocityX = 0;
            }
        }

        //if (Keyboard.current.aKey.isPressed && Keyboard.current.dKey.isPressed)
        //{
        //    if (myRigidbody.linearVelocityX > 0 + movementSpeed * Time.deltaTime)
        //    {
        //        myRigidbody.linearVelocityX -= movementSpeed * Time.deltaTime;

        //    }
        //    else if (myRigidbody.linearVelocityX < 0 - movementSpeed * Time.deltaTime)
        //    {
        //        myRigidbody.linearVelocityX += movementSpeed * Time.deltaTime;

        //    }
        //    else
        //    {
        //        myRigidbody.linearVelocityX = 0;
        //    }
        //}
    }
    private void JumpHandling() 
    {
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
            myRigidbody.linearVelocityY -= jumpForce - jumpTime * myRigidbody.gravityScale * (9.81f / jumpReduceMulti);
            jumpTime = 0;
        }
        if ((jumpReduceMulti > 1 || jumpReduceBase != 0) && myRigidbody.linearVelocityY <= 0)
        {
            jumping = false;
            jumpTime = 0;
        }
        if (jumpForce - jumpTime * myRigidbody.gravityScale * (9.81f / jumpReduceMulti) <= 0)
        {
            jumping = false;
            jumpTime = 0;
        }
    }

    private void WallJump()
    {
        if (Keyboard.current.spaceKey.isPressed && !onGround)
        {
            if (onLeftWall)
            {

            }
            if (onRightWall)
            {

            }
        }
    }
}