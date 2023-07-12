using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DesertImage.Audio
{
    public class SoundLayer : AudioLayerAbstract, IAudioLayer
    {
        private readonly HashSet<ISoundSource> _playing = new HashSet<ISoundSource>();

        private readonly int _simultaneousSounds;
        private readonly Transform _content;

        private AudioLayerSettings _settings;

        public SoundLayer(Transform content, int simultaneousSounds)
        {
            _content = content;
            _simultaneousSounds = simultaneousSounds;
        }

        public override void Play(ISoundSource sound, bool isLooped = false)
        {
            if (_playing.Count >= _simultaneousSounds)
            {
                _playing.First().Dispose();
            }

            sound.Disposed += SoundOnDisposed;
            _playing.Add(sound);

#if UNITY_EDITOR
            if (sound is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.transform.parent = _content;
            }
#endif
            sound.Play(sound.Clip, _settings.Volume, _settings.Pitch, isLooped);
        }

        public override void Stop(ISoundSource sound)
        {
            if (!_playing.Contains(sound))
            {
#if DEBUG
                throw new Exception("SoundSource is not playing on this layer");
#else
                return;
#endif
            }

            sound.Dispose();
        }

        public override void StopAll()
        {
            while (_playing.Count > 0)
            {
                _playing.First().Dispose();
            }
        }

        private void SoundOnDisposed(ISoundSource sound)
        {
#if UNITY_EDITOR
            if (sound is MonoBehaviour monoBehaviour)
            {
                monoBehaviour.transform.parent = _content;
                monoBehaviour.name = sound.Clip.name;
            }
#endif
            sound.Disposed -= SoundOnDisposed;
            _playing.Remove(sound);
        }
    }
}