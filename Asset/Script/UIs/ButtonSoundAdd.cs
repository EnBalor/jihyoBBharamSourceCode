using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSoundAdd : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void ClickBtn()
    {
        audioSource.PlayOneShot(clip);
    }
}
