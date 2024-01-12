using UnityEngine;

public class MouseUtil : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public static MouseUtil Instance;
    private GameObject mousePointer;
    private bool isMouseAvailable = true;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            mousePointer = transform.GetChild(0).gameObject;
        } else
        {
            Destroy(this);
        }
    }

    public void Update()
    {
        if(Input.GetMouseButtonDown(1)) 
        {
            ToggleMouse();
        }
        if (isMouseAvailable)
        {
            mousePointer.SetActive(true);
            transform.position = GetMousePosition();
        } else
        {
            mousePointer.SetActive(false);
        }
    }

    public Vector3 GetMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, layerMask);
        return hitInfo.point;
    }

    public bool TryGetMousePosition(out Vector3 pos)
    {
        pos = GetMousePosition();
        return isMouseAvailable;
    }

    public bool IsMouseAvailable()
    {
        return isMouseAvailable;
    }

    private void ToggleMouse()
    {
        isMouseAvailable = !isMouseAvailable;
    }

    public Transform GetMousePointer()
    {
        return mousePointer.transform;
    }
}
