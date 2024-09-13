using UnityEngine;

public enum SoundType
{
    Flip,
    Match,
    Mismatch,
    GameOver
}

public class AudioManager : MonoBehaviour, IAudioManager
{
    public AudioClip flipSound;
    public AudioClip matchSound;
    public AudioClip mismatchSound;
    public AudioClip gameOverSound;

    private AudioSource _audioSource;

    void Awake()
    {
        ServiceLocator.RegisterService<IAudioManager>(this);
    }

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SoundType type)
    {
        switch (type)
        {
            case SoundType.Flip:
                _audioSource.PlayOneShot(flipSound);
                break;
            case SoundType.Match:
                _audioSource.PlayOneShot(matchSound);
                break;
            case SoundType.Mismatch:
                _audioSource.PlayOneShot(mismatchSound);
                break;
            case SoundType.GameOver:
                _audioSource.PlayOneShot(gameOverSound);
                break;
        }
    }

    void OnDestroy()
    {
        ServiceLocator.UnregisterService<IAudioManager>();
    }
}