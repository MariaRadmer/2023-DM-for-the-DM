using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Source https://www.youtube.com/watch?v=pJQndtJ2rk0 

public class CameraSystem : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] float movementSpeed = 30f;
    [SerializeField] float dragPanSpeed = 10f;
    [SerializeField] float edgeScrollSize = 20f;
    [SerializeField] float zoomMin = 10f;
    [SerializeField] float zoomMax = 50f;
    [SerializeField] float zoomSpeed = 5f;


    private bool dragPanMoveActive;
    private Vector2 lastMousePosition;
    private float zoomTarget = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        HandleCameraMovement();
        EdgeScrolling();
        DragPan();
        Zoom();

    }



    private void HandleCameraMovement()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.W)) inputDirection.y = +1f;
        if (Input.GetKey(KeyCode.S)) inputDirection.y = -1f;
        if (Input.GetKey(KeyCode.A)) inputDirection.x = -1f;
        if (Input.GetKey(KeyCode.D)) inputDirection.x = +1f;




        Vector3 moveDirection = transform.up * inputDirection.y + transform.right * inputDirection.x;

        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }


    private void EdgeScrolling()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        // Edge scrolling
        if (Input.mousePosition.x < edgeScrollSize) inputDirection.x = -1f;
        if (Input.mousePosition.y < edgeScrollSize) inputDirection.y = -1f;
        if (Input.mousePosition.x > Screen.width - edgeScrollSize) inputDirection.x = +1f;
        if (Input.mousePosition.y > Screen.height - edgeScrollSize) inputDirection.y = +1f;

        Vector3 moveDirection = transform.up * inputDirection.y + transform.right * inputDirection.x;

        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void DragPan()
    {
        Vector3 inputDirection = new Vector3(0, 0, 0);
        if (Input.GetMouseButtonDown(1))
        {
            dragPanMoveActive = true;
            lastMousePosition = Input.mousePosition;

        }

        if (Input.GetMouseButtonUp(1))
        {
            dragPanMoveActive = false;
        }


        if (dragPanMoveActive)
        {
            Vector2 mouseMovementDelta = (Vector2)Input.mousePosition - lastMousePosition;
            inputDirection.x = mouseMovementDelta.x * dragPanSpeed;
            inputDirection.y = mouseMovementDelta.y * dragPanSpeed;

            lastMousePosition = Input.mousePosition;
        }
        Vector3 moveDirection = transform.up * inputDirection.y + transform.right * inputDirection.x;

        transform.position += moveDirection * movementSpeed * Time.deltaTime;
    }

    private void Zoom()
    {
        //Debug.Log(Input.mouseScrollDelta.y);

        if (Input.mouseScrollDelta.y > 0)
        {
            zoomTarget -= 5;
            
        }

        if (Input.mouseScrollDelta.y < 0)
        {
            zoomTarget += 5;
        }

        zoomTarget = Mathf.Clamp(zoomTarget, zoomMin, zoomMax);

        virtualCamera.m_Lens.OrthographicSize  = Mathf.Lerp(virtualCamera.m_Lens.OrthographicSize, zoomTarget, Time.deltaTime* zoomSpeed);

        
    }
}
