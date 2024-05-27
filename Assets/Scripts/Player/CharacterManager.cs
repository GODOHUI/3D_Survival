using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager _instance;
    public static CharacterManager Instance
    {
        get  //�ȿ� ���� ���� ����  CharacterManager �ϳ� ������ְ� _instance �ν��Ͻ� �� �־ ��ȯ���ش�
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
        //�ν��Ͻ��� ��������� Awake()������ �Ȱ��� �پ��ִ� ��ũ��Ʈ���پ��ִ»��¿� �����̵Ǿ���
            //�׷��� ���� ���ӿ�����Ʈ ���������� ���� this�־��ش� 
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else //�ν��Ͻ��� ���� �ƴҶ� �ν��Ͻ� �ȿ��ִ°Ͱ� �� �ڽ��� �ٸ��ٸ� �����ִ°� �ı������
        {
            if(_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
