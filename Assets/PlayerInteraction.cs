using Unity.Mathematics;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public LayerMask item;
    Ray ray;
    RaycastHit hit;
    GameObject itemToGrab = null;
    float rayDistance = 5f;
    public float itemHeldSpeed;
    public float itemRoatateSpeed;
    public float itemDistance;
    bool grabbed = false;

    public Transform itemFloatPos;

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
            itemRB.isKinematic = true;

            itemToGrab.transform.position = itemFloatPos.transform.position; //Vector3.Lerp(itemToGrab.transform.position, itemFloatPos.transform.position, itemHeldSpeed);
            //itemToGrab.transform.LookAt(gameObject.transform.position, itemToGrab.transform.up);

            itemToGrab.transform.rotation = Quaternion.Slerp(itemToGrab.transform.rotation, gameObject.transform.rotation, itemRoatateSpeed);
        }
        else
        {
            if (itemToGrab != null)
            {
                Rigidbody itemRB = itemToGrab.GetComponent<Rigidbody>();
                itemRB.isKinematic = false;

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
