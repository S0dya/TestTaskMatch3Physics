using Gameplay.Effects;
using UnityEngine;

namespace Tools
{
    public abstract class ObjectEffectBase
    {
        public GameObject ObjectGameObject;

        protected ObjectEffectBase(GameObject objectGameObject)
        {
            ObjectGameObject = objectGameObject;
        }

        public abstract void ActivateEffect();
        public abstract void DeactivateEffect();
    }

    public class ObjectParticleSystem : ObjectEffectBase
    {
        public ParticleSystem EffectParticleSystem;

        public ObjectParticleSystem(GameObject objectGameObject, ParticleSystem effectParticleSystem)
            : base(objectGameObject)
        {
            EffectParticleSystem = effectParticleSystem;
        }

        public override void ActivateEffect()
        {
            ObjectGameObject.SetActive(true);
            EffectParticleSystem.Play();
        }
        public override void DeactivateEffect()
        {
            EffectParticleSystem.Clear();
            ObjectGameObject.SetActive(false);
        }
    }

    public class ObjectParticleSystemPool : PoolBase<ObjectParticleSystem>
    {
        protected override ObjectParticleSystem CreateObject()
        {
            var obj = GameObject.Instantiate(_prefabs[Random.Range(0, _prefabs.Length)], _parent);
            obj.GetComponent<PoolEffectHandler>().Init(Return);

            return new ObjectParticleSystem(obj, obj.GetComponent<ParticleSystem>());
        }
        protected override void OnGet(ObjectParticleSystem objPS) => objPS.ActivateEffect();
        protected override void OnSet(ObjectParticleSystem objPS) => objPS.DeactivateEffect();

        public void Return(GameObject obj, ParticleSystem ps) => Set(new ObjectParticleSystem(obj, ps));
    }
}
