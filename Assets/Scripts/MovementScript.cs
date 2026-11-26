using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;
    // VILMER :S
    ChargeJump chargeJump;
    //
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
        // VILMER :}
        chargeJump = GetComponent<ChargeJump>();
        //
    }

    void Update()
    {
        // VILMER :)
        if (!onGround && myRigidbody.IsTouching(contactFilters[1]) && !((Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed && myRigidbody.linearVelocityX < 0) || (Keyboard.current.dKey.isPressed && !Keyboard.current.aKey.isPressed && myRigidbody.linearVelocityX > 0)))
        {
            myRigidbody.linearVelocityX = 0;
        }
        //
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
        // VILMER :U
        // This is mostly a copy of the deceleration code, but I didn't want to mess with your script too much so I'll leave making it into a proper function to you.
        else if (onGround)
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
        //
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
            // VILMER :P
            //else
            else if (myRigidbody.linearVelocityX >= -movementSpeedMax - 1)
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
            // VILMER :3
            //else
            else if (myRigidbody.linearVelocityX <= movementSpeedMax + 1)
            {
                myRigidbody.linearVelocityX = movementSpeedMax;
            }
        }
        // VILMER :D
        if (myRigidbody.linearVelocityX < 0)
        {
            chargeJump.eyes.transform.position = new Vector2(transform.position.x - 0.4f, transform.position.y);
        }
        else if (myRigidbody.linearVelocityX > 0)
        {
            chargeJump.eyes.transform.position = new Vector2(transform.position.x + 0.4f, transform.position.y);
        }
        else
        {
            chargeJump.eyes.transform.position = transform.position;
        }
        //if ((Keyboard.current.aKey.isPressed && Keyboard.current.dKey.isPressed) || !(Keyboard.current.aKey.isPressed || Keyboard.current.dKey.isPressed))
        if (onGround && !Keyboard.current.aKey.isPressed && !Keyboard.current.dKey.isPressed)
        //
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
            // VILMER :O
            //onGround = false;
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