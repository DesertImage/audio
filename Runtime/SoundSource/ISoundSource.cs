using System;
using UnityEngine;

namespace DesertImage.Audio
{
    public interface ISoundSource : IPoolable
    {
        event Action<ISoundSource> Disposed;
        event Action<ISoundSource> Finished;

        AudioClip Clip { get; }

        void Play(AudioClip clip);
        void Play(AudioClip clip, bool isLooped);
        void Play(AudioClip clip, float volume);
        void Play(AudioClip clip, float volume, bool isLooped);
        void Play(AudioClip clip, float volume, float pitch);
        void Play(AudioClip clip, float volume, float pitch, bool isLooped);

        void Stop();
        void Pause();

        void Dispose();
    }
}