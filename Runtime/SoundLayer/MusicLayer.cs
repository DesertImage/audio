namespace DesertImage.Audio
{
    public class MusicLayer : AudioLayerAbstract, IAudioLayer
    {
        private AudioLayerSettings _settings;

        private ISoundSource _current;
        
        public override void Play(ISoundSource sound, bool isLooped = false)
        {
            sound.Play(sound.Clip, _settings.Volume, _settings.Pitch, isLooped);
        }

        public override void Stop(ISoundSource sound)
        {
            sound.Dispose();
        }

        public override void StopAll() => Stop(_current);

        private void SoundOnDisposed(ISoundSource sound)
        {
            
        }
    }
}