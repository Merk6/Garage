using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjRB;

    [Header("Physics Parameters")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;
    private float x;

    private void Update()
    {
        x = holdArea.transform.localPosition.z;
        x = Mathf.Clamp(x, 2f, 15f);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (heldObj == null)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange))
                {
                    PickUpObject(hit.transform.gameObject);
                }
            }
            else
            {
                DropObject();
            }
        }
        
        if (heldObj != null)
        {
            MoveObject();
            //itemRange = Vector3.Distance(gameObject.transform.position, heldObj.transform.position);
        }
    }

    void MoveObject()
    {
        

        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjRB.AddForce(moveDirection * pickupForce);
        }

        if(holdArea.localPosition.z >= 2 && holdArea.localPosition.z <= 5)
        {
            holdArea.localPosition += Vector3.forward * Input.GetAxis("Mouse ScrollWheel");
        }
        else
        {
            if (holdArea.localPosition.z <= 2)
            {
                holdArea.localPosition = new Vector3(0,0,2);
            }
            else if (holdArea.localPosition.z > 5)
            {
                holdArea.localPosition = new Vector3(0, 0, 5);
            }
        }
    }

    void PickUpObject(GameObject pickObj)
    {
        holdArea.localPosition = new Vector3(0, 0, 2);
        
        if(pickObj.GetComponent<Rigidbody>())
        {
            heldObjRB = pickObj.GetComponent<Rigidbody>();
            heldObjRB.useGravity = false;
            heldObjRB.linearDamping = 10;
            heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjRB.transform.parent = holdArea;
            
            heldObj = pickObj;
        }
    }

    void DropObject()
    {
        heldObjRB.useGravity = true;
        heldObjRB.linearDamping = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;

        heldObj.transform.parent = null;
        heldObj = null;

    }
}
