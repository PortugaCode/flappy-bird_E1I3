using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //ĳ���Ͱ� ���õǸ� ��ư �۵� ���� --> ���� �ʿ� ������ X
    //���� ȭ������ �Ѿ�� ��
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }


}
