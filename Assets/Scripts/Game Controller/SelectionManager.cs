
using UnityEngine;

public class SelectionManager : MonoBehaviour
{

    public Material MaterialSelect;
    Material defauldMaterial;

    string SelectableTag = "Selectable";
    string ButtonTag = "Button";
    private GameObject SelectedObject;
    private GameObject PickedUpGameObject;
    bool SelectedModeEnable = false;
    public CameraController CameraController;
    Transform cam;
    Ray ray;
    RaycastHit hit;
    Animator animator;
    private void Start()
    {

        hit = new RaycastHit();
    }

    void Update()
    {
        cam = Camera.main.transform;
        ray = new Ray(cam.position, cam.forward);
        if (Input.GetKeyDown(KeyCode.M))
        {
            SelectedModeEnable = !SelectedModeEnable;
        }

        if (SelectedObject != null && SelectedModeEnable)
        {
            var selectionRender = SelectedObject.GetComponent<Renderer>();
            selectionRender.material = defauldMaterial;
            SelectedObject = null;
        }

        if (SelectedModeEnable)
        {

            if (Physics.Raycast(ray, out hit))
            {
                // Debug.DrawLine(cam.transform.position, hit.point, Color.red);
                // Debug.Log (hit.collider.transform.tag + " " + hit.collider.transform.name);
                // Debug.Log (hit.transform.gameObject.layer);
                if (hit.collider.transform.CompareTag(SelectableTag))
                {
                    SelectedObject = hit.collider.transform.gameObject;
                    var renderer = SelectedObject.GetComponent<Renderer>();
                    defauldMaterial = renderer.material;
                    if (renderer != null)
                    {
                        renderer.material = MaterialSelect;
                    }
                    // Debug.Log(hit.collider.transform.name);

                }
            }

        }
        else
        {
            if (Physics.Raycast(ray, out hit))
            {
                // Debug.DrawLine(cam.transform.position, hit.point, Color.red);
                // Debug.Log (hit.collider.transform.tag + " " + hit.collider.transform.name);
                if (hit.collider.transform.CompareTag(ButtonTag))
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        animator = hit.collider.gameObject.GetComponent<Animator>();
                        animator.SetTrigger("Pressed");
                    }

                }

            }

        }
        if (Input.GetKey(KeyCode.G))
        {
            // Debug.Log("G is Pressed");
        }

        if (!CameraController.MouseLock)
        {
            if (SelectedObject != null)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    SelectedObject.transform.Translate(SelectedObject.transform.worldToLocalMatrix.MultiplyVector(-SelectedObject.transform.forward));
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    SelectedObject.transform.Translate(SelectedObject.transform.worldToLocalMatrix.MultiplyVector(SelectedObject.transform.forward));
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    SelectedObject.transform.Translate(SelectedObject.transform.worldToLocalMatrix.MultiplyVector(-SelectedObject.transform.right));
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    SelectedObject.transform.Translate(SelectedObject.transform.worldToLocalMatrix.MultiplyVector(SelectedObject.transform.right));
                }
            }
        }

    }

}