using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float currentValue; 
    public void LiftCamera()
    {
        //StartCoroutine(SmoothIncrease());
    }
    IEnumerator SmoothIncrease()
    {
        float startTime = Time.time; 

        while (Time.time - startTime < 0.3)
        {
            float time = (Time.time - startTime) / 0.3f;

            float positionY = Mathf.Lerp(transform.position.y, transform.position.y + 0.01f, time);
            transform.position = new Vector3(transform.position.x, positionY, transform.position.z);

            yield return null;
        }
    }
}
