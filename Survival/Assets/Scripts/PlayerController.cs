using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camCurrentXRotation;
    public float lookSensitivity;
    private Vector2 mouseDelta;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //dissapear mouse cursor
    }

    private void LateUpdate()
    {
        CameraLook();
    }

    //looking around with camera
    void CameraLook()
    {
        camCurrentXRotation += mouseDelta.y * lookSensitivity;
        camCurrentXRotation = Mathf.Clamp(camCurrentXRotation, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camCurrentXRotation, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }


    public void OnLookInput (InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
}
