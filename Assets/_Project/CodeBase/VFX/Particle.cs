using UnityEngine;

namespace _Project.CodeBase.VFX
{
    public class Particle
    {
        private readonly ParticleSystem _particleSystem;

        public Particle(ParticleSystem particleSystem)
        {
            _particleSystem = particleSystem;
        }

        public void StarParticle(Vector3 position)
        {
            _particleSystem.transform.position = position;
            _particleSystem.Play();
        }
    }
}