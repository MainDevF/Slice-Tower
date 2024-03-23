using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float _positionY;
    public void LiftCamera()
    {
        StartCoroutine(SmoothIncrease());
    }
    IEnumerator SmoothIncrease()
    {
        _positionY = transform.position.y;

        float startTime = Time.time; 

        while (Time.time - startTime < 0.3)
        {
            float time = (Time.time - startTime) / 0.3f;

            float positionY = Mathf.Lerp(_positionY, _positionY + 0.1f, time);
            transform.position = new Vector3(transform.position.x, positionY, transform.position.z);

            yield return null;
        }
    }
}
