using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.OpenXR.Input;

public class TouchController : MonoBehaviour
{
    [SerializeField] InputActionReference m_PositionReference;
    [SerializeField] InputActionReference m_RotationReference;

    private Vector3 m_CurrPosition;
    private Quaternion m_CurrRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_PositionReference.action.performed += OnPositionUpdated;
        m_RotationReference.action.performed += OnRotationUpdated;
    }

    private void OnDestroy()
    {
        m_PositionReference.action.performed -= OnPositionUpdated;
        m_RotationReference.action.performed -= OnRotationUpdated;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_CurrPosition;
        transform.rotation = m_CurrRotation;
    }

    private void OnPositionUpdated(InputAction.CallbackContext context)
    {
        m_CurrPosition = context.ReadValue<Vector3>();
    }

    private void OnRotationUpdated(InputAction.CallbackContext context)
    {
        m_CurrRotation = context.ReadValue<Quaternion>();
    }
}
