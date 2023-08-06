using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Менеджер отвечает за воспроизведение фоновых звуков, музыки и звуковых эффектов игры
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region Serialize fields

    [SerializeField] private AudioManagerConfig _config;

    [Header("Audio sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    #endregion

    #region Private fields

    private Dictionary<SoundType, AudioClip> _audioClips;

    #endregion

    public enum SoundType
    {
        AmbientMusic,
        ChestCollected,
        TorchSwitch,
        ButtonSelected,
        ButtonClick,
    }

    #region Mono

    private void Start()
    {
        _audioClips = new Dictionary<SoundType, AudioClip>();

        _audioClips.Add(SoundType.AmbientMusic, _config.AmbientMusic);
        _audioClips.Add(SoundType.ChestCollected, _config.ChestCollected);
        _audioClips.Add(SoundType.TorchSwitch, _config.TorchSwitch);
        _audioClips.Add(SoundType.ButtonSelected, _config.ButtonSelected);
        _audioClips.Add(SoundType.ButtonClick, _config.ButtonClick);
    }

    #endregion Mono

    #region Private methods

    /// <summary>
    /// Метод возвращает источник звука для типа звука, который будем использовать
    /// </summary>
    /// <param name="soundType">Тип звука</param>
    /// <returns>Источник для данного типа звука</returns>
    private AudioSource GetAudioSource(SoundType soundType)
    {
        if(soundType is SoundType.AmbientMusic)
            return _musicSource;
        else
            return _sfxSource;
    }

    #endregion Private methods

    #region Public methods

    public void PauseAllAudioSource(bool isPause)
    {
        if (isPause)
        {
            if (_musicSource.isPlaying)
                _musicSource.Pause();
            if (_sfxSource.isPlaying)
                _sfxSource.Pause();
        }
        else
        {
            _musicSource.UnPause();
            _sfxSource.UnPause();
        }
    }

    public void StopAllAudioSource()
    {
        _musicSource.Stop();
        _sfxSource.Stop();
    }

    public void PlaySound(SoundType soundType, float delay = 0)
    {
        // Получаем источник звука для звука определенного типа
        AudioSource audioSource = GetAudioSource(soundType);

        AudioClip audioClip = _audioClips[soundType];

        if (soundType is SoundType.AmbientMusic)
        {
            audioSource.clip = audioClip;
            audioSource.loop = true;

            audioSource.PlayDelayed(delay);

            return;
        }

        audioSource.PlayOneShot(audioClip);
    }

    #endregion Public methods
}
