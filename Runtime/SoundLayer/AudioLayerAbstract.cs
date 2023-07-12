namespace DesertImage.Audio
{
    public abstract class AudioLayerAbstract
    {
        private AudioLayerSettings _settings;

        public abstract void Play(ISoundSource sound, bool isLooped = false);
        public abstract void Stop(ISoundSource sound);
        public abstract void StopAll();

        public void SetVolume(float value) => _settings.Volume = value;
        public void SetPitch(float value) => _settings.Pitch = value;

        public ref AudioLayerSettings GetSettings() => ref _settings;
        public void Setup(AudioLayerSettings settings) => _settings = settings;
    }
}