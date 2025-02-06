using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class Grab : MonoBehaviour
{
    [SerializeField] private InputActionReference m_GrabAction;
    [SerializeField] private float GrabRadius;
    [SerializeField] private LayerMask GrabMask;

    private GameObject m_GrabbedObject;

    private bool m_IsGrabbing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_GrabAction.action.performed += OnToggleGrab;
    }

    private void OnDestroy()
    {
        m_GrabAction.action.performed -= OnToggleGrab;
    }

    private void OnToggleGrab(InputAction.CallbackContext context)
    {
        if (!m_IsGrabbing)
        {
            Debug.Log("Grab");
            GrabObject();
        }
        else if (m_IsGrabbing)
        {
            Debug.Log("Drop");
            DropObject();
        }
    }

    void GrabObject()
    {
        m_IsGrabbing = true;

        RaycastHit[] hits;

        hits = Physics.SphereCastAll(transform.position, GrabRadius, transform.forward, 0f, GrabMask);

        if (hits.Length > 0)
        {
            int closestHit = 0;

            for (int i = 0; i < hits.Length; ++i)
            {
                if ((hits[i]).distance < hits[closestHit].distance)
                {
                    closestHit = i;
                }
            }

            m_GrabbedObject = hits[closestHit].transform.gameObject;
            m_GrabbedObject.GetComponent<Rigidbody>().isKinematic = true;
            m_GrabbedObject.transform.position = transform.position;
            m_GrabbedObject.transform.parent = transform;
        }
    }

    void DropObject()
    {
        m_IsGrabbing = false;

        if (m_GrabbedObject != null)
        {
            m_GrabbedObject.transform.parent = null;
            m_GrabbedObject.GetComponent<Rigidbody>().isKinematic = false;

            m_GrabbedObject.GetComponent<Rigidbody>().linearVelocity = new Vector3(20, 0, 0);
            m_GrabbedObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(20, 0, 0);

            m_GrabbedObject = null;
        }
    }
}
