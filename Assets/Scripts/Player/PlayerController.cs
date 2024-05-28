using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed; // ���ǵ� ����
    private Vector2 curMovementInput; //InputAction���� �޾ƿ�  �� ������
    public Rigidbody _rigidbody;
    public float jumptForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;   //ī�޶� �����̳� ������ش� 
    public float minXLook; // x ȸ������ �ִ�
    public float maxXLook; // x ȸ������ �ּ�
    private float camCurXRot;  //���콺�� ��Ÿ ��� �� ���޾ƿ´�
    public float lookSensitivity; // �ΰ���
    private Vector2 mouseDelta; //���콺 ��Ÿ�� 

    [HideInInspector]
    public bool canLook = true;
    public Action inventory;

 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
     
        Cursor.lockState = CursorLockMode.Locked;  // Ŀ���� ���̰� �ϰ���� �ʴ� 
      
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        if (canLook)  //true�϶��� ī�޶� ���ư���
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        //���� ���� forward =w,s��
        dir *= moveSpeed;
       //���⿡ �ӵ����� �����ش�
        dir.y = _rigidbody.velocity.y; //�ʱ�ȭ velocity.y ������ ������ �ʱ�ȭ

        _rigidbody.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext context)  //���� ���� �޾ƿ��� 
    {
        if (context.phase == InputActionPhase.Performed) //Ű�� �������� Performed���ÿ� ������ �����ϰ�
        {
            curMovementInput = context.ReadValue<Vector2>();

        }
        else if (context.phase == InputActionPhase.Canceled)  //Ű�� ������ 
        {
            curMovementInput = Vector2.zero;      // �����
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();   //���콺�� ���� �Ǿ ���� �־��ָ�ȴ�
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started&& IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }
    void CameraLook() //ī�޶� �����ش� ĳ���� ����
    { //mouseDelta �¿� x �� �̵�
        camCurXRot += mouseDelta.y * lookSensitivity;  //���콺 ��Ÿ�� * �ΰ���
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); 
        //Clamp = camCurXRot �� �ּҰ����� �۾����� �ּҰ� ��ȯ ,�ִ밪���� Ŀ���� �ִ񰪹�ȯ
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  //localEulerAngles ���� ��ǥ

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // ���Ʒ� �����ִ°�
    }

    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) +(transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++) 
        {
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true;
            }
        }

        return false;
    }

    public void ToggleCursor(bool toggle)
    {
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }


    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)  //tabŰ�� ������ �Լ� ȣ��
        {
            inventory?.Invoke();  
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;  //�κ��丮�� �ȿ������ְ� ȭ���� �����̴»���
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; //���� �Ǿ������� none
        canLook = !toggle;  
    }

}


