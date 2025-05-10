using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;

    [Header("Music Playlist")]
    public AudioClip[] playlist;

    private bool player_alive = true;
    private bool isTransitioning = false;
    public float fadeDuration = 2f;
    public float transitionStartTime = 5f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayRandomTrack();
    }

    private void Update()
    {
        if (!player_alive || isTransitioning) return;

        if (musicSource.isPlaying && musicSource.time >= musicSource.clip.length - transitionStartTime)
        {
            StartCoroutine(HandleTransition());
        }

        if (!musicSource.isPlaying)
        {
            StartCoroutine(PlayRandomTrackWithFade());
        }
    }

    private IEnumerator HandleTransition()
    {
        isTransitioning = true;

        yield return StartCoroutine(FadeOut());

        yield return new WaitForSeconds(transitionStartTime);

        yield return StartCoroutine(PlayRandomTrackWithFade());

        isTransitioning = false;
    }

    private IEnumerator PlayRandomTrackWithFade()
    {
        if (playlist == null || playlist.Length == 0)
        {
            Debug.LogWarning("Playlist is empty or not assigned.");
            yield break;
        }

        int index = Random.Range(0, playlist.Length);
        AudioClip nextClip = playlist[index];

        musicSource.clip = nextClip;

        ApplyRandomPitch(); 

        yield return StartCoroutine(FadeIn());
    }

    private void PlayRandomTrack()
    {
        if (playlist != null && playlist.Length > 0)
        {
            int index = Random.Range(0, playlist.Length);
            musicSource.clip = playlist[index];
            ApplyRandomPitch(); 
            musicSource.volume = 0.4f;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Playlist is empty or not assigned.");
        }
    }

    private void ApplyRandomPitch()
    {
        musicSource.pitch = Random.Range(0.95f, 1.05f);
    }

    private IEnumerator FadeOut()
    {
        if (!musicSource.isPlaying) yield break;

        float startVolume = musicSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = 0f;
        musicSource.Stop();
    }

    private IEnumerator FadeIn()
    {
        float targetVolume = 1f;
        musicSource.volume = 0f;
        musicSource.Play();

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, targetVolume, t / fadeDuration);
            yield return null;
        }

        musicSource.volume = targetVolume;
    }

    public void StopMusic()
    {
        StopAllCoroutines();
        player_alive = false;
        StartCoroutine(FadeOutThenDestroy());
    }

    private IEnumerator FadeOutThenDestroy()
    {
        yield return StartCoroutine(FadeOut());
        Destroy(gameObject);
    }
}
