using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound Manager", menuName = "Managers/Sound Manager")]
public class SoundManager : ScriptableObject
{
    [Header("Audio Clips")]
    [SerializeField] AudioClip _buttonClickSFX;
    [Header("Variables")]
    [SerializeField] FloatVariable _masterVolume;
    [SerializeField] FloatVariable _sfxVolume;
    [SerializeField] FloatVariable _bgmVolume;
    [SerializeField] FloatVariable _voVolume;
    [SerializeField] BoolVariable _masterToggle;
    [SerializeField] BoolVariable _bgmToggle;
    [SerializeField] BoolVariable _sfxToggle;
    [SerializeField] BoolVariable _voToggle;
    [Header("Components")]
    [SerializeField] GameObject _audioSource;
    AudioSource _sfxSource, _bgmSource, _voSource;
    Coroutine _bgmFadeOutTask;

    public float bgmProgress
    {
        get
        {
            float progress = 0;
            if (_bgmSource != null)
            {
                progress = _bgmSource.time / _bgmSource.clip.length;
            }
            return progress;
        }
    }

    public void PlaySFX(AudioClip p_audioClip)
    {
        float volume;
        if (_sfxSource == null) _sfxSource = CreateAudioSource("SFX Source");

        if (!_masterToggle.value || !_sfxToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _sfxVolume.value;
        }

        _sfxSource.PlayOneShot(p_audioClip, volume);
    }

    public void PlayBGM(AudioClip p_audioClip, bool p_fresh = false)
    {
        if (_bgmSource == null) _bgmSource = CreateAudioSource("BGM Source");

        if (!p_fresh)
        {
            if (_bgmSource.clip == p_audioClip)
            {
                if (_bgmSource.isPlaying)
                {
                    return;
                }
                else
                {
                    _bgmSource.UnPause();
                    return;
                }
            }
        }

        float volume;
        if (!_masterToggle.value || !_bgmToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _bgmVolume.value;
        }

        _bgmSource.clip = p_audioClip;
        _bgmSource.volume = volume;
        _bgmSource.loop = true;
        _bgmSource.Play();
    }

    public void PlayVO(AudioClip p_audioClip)
    {
        float volume;
        if (_voSource == null) _voSource = CreateAudioSource("VO Source");

        if (!_masterToggle.value || !_voToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _voVolume.value;
        }

        _voSource.clip = p_audioClip;
        _voSource.volume = volume;
        _voSource.Play();
    }

    public void PlayButtonClickSFX()
    {
        PlaySFX(_buttonClickSFX);
    }

    public void UpdateBGMSourceVolume()
    {
        if (_bgmSource == null) return;

        float volume;
        if (_bgmSource == null) _bgmSource = CreateAudioSource("SFX Source");

        if (!_masterToggle.value || !_bgmToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _bgmVolume.value;
        }

        _bgmSource.volume = volume;
    }

    public void UpdateVOSourceVolume()
    {
        if (_voSource == null) return;

        float volume;
        if (!_masterToggle.value || !_voToggle.value)
        {
            volume = 0f;
        }
        else
        {
            volume = _masterVolume.value * _voVolume.value;
        }

        _voSource.volume = volume;
    }

    public void PauseBGM()
    {
        _bgmSource.Pause();
    }

    public void FadeOutBGM(float p_duration)
    {
        if (_bgmFadeOutTask != null) return;
        _bgmFadeOutTask = _bgmSource.GetComponent<AudioSourceObject>().StartCoroutine(FadeOutBGMTask(p_duration));
    }

    private IEnumerator FadeOutBGMTask(float p_duration)
    {
        float timer = 0f;
        float startingVolume = _bgmSource.volume;
        while (timer < p_duration)
        {
            timer += Time.deltaTime;
            float newVolume = Mathf.Lerp(startingVolume, 0f, timer / p_duration);
            _bgmSource.volume = newVolume;
            yield return null;
        }
        _bgmFadeOutTask = null;
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void StopVO()
    {
        _voSource.Stop();
    }

    private AudioSource CreateAudioSource(string p_name)
    {
        GameObject audioSource = Instantiate(_audioSource);
        DontDestroyOnLoad(audioSource);
        audioSource.name = p_name;
        return audioSource.GetComponent<AudioSource>();
    }
}
