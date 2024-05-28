using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float moveSpeed; // 스피드 변수
    private Vector2 curMovementInput; //InputAction에서 받아올  값 넣을곳
    public Rigidbody _rigidbody;
    public float jumptForce;
    public LayerMask groundLayerMask;

    [Header("Look")]
    public Transform cameraContainer;   //카메라 컨데이너 만들어준다 
    public float minXLook; // x 회전범위 최대
    public float maxXLook; // x 회전범위 최소
    private float camCurXRot;  //마우스의 델타 계산 값 을받아온다
    public float lookSensitivity; // 민감도
    private Vector2 mouseDelta; //마우스 델타값 

    [HideInInspector]
    public bool canLook = true;
    public Action inventory;

 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
     
        Cursor.lockState = CursorLockMode.Locked;  // 커서를 보이게 하고싶지 않다 
      
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void LateUpdate()
    {
        if (canLook)  //true일때만 카메라가 돌아간다
        {
            CameraLook();
        }
    }

    private void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        //방향 추출 forward =w,s값
        dir *= moveSpeed;
       //방향에 속도값을 곱해준다
        dir.y = _rigidbody.velocity.y; //초기화 velocity.y 점프를 했을때 초기화

        _rigidbody.velocity = dir;
    }
    public void OnMove(InputAction.CallbackContext context)  //현재 상태 받아오기 
    {
        if (context.phase == InputActionPhase.Performed) //키를 눌렀을때 Performed동시에 눌려도 가능하게
        {
            curMovementInput = context.ReadValue<Vector2>();

        }
        else if (context.phase == InputActionPhase.Canceled)  //키를 놨을때 
        {
            curMovementInput = Vector2.zero;      // 멈춰라
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();   //마우스는 유지 되어서 값만 넣어주면된다
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started&& IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * jumptForce, ForceMode.Impulse);
        }
    }
    void CameraLook() //카메라를 돌려준다 캐릭터 말고
    { //mouseDelta 좌우 x 값 이동
        camCurXRot += mouseDelta.y * lookSensitivity;  //마우스 델타값 * 민감도
        camCurXRot = Mathf.Clamp(camCurXRot, minXLook, maxXLook); 
        //Clamp = camCurXRot 가 최소값보다 작아지면 최소값 반환 ,최대값보다 커지만 최댓값반환
        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);  //localEulerAngles 로컬 좌표

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0); // 위아래 돌려주는거
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
        if (context.phase == InputActionPhase.Started)  //tab키를 누르면 함수 호출
        {
            inventory?.Invoke();  
            ToggleCursor();
        }
    }
    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;  //인벤토리가 안열려져있고 화면이 움직이는상태
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked; //락이 되어있으면 none
        canLook = !toggle;  
    }

}


