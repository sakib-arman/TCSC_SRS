using UnityEngine;

public class FaceController : MonoBehaviour
{
    public GameObject FollowObject;
    private Transform[] camPositions;
    private Transform camTarget;
    public float speed = 10f;
    public int positionIndex = 1;
    void Start()
    {
        camPositions = FollowObject.transform.Find("CameraPositions").GetComponentsInChildren<Transform>();
        camTarget = camPositions[positionIndex];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchCameraPosition();
        }

    }
    void FixedUpdate()
    {
        Vector3 dPos = camTarget.position;
        Vector3 sPos = Vector3.Lerp(this.transform.position, dPos, speed * Time.fixedDeltaTime);
        this.transform.position = sPos;
        this.transform.LookAt(FollowObject.transform.position);
    }
    private void SwitchCameraPosition()
    {
        positionIndex++;
        if (positionIndex >= camPositions.Length)
        {
            positionIndex = 1;
        }
        camTarget = camPositions[positionIndex];
    }

}

