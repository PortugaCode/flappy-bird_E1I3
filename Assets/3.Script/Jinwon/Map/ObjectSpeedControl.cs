using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpeedControl : MonoBehaviour
{
    public void SetObjectSpeed(float speed) // 오브젝트풀에 저장된 오브젝트들의 이동속도 조정
    {
        for (int i = 0; i < transform.childCount; i++) // 오브젝트풀의 모든  자식 오브젝트들
        {
            if (transform.GetChild(i).TryGetComponent(out Ground ground)) // ground 오브젝트라면 ground 속도 조정
            {
                ground.moveSpeed = speed;
            }
            else if (transform.GetChild(i).TryGetComponent(out Pipe pipe)) // pipe 오브젝트라면 pipe 속도 조정
            {
                pipe.moveSpeed = speed;
            }
        }
    }
}
