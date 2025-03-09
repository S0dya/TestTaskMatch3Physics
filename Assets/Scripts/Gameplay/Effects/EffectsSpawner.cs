using UnityEngine;
using Tools;

namespace Gameplay.Effects
{
    public class EffectsSpawner
    {
        private ObjectParticleSystemPool _effectsPool = new();

        public void Init(GameObject[] prefabs, Transform parent, int amount)
        {
            _effectsPool.Init(prefabs, parent, amount);
        }

        public void Spawn(Vector2 pos)
        {
            var effect = _effectsPool.Get();
            effect.ObjectGameObject.transform.position = pos;
        }
    }
}
