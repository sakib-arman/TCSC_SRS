using UnityEngine;

public class SpawnHandler : MonoBehaviour
{
    RaycastHit hit;
    Camera cam;
    private Rigidbody rb;
    private void Start()
    {
        cam = Camera.main;
    }
    public void SpawnObject(GameObject gameObject)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            // Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            float offsetX = gameObject.transform.localScale.x / 2;
            float offsetY = gameObject.transform.localScale.y / 2;
            float offsetZ = gameObject.transform.localScale.z / 2;
            GameObject spawnedObject = Instantiate(gameObject, new Vector3(hit.point.x, hit.point.y + offsetY, hit.point.z), Quaternion.identity) as GameObject;
            rb = spawnedObject.GetComponent<Rigidbody>();
            // Destroy(rb, Time.deltaTime * 2);


        }



    }
}
