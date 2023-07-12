using System;
using UnityEngine;

namespace DesertImage.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundSource : MonoBehaviour, ISoundSource, ITick
    {
        public event Action<ISoundSource> Disposed;
        public event Action<ISoundSource> Finished;

        public AudioClip Clip => audioSource.clip;

        [SerializeField] private AudioSource audioSource;

        public void OnCreate() => audioSource.playOnAwake = false;

        public void ReturnToPool()
        {
#if UNITY_EDITOR
            name = "Inactive";
#endif
            audioSource.enabled = false;

            audioSource.loop = false;
            audioSource.pitch = 1f;
            audioSource.volume = 1f;
        }

        #region PLAY

        public void Play(AudioClip clip)
        {
#if UNITY_EDITOR
            name = $"{clip.name}";
#endif
            audioSource.enabled = true;
            audioSource.Play();
        }

        public void Play(AudioClip clip, bool isLooped)
        {
            audioSource.loop = isLooped;
            Play(clip);
        }

        public void Play(AudioClip clip, float volume)
        {
            audioSource.volume = volume;
            Play(clip, false);
        }

        public void Play(AudioClip clip, float volume, bool isLooped)
        {
            audioSource.volume = volume;
            audioSource.loop = isLooped;
            Play(clip);
        }

        public void Play(AudioClip clip, float volume, float pitch)
        {
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            Play(clip, false);
        }

        public void Play(AudioClip clip, float volume, float pitch, bool isLooped)
        {
            audioSource.volume = volume;
            audioSource.pitch = pitch;
            Play(clip, isLooped);
        }

        #endregion

        public void Stop() => audioSource.Stop();
        public void Pause() => audioSource.Pause();

        public void Tick(float deltaTime)
        {
            if (audioSource.isPlaying) return;

            Stop();
            Dispose();
        }

        public void Dispose() => Disposed?.Invoke(this);
    }
}