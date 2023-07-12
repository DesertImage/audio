namespace DesertImage.Audio
{
    public interface IAudioLayer
    {
        void Play(ISoundSource sound, bool isLooped = false);
        void Stop(ISoundSource sound);

        void StopAll();

        void SetVolume(float value);
        void SetPitch(float value);

        ref AudioLayerSettings GetSettings();
        void Setup(AudioLayerSettings settings);
    }
}