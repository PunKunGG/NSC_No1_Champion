using UnityEngine;

public class RTSCameraController : MonoBehaviour
{
    public Transform camTransform;
    public float moveSpeed = 20f;
    public float zoomSpeed = 500f;
    public float rotateSpeed = 100f;
    public float minZoom = 10f;
    public float maxZoom = 80f;

    private void Update()
    {
        Vector3 pos = transform.position;

        // Movement
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            pos += transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            pos -= transform.forward * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            pos += transform.right * moveSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            pos -= transform.right * moveSpeed * Time.deltaTime;

        transform.position = pos;

        //Zoom by moving Camera (not parent)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 camPos = camTransform.localPosition;
        camPos.y -= scroll * zoomSpeed;
        camPos.y = Mathf.Clamp(camPos.y, minZoom, maxZoom);
        camTransform.localPosition = camPos;

        //Rotate (Q = ซ้าย, E = ขวา)
        if (Input.GetKey(KeyCode.Q))
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.E))
            transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
    }
}
