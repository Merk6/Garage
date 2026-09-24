using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float playerHeight;

    public GameObject Cam;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rB;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        rB = GetComponent<Rigidbody>();
        rB.freezeRotation = true;
    }

    public void Update()
    {
        SpeedControl();
        MyInput();
        rB.linearDamping = groundDrag;
    }

    private void FixedUpdate()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        rB.AddForce(10f * moveSpeed * moveDirection.normalized, ForceMode.Force);
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rB.linearVelocity.x, 0f, rB.linearVelocity.z);

        // limit velocity if needed
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rB.linearVelocity = new Vector3(limitedVel.x, rB.linearVelocity.y, limitedVel.z);
        }
    }
}
