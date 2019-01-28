using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shakes the camera, because that's very cool
/// </summary>

public class CamShake : MonoBehaviour
{
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = new Vector3(0f,0f,0f);

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsedTime += Time.deltaTime;
            yield return null;      //waits for the next frame before continuing while loop
        }

        transform.localPosition = originalPos;  
    }
}
