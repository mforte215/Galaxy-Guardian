using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField] float shakeDuration = 1f;
    [SerializeField] float shakeMag = 0.5f;

    Vector3 initalPos;

    void Start()
    {
        initalPos = transform.position;
    }


    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float elaspedTime = 0;
        while (elaspedTime < shakeDuration)
        {
            transform.position = initalPos + (Vector3)Random.insideUnitCircle * shakeMag;
            elaspedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.position = initalPos;



    }
}
