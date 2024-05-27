using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{//얼마나 자주 최신화해서 검출할것인가

    public float checkRate = 0.05f;  //최신화 시간 
    private float lastCheckTime;    //마지막으로 체크한시간
    public float maxCheckDistance;  //얼마나 멀리있는걸 체크할지
    public LayerMask layerMask;  //어떤 레이어가 달려있는 게임 오브젝트를 추출할지

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;  //인터페이스 

    public TextMeshProUGUI promptText;   //보여지기용
    private Camera camera;     

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)  //매 프레임마다 레일 쏘는거 방지  얼마나 자주 호출할껀지 현재 타임에서 마지막으로 호출한 타임을 빼주고 그게 checkRate 보다 크면
        {
            lastCheckTime = Time.time;  //현재 시간 넣으주기

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));   //   카메라 기준으로 레일 쏘기 정 중앙에서 쏘기 위한 세팅
            RaycastHit hit;   //부딪쳤을 때 오브젝트 정보 담아둘곳 

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject) //충돌이 되었을때  //현재 존재하는 게 같지 않을때
                {
                    curInteractGameObject = hit.collider.gameObject;   //새로 충돌된 정보를 담아놓고 
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();  //프롬포트에 출력해줘라 
                }
            }
            else  // 빈공간 레일 쏠때 
            {
                curInteractGameObject = null;  //정보 초기화 
                curInteractable = null;
                promptText.gameObject.SetActive(false);  
            }
        }

    }
    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true); 
        promptText.text = curInteractable.GetInteractPrompt();   //인터페이스 기능을 가지고온다 
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)  //눌렸을때 현재 에임이 아이템을 바라보고있을때
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }
}
