using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRTS : MonoBehaviour
{
    private float _gridWidth;
    private float _gridHeight;

    private static Vector3 _initialCameraPos;
    private static readonly float _zoomSpeed = 2.0f;
    private static readonly float _keyBoardPanSpeed = 10.0f;
    private static readonly float _mousePanSpeed = 20.0f;

    private static readonly float _zoomMinSize = 5f;
    private static readonly float _zoomMaxSize = 15f;

    private Vector3 lastMousePosition;

    // Start is called before the first frame update
    void Start()
    {

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
                Camera.main.transform.Translate(-translation, 0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                Camera.main.transform.Translate(translation, 0, 0);
            }

            if (Input.GetKey(KeyCode.W))
            {
                Camera.main.transform.Translate(0, translation, 0);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                Camera.main.transform.Translate(0, -translation, 0);
            }
        }

        if (Input.mouseScrollDelta.y != 0|| Input.mouseScrollDelta.x != 0)
            ZoomCamera(Input.mouseScrollDelta.y, _zoomSpeed);
    }

    private void PanCamera(Vector3 newMousePosition)
    {
        Vector3 diff = Camera.main.ScreenToViewportPoint(lastMousePosition - newMousePosition);
        Vector3 move = new Vector3(diff.x * _mousePanSpeed, diff.y * _mousePanSpeed);

        Camera.main.transform.Translate(move, Space.World);

        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        var minX = (horzExtent > _gridWidth) ? _initialCameraPos.x : (_initialCameraPos.x - (_gridWidth - horzExtent));
        var maxX = (horzExtent > _gridWidth) ? _initialCameraPos.x : (_initialCameraPos.x + (_gridWidth - horzExtent));
        var minY = (vertExtent > _gridHeight) ? _initialCameraPos.y : _initialCameraPos.y - (_gridHeight - vertExtent);
        var maxY = (vertExtent > _gridHeight) ? _initialCameraPos.y : _initialCameraPos.y + (_gridHeight - vertExtent);

        Vector3 pos = Camera.main.transform.position;
        pos.x = Mathf.Clamp(Camera.main.transform.position.x, minX, maxX);
        pos.y = Mathf.Clamp(Camera.main.transform.position.y, minY, maxY);
        Camera.main.transform.position = pos;

        Debug.Log("X: " + Camera.main.transform.position.x + ", Y: " + Camera.main.transform.position.y);

        lastMousePosition = newMousePosition;
    }

    private void ZoomCamera(float offset, float speed)
    {
        if (offset == 0) return;

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - (offset * speed), _zoomMinSize, _zoomMaxSize);
    }

    public void SetGridSizeAndStoreInitialPosition(float width, float height)
    {
        // Need to swap X and Y ??
        _gridWidth = height;
        _gridHeight = width;
        _initialCameraPos = Camera.main.transform.position;
    }
}
