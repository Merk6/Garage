using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public float mouseX, mouseY;

    public Transform orientation; // player body direction

    float xRotation;
    float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {
        // get mouse input

        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;
        
        yRotation += mouseX; // rotation
        xRotation -= mouseY;

        /*yRotation += mouseX; // rotation
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //prevents mouse from turning more than 90 degrees

        // rotate cam and orientation

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0); */

        //TEST to see if better
    }

    public void FixedUpdate()
    {
        /*float mouseX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime;*/

        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //prevents mouse from turning more than 90 degrees

        // rotate cam and orientation

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}