using System.Collections;
using UnityEngine;

public class SlotVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private float _duration;
    [SerializeField] private float _startValue;
    [SerializeField] private float _endVaue;

    private Coroutine _spawningParticles;

    public void StartSpawnParticles()
    {
        _spawningParticles = StartCoroutine(ChangeEmissionOverTime(_startValue, _endVaue, _duration));
    }

    public void StopSpawnParticles()
    {
        _spawningParticles = StartCoroutine(ChangeEmissionOverTime(_endVaue, _startValue, _duration));
    }

    private IEnumerator ChangeEmissionOverTime(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float value = Mathf.Lerp(from, to, elapsed / duration);

            foreach (var particle in _particles)
            {
                var emission = particle.emission;
                emission.rateOverTime = value;
            }

            yield return null;
        }

        foreach (var particle in _particles)
        {
            var emission = particle.emission;
            emission.rateOverTime = to;
        }
    }
}
