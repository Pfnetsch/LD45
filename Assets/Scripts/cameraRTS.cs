using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRTS : MonoBehaviour
{
    private static Camera _attachedCamera;
    private static readonly float _zoomSpeed = 2.0f;
    private static readonly float _keyBoardPanSpeed = 10.0f;
    private static readonly float _mousePanSpeed = 20.0f;

    private static readonly float _zoomMinSize = 5f;
    private static readonly float _zoomMaxSize = 15f;

    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {
        _attachedCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        float translation = Time.deltaTime* _keyBoardPanSpeed;

        if (Input.GetMouseButtonDown(1))
        {
            lastMousePosition = Input.mousePosition;
        }
        else if (Input.GetMouseButton(1))
        {
            PanCamera(Input.mousePosition);
        }
        else
        {
            if (Input.GetKey(KeyCode.A))
            {
                _attachedCamera.transform.Translate(-translation, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                _attachedCamera.transform.Translate(translation, 0, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                _attachedCamera.transform.Translate(0, translation, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                _attachedCamera.transform.Translate(0, -translation, 0);
            }
        }

        if (Input.mouseScrollDelta.y != 0|| Input.mouseScrollDelta.x != 0)
            ZoomCamera(Input.mouseScrollDelta.y, _zoomSpeed);
    }

    private void PanCamera(Vector3 newMousePosition)
    {
        Vector3 diff = _attachedCamera.ScreenToViewportPoint(lastMousePosition - newMousePosition);
        Vector3 move = new Vector3(diff.x * _mousePanSpeed, diff.y * _mousePanSpeed);

        _attachedCamera.transform.Translate(move, Space.World);

        //Vector3 pos = _attachedCamera.transform.position;
        //pos.x = Mathf.Clamp(_attachedCamera.transform.position.x, )
    
        // TODO Clamp X Y bounds

        lastMousePosition = newMousePosition;
    }

    private void ZoomCamera(float offset, float speed)
    {
        if (offset == 0) return;

        _attachedCamera.orthographicSize = Mathf.Clamp(_attachedCamera.orthographicSize - (offset * speed), _zoomMinSize, _zoomMaxSize);
    }
}
