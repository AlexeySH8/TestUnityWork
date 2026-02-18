using AxGrid.Base;
using AxGrid.Model;
using System.Collections;
using UnityEngine;

public class SlotVFXController : MonoBehaviourExtBind
{
    [SerializeField] private ParticleSystem[] _particles;
    [SerializeField] private float _duration;
    [SerializeField] private float _startValue;
    [SerializeField] private float _endValue;

    [Bind("StartParticles")]
    private void StartParticles()
    {
        StartCoroutine(ChangeEmission(_startValue, _endValue));
    }

    [Bind("StopParticles")]
    private void StopParticles()
    {
        StartCoroutine(ChangeEmission(_endValue, _startValue));
    }

    private IEnumerator ChangeEmission(float from, float to)
    {
        float elapsed = 0f;
        while (elapsed < _duration)
        {
            elapsed += Time.deltaTime;
            float value = Mathf.Lerp(from, to, elapsed / _duration);
            foreach (var ps in _particles)
            {
                var emission = ps.emission;
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
