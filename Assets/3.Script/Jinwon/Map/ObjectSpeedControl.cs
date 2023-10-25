using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpeedControl : MonoBehaviour
{
    public void SetObjectSpeed(float speed)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Ground ground))
            {
                ground.moveSpeed = speed;
            }
            else if (transform.GetChild(i).TryGetComponent(out Pipe pipe))
            {
                pipe.moveSpeed = speed;
            }
        }
    }
}
