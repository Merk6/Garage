using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask item;
    Ray ray;
    RaycastHit hit;
    GameObject itemToGrab = null;
    float rayDistance = 5f;
    public float itemHeldSpeed;
    public float itemDistance;
    bool grabbed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        item = LayerMask.GetMask("Item");
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Physics.Raycast(ray, out hit, rayDistance, item))
        {
            GrabItem(hit);
        }

        if (Input.GetMouseButtonDown(0) && itemToGrab != null)
        {
            grabbed = !grabbed;
        }
    }

    private void FixedUpdate()
    {
        ray = new Ray(gameObject.transform.position, gameObject.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * rayDistance);

        if (grabbed)
        {
            Rigidbody itemRB = itemToGrab.GetComponent<Rigidbody>();
            itemRB.useGravity = false;

            itemToGrab.transform.SetPositionAndRotation(Vector3.Lerp(itemToGrab.transform.position, gameObject.transform.position + gameObject.transform.forward * itemDistance, itemHeldSpeed + Time.deltaTime), gameObject.transform.rotation);
        }
        else
        {
            if (itemToGrab != null)
            {
                Rigidbody itemRB = itemToGrab.GetComponent<Rigidbody>();
                itemRB.useGravity = true;

                itemToGrab = null;
            }
        }
    }

    public void GrabItem(RaycastHit hit)
    {

        if (Input.GetMouseButtonDown(0) && !grabbed)
        {
            itemToGrab = hit.collider.gameObject;
            Debug.Log("pressed");
        }

    }
}
