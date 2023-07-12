using System.Collections.Generic;
using UnityEngine;

namespace DesertImage.Audio
{
    public class SoundSourcesPool : IPool<ISoundSource>
    {
        private readonly Stack<ISoundSource> _stack = new Stack<ISoundSource>();

        private readonly Transform _parent;

        public SoundSourcesPool(Transform parent) => _parent = parent;

        public void Register(int count)
        {
            for (var i = 0; i < count; i++)
            {
                ReturnInstance(CreateInstance());
            }
        }

        public ISoundSource GetInstance()
        {
            var instance = _stack.Count > 0 ? _stack.Pop() : CreateInstance();
            instance.OnCreate();
            return instance;
        }

        public void ReturnInstance(ISoundSource instance) => instance.ReturnToPool();

        private ISoundSource CreateInstance()
        {
            var obj = new GameObject("SoundSource");
            obj.transform.parent = _parent;
            
            var source = obj.AddComponent<SoundSource>();

            source.Disposed += ReturnInstance;

            return source;
        }
    }
}