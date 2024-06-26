using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    public static AudioManager instance;

    [Header("Background Music")]
    public AudioSource bgmSource;

    [Header("Sound Effects")]
    public AudioSource[] sfxSource;
    public List<AudioClip> sfxClips;
    public int initialSFXSourceCount = 10;

    private int sfxSourceIndex = 0;
    private Dictionary<string, AudioClip> sfxDictionary;

    protected override void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }

        if (sfxClips != null)
        {
            sfxDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in sfxClips)
            {
                sfxDictionary[clip.name] = clip;
            }
        }

        else
        {
            Debug.Log("Clip List is Null");
        }

        InitializeSFX(initialSFXSourceCount);
    }

    private void InitializeSFX(int count)
    {
        sfxSource = new AudioSource[count];
        for (int i = 0; i < count; i++)
        {
            sfxSource[i] = CreateSFXSource();
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        if (bgmSource.clip == clip)
        {
            return;
        }

        bgmSource.clip = clip;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySFX(string clipName)
    {
        if (sfxDictionary.ContainsKey(clipName))
        {
            AudioSource sfxSource = GetAvilableSFXSource();
            sfxSource.PlayOneShot(sfxDictionary[clipName]);
        }
    }

    private AudioSource GetAvilableSFXSource()
    {
        for (int i = 0; i < sfxSource.Length; i++)
        {
            int index = (sfxSourceIndex + i) % sfxSource.Length;
            if (!sfxSource[index].isPlaying)
            {
                sfxSourceIndex = index;
                return sfxSource[index];
            }
        }

        ExpandSFX();
        return sfxSource[sfxSourceIndex];
    }

    private void ExpandSFX()
    {
        int newLength = sfxSource.Length + 1;
        AudioSource[] newArr = new AudioSource[newLength];
        for (int i = 0; i < sfxSource.Length; i++)
        {
            newArr[i] = sfxSource[i];
        }

        newArr[newLength - 1] = CreateSFXSource();
        sfxSource = newArr;
        sfxSourceIndex = newLength - 1;
    }

    public void SFXSourceCount(int count)
    {
        if (count > sfxSource.Length)
        {
            AudioSource[] newArr = new AudioSource[count];
            for (int i = 0; i < sfxSource.Length; i++)
            {
                newArr[i] = sfxSource[i];
            }

            for (int i = sfxSource.Length; i < count; i++)
            {
                newArr[i] = CreateSFXSource();
            }
            sfxSource = newArr;
        }

        else if (count < sfxSource.Length)
        {
            for (int i = count; i < sfxSource.Length; i++)
            {
                Destroy(sfxSource[i].gameObject);
            }

            AudioSource[] newArr = new AudioSource[count];

            for (int i = 0; i < count; i++)
            {
                newArr[i] = sfxSource[i];
            }

            sfxSource = newArr;
        }
    }

    private AudioSource CreateSFXSource()
    {
        GameObject sfxObj = new GameObject("SFX Source");
        sfxObj.transform.parent = transform;
        AudioSource newSource = sfxObj.AddComponent<AudioSource>();
        return newSource;
    }
}
