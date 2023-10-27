using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //캐릭터가 선택되면 버튼 작동 정지 --> 굳이 필요 없으면 X
    //다음 화면으로 넘어가야 함
    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }


}
