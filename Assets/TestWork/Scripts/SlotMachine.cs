using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Reel _reel;
    [SerializeField] private SlotVFX _slotVFX;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _stopButton;
    [SerializeField] private float _disableStopButtonTime;

    private Coroutine _spinning;
    public bool IsSpinning => _spinning != null;

    private void Awake()
    {
        _stopButton.enabled = false;
    }

    public void StartSpinnig()
    {
        _startButton.enabled = false;
        _slotVFX.StartSpawnParticles();
        StartCoroutine(DisableStopButtonRoutine());
        _spinning = StartCoroutine(_reel.StartSpinning());
    }

    public void StopSpinning() => StartCoroutine(StopSpinRoutine());

    private IEnumerator StopSpinRoutine()
    {
        _slotVFX.StopSpawnParticles();
        StopCoroutine(_spinning);
        yield return _spinning = StartCoroutine(_reel.StopSpinning());

        StopCoroutine(_spinning);
        yield return _spinning = StartCoroutine(_reel.SnapToNearestSymbol());

        SymbolType symbol = (SymbolType)_reel.GetCurrentSymbolIndex();
        Debug.Log(symbol);

        _startButton.enabled = true;
        _stopButton.enabled = true;

        StopCoroutine(_spinning);
        _spinning = null;
    }

    private IEnumerator DisableStopButtonRoutine()
    {
        _stopButton.enabled = false;
        yield return new WaitForSeconds(_disableStopButtonTime);
        _stopButton.enabled = true;
    }
}
