using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public interface IInteractable
{
    public string GetInteractPrompt();   // 화면에 띄워줄 함수
    public void OnInteract();     // 어떤효과를 발생해줄껀지
}


public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public string GetInteractPrompt()
    {
        string str = $"{data.displayName}\n{data.description}";   //이름과 설명 반환
        return str;

    }

    public void OnInteract()
    {
        CharacterManager.Instance.Player.itemData = data;
        CharacterManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }


    void Start()
    {

    }


    void Update()
    {

    }
}
