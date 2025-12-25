using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager_ : MonoBehaviour
{
    public static AudioManager_ Instance;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip ,explosionClip;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShootSound()
    {
        audioSource.PlayOneShot(shootClip);
    }
    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(explosionClip);
    }
}
