using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFire : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float basefiringRate = 0.2f;

    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;
    Coroutine firingCoroutine;

    [HideInInspector] public bool isFiring;
    AudioPlayer audioPlayer;

    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }


    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }

    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            Vector3 gamePosition = new Vector3();
            gamePosition.x = transform.position.x;
            gamePosition.z = transform.position.z;
            if (useAI)
            {
                gamePosition.y = transform.position.y - 1f;
                GameObject instance = Instantiate(projectilePrefab, gamePosition, Quaternion.identity);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
                float timeToNextProjectile = Random.Range(basefiringRate - firingRateVariance, basefiringRate + firingRateVariance);
                timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
                audioPlayer.PlayShootingClip();
                yield return new WaitForSeconds(timeToNextProjectile);
            }
            else
            {
                gamePosition.y = transform.position.y + 1f;
                GameObject instance = Instantiate(projectilePrefab, gamePosition, Quaternion.identity);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }
                Destroy(instance, projectileLifetime);
                audioPlayer.PlayShootingClip();
                yield return new WaitForSeconds(basefiringRate);
            }


        }
    }

}
