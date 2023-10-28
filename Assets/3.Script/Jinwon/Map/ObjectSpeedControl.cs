using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpeedControl : MonoBehaviour
{
    public void SetObjectSpeed(float speed) // ������ƮǮ�� ����� ������Ʈ���� �̵��ӵ� ����
    {
        for (int i = 0; i < transform.childCount; i++) // ������ƮǮ�� ���  �ڽ� ������Ʈ��
        {
            if (transform.GetChild(i).TryGetComponent(out Ground ground)) // ground ������Ʈ��� ground �ӵ� ����
            {
                ground.moveSpeed = speed;
            }
            else if (transform.GetChild(i).TryGetComponent(out Pipe pipe)) // pipe ������Ʈ��� pipe �ӵ� ����
            {
                pipe.moveSpeed = speed;
            }
        }
    }
}
