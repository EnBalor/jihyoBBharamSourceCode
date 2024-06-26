using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    [Range(0f, 1f)] private float musicVolume;

    private AudioSource audioSource;

    protected override void Awake()
    {
        base.Awake();

        audioSource = GetComponent<AudioSource>();
        audioSource.volume = musicVolume;
        audioSource.loop = true;
    }

    private void Start()
    {
        //ChangeBGM();
    }

    public void ChangeBGM(AudioClip musicClip)
    {
        audioSource.Stop();

        audioSource.clip = musicClip;

        audioSource.Play();
    }

    public static void PlayClip(AudioClip clip)
    {
        //GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool("");

        //obj.SetActive(true);

        //SoundSource soundSource = obj.GetComponent<SoundSource>();
        //soundSource.Play(clip, );
    }
}
