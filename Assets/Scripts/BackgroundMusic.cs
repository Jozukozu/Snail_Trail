using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{

    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1.0f);
        SetVolume(musicVolume);
        SceneManager.LoadScene("StartScene");
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }

    public void SetVolume(float volume)
    {
        _audioSource.volume = volume;
    }
}
