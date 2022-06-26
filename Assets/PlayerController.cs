using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Photon.Pun;
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject cameraHolder;
    [SerializeField] float mouseSenstivity, sprintSpeed, walkSpeed, jumpForce, smoothTime;
    [SerializeField] float verticalLookRotation;
    [SerializeField] bool grounded;
    [SerializeField] Vector3 smoothMoveVelocity;
    [SerializeField] Vector3 moveAmount;

    [SerializeField] Rigidbody rb;
    PhotonView PV;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        PV = GetComponent<PhotonView>();


    
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);

        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!PV.IsMine)
            return;
        Look();
        Move();
        Jump();
        
        
    }


    void Move()
    {
        Vector3 moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;


        moveAmount = Vector3.SmoothDamp(moveAmount, moveDir * (Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed), ref smoothMoveVelocity, smoothTime);

    }


    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space)&&grounded)
        {
         rb.AddForce(transform.up * jumpForce);
        }
    }

    void Look()
    {
        transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSenstivity);

        verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSenstivity;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 90f);

        cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
    }

    public void SetGroundedState(bool _grounded)
    {
        grounded = _grounded;
    }


     void FixedUpdate()
    {

        if (!PV.IsMine)
            return;
        rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
    }
}
