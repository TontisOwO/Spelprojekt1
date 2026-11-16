using UnityEngine;

public class MovmentScript : MonoBehaviour
{
    Rigidbody2D myRigidbody;

    [SerializeField] float JumpForce;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {

        }

        if (Input.GetKey(KeyCode.D))
        {

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForceY(JumpForce);
        }
    }
}
