using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{//�󸶳� ���� �ֽ�ȭ�ؼ� �����Ұ��ΰ�

    public float checkRate = 0.05f;  //�ֽ�ȭ �ð� 
    private float lastCheckTime;    //���������� üũ�ѽð�
    public float maxCheckDistance;  //�󸶳� �ָ��ִ°� üũ����
    public LayerMask layerMask;  //� ���̾ �޷��ִ� ���� ������Ʈ�� ��������

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;  //�������̽� 

    public TextMeshProUGUI promptText;   //���������
    private Camera camera;     

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)  //�� �����Ӹ��� ���� ��°� ����  �󸶳� ���� ȣ���Ҳ��� ���� Ÿ�ӿ��� ���������� ȣ���� Ÿ���� ���ְ� �װ� checkRate ���� ũ��
        {
            lastCheckTime = Time.time;  //���� �ð� �����ֱ�

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));   //   ī�޶� �������� ���� ��� �� �߾ӿ��� ��� ���� ����
            RaycastHit hit;   //�ε����� �� ������Ʈ ���� ��ƵѰ� 

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject) //�浹�� �Ǿ�����  //���� �����ϴ� �� ���� ������
                {
                    curInteractGameObject = hit.collider.gameObject;   //���� �浹�� ������ ��Ƴ��� 
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();  //������Ʈ�� �������� 
                }
            }
            else  // ����� ���� �� 
            {
                curInteractGameObject = null;  //���� �ʱ�ȭ 
                curInteractable = null;
                promptText.gameObject.SetActive(false);  
            }
        }

    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true); 
        promptText.text = curInteractable.GetInteractPrompt();   //�������̽� ����� ������´� 
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)  //�������� ���� ������ �������� �ٶ󺸰�������
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
