using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] Transform t;
    [SerializeField] Vector3 offSet;
    [SerializeField] Vector2 boundaryX;

    Camera cam;
    float camSize;
    Vector3 pos;
    private void Awake()
    {
        cam = GetComponent<Camera>();
    }
    private void Start()
    {
        camSize = cam.orthographicSize * cam.aspect;
        pos = transform.position;
    }

    private void LateUpdate()
    {
        if (t.position.x - camSize > boundaryX.x && t.position.x + camSize < boundaryX.y)
            pos.x = t.position.x + offSet.x;

        pos.y = t.position.y + offSet.y;

        transform.position = pos;
    }

    Vector3 left, right;
    private void OnDrawGizmosSelected()
    {

            left = transform.position;
            left.x = boundaryX.x;


            right = transform.position;
            right.x = boundaryX.y;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(left, right);
    }

}
