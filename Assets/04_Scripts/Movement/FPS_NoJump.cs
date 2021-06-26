using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Tutorial(s) that was followed: https://www.youtube.com/watch?v=LqnPeqoJRFY,https://www.youtube.com/watch?v=E5zNi_SSP_w&t=23s[]

[RequireComponent(typeof(Rigidbody))]
public class FPS_NoJump : MonoBehaviour
{
    //Inspector Values
    [Header("Movement Variables")]
        [SerializeField]
        [InspectorName("Speed")]
        private float _movementSpeed = 6f;
        [SerializeField]
        [InspectorName("Drag on Ground")]
        private float _groundDrag = 6f;
        [SerializeField]
        [InspectorName("Camera Rotation Speed")]
        private float _cameraRot = 5f;

    //Input Variables
    private float 
        _horizontalMove, 
        _verticalMove,
        _MouseY,
        _MouseX,
        _FinalMouseY;

    

    //Components
    private Rigidbody rb;


    //Other
    private float _movementMultiplier = 10f;
    private Vector3 _moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = _groundDrag;
        //rb.freezeRotation = true;



        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        

    }

    private void Update()
    {
        InputReader();
        CameraController();
        
       
    }

    private void FixedUpdate()
    {
       
        rb.AddForce(_moveDirection.normalized * _movementSpeed * _movementMultiplier, ForceMode.Acceleration);
        rb.constraints = RigidbodyConstraints.FreezePositionY;
       

    }

    void InputReader()
    {
        _horizontalMove = Input.GetAxis("Horizontal");
        _verticalMove = Input.GetAxis("Vertical");
        _moveDirection = transform.forward * _verticalMove + transform.right * _horizontalMove;

        _MouseX += Input.GetAxisRaw("Mouse X");
        _MouseY -= Input.GetAxisRaw("Mouse Y");
        _MouseY = Mathf.Clamp(_MouseY, -35, 60);
        

        
    }

    void CameraController()
    {
        gameObject.GetComponentInChildren<Transform>().transform.rotation = Quaternion.Euler(_MouseY, _MouseX, 0);
        gameObject.transform.rotation = Quaternion.Euler(_MouseY, _MouseX * _cameraRot, 0 );
    }
}
