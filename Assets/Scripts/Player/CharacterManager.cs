using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get  //안에 값이 널이 더라도  CharacterManager 하나 만들어주고 _instance 인스턴스 에 넣어서 반환해준다
        {
            if (_instance == null)
            {
                _instance = new GameObject("CharacterManager").AddComponent<CharacterManager>();
            }
            return _instance;
        }

    }
    public Player _player;
    public Player Player
    {
        get
        {
            return _player;
        }
        set { _player = value; }
    }



    private void Awake()
    {
        //인스턴스가 비어있을때 Awake()싱행이 된것은 붙어있는 스크립트가붙어있는상태에 실행이되었다
            //그래서 따로 게임오브젝트 만들이유는 없고 this넣어준다 
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else //인스턴스가 널이 아닐때 인스턴스 안에있는것과 내 자신이 다르다면 현재있는걸 파괴해줘라
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
