using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Use this for initialization
    public float XSensitivity = 2f;
    public float YSensitivity = 2f;
    public bool clampVerticalRotation = true;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool smooth;
    public float smoothTime = 5f;
    // internal private variables
    private Quaternion m_CharacterTargetRot;
    private Quaternion m_CameraTargetRot;
    private Transform character;
    private Transform cameraTransform;
    public float ZoomScale = 0.5f;
    public float speed = 1f;
    public bool MouseLock = true;
    bool isFollow = false;
    public GameObject FollowingObject;
    // Threshold 
    public float groundOffset = 0f;
    public float height = 100f;
    public float radius = 100f;
    void Start()
    {
        LockCursor(MouseLock);
        character = gameObject.transform;
        cameraTransform = Camera.main.transform;
        m_CharacterTargetRot = character.localRotation;
        m_CameraTargetRot = cameraTransform.localRotation;

    }

    void Update()
    {

        ZoomController(); //Use Mouse Scroll
        if (MouseLock)
        {
            ListenKeyboard(); // Listen for keyboard
            LookRotation(); // Camera Rotation by Mouse movment
        }
        // if LeftControll key is pressed, then unlock the cursor
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            MouseLock = !MouseLock;
            LockCursor(MouseLock);
        }

    }

    private void ZoomController()
    {
        float y = Input.mouseScrollDelta.y;
        if (y > 0)
        {
            ZoomIn();
        }
        else if (y < 0)
        {
            ZoomOut();
        }
    }

    private void ListenKeyboard()
    {
        if (Input.GetKey(KeyCode.W))
        {
           
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            character.position += character.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            character.position -= character.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            character.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
          
            
            character.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.F))
        {
            isFollow = !isFollow;
        }
        if (isFollow)
        {
            CameraFollowObject();
        }
        //Adjust Charecter Possion 
        //if(character.transform.position.y< Terrain.activeTerrain.SampleHeight(transform.position) + groundOffset)
        //{
        //    Vector3 pos = transform.position;
        //    pos.y = Terrain.activeTerrain.SampleHeight(transform.position) + groundOffset;
        //    character.transform.position = pos;
        //}
        
    }

    void CameraFollowObject()
    {
        transform.position = new Vector3(FollowingObject.transform.position.x, FollowingObject.transform.position.y + 150, FollowingObject.transform.position.x);

    }
    public void LockCursor(bool isLocked)
    {
        if (isLocked)
        {
            // make the mouse pointer invisible
            Cursor.visible = false;

            // lock the mouse pointer within the game area
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // make the mouse pointer visible
            Cursor.visible = true;

            // unlock the mouse pointer so player can click on other windows
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void LookRotation()
    {
        //get the y and x rotation based on the Input manager
        float yRot = Input.GetAxis("Mouse X") * XSensitivity;
        float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
        // calculate the rotation
        m_CharacterTargetRot *= Quaternion.Euler(0f, yRot, 0f);
        m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);

        // clamp the vertical rotation if specified
        if (clampVerticalRotation)
            m_CameraTargetRot = ClampRotationAroundXAxis(m_CameraTargetRot);
        // update the character and camera based on calculations
        if (smooth) // if smooth, then slerp over time
        {
            character.localRotation = Quaternion.Slerp(character.localRotation, m_CharacterTargetRot,
                smoothTime * Time.deltaTime);
            cameraTransform.localRotation = Quaternion.Slerp(cameraTransform.localRotation, m_CameraTargetRot,
                smoothTime * Time.deltaTime);
        }
        else // not smooth, so just jump
        {
            character.localRotation = m_CharacterTargetRot;
            cameraTransform.localRotation = m_CameraTargetRot;
        }
        transform.rotation = cameraTransform.transform.rotation;
    }
    // Some math ... eeck!
    Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, MinimumX, MaximumX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);
        return q;
    }

    public void ZoomIn()
    {
        transform.position += transform.forward * ZoomScale;

    }
    public void ZoomOut()
    {
        transform.position -= transform.forward * ZoomScale;
    }

}