using AxGrid.Base;
using AxGrid.Model;
using AxGrid.Path;
using System;
using System.Collections;
using System.Reflection;
using UnityEngine;

public class ReelController : MonoBehaviourExtBind
{
    [SerializeField] private RectTransform _reelTransform;
    [SerializeField] private float _spinningMaxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _deceleration;

    private static readonly string CurrentSymbol = "CurrentSymbol";
    private float _currentSpeed = 0;
    private float _snapDuration = 0.5f;
    private static int _symbolsCount = 5;
    private static float _symbolHeight = 256f;
    private float _spinOffset = 1.5f;
    private float _symbolOffset = _symbolHeight / 2;
    private Coroutine _spinning;


    [Bind("StartReel")]
    public void StartSpinning()
    {
        _spinning = StartCoroutine(StartSpinningRoutine());
    }

    [Bind("StopReel")]
    private void StopSpinning()
    {
        StopCoroutine(_spinning);
        _spinning = StartCoroutine(StopSpinningRoutine());
    }

    [Bind("SnapReelToSymbol")]
    private void SnapToSymbol()
    {
        StartCoroutine(SnapToSymbolRoutine());
    }

    private IEnumerator StartSpinningRoutine()
    {
        _currentSpeed = 0f;
        while (true)
        {
            MoveReel(_spinningMaxSpeed);
            yield return null;
        }
    }

    private IEnumerator StopSpinningRoutine()
    {
        while (_currentSpeed > 0)
        {
            MoveReel(0);
            yield return null;
        }
        StopCoroutine(_spinning);
        _spinning = null;
    } 

    private void MoveReel(float target)
    {
        _currentSpeed = Mathf.MoveTowards(_currentSpeed, target, _deceleration * Time.deltaTime);
        _reelTransform.anchoredPosition += Vector2.down * _currentSpeed * Time.deltaTime;

        if (_reelTransform.anchoredPosition.y < -(_symbolsCount - _spinOffset) * _symbolHeight)
            _reelTransform.anchoredPosition += new Vector2(0, _symbolsCount * _symbolHeight);
    }

    private IEnumerator SnapToSymbolRoutine()
    {
        float targetY =
            Mathf.Round((_reelTransform.anchoredPosition.y - _symbolOffset) / _symbolHeight)
            * _symbolHeight
            + _symbolOffset;
        Vector2 targetPosition = new Vector2(_reelTransform.anchoredPosition.x, targetY);
        Vector2 startPosition = _reelTransform.anchoredPosition;
        float elapsed = 0;

        while (elapsed < _snapDuration)
        {
            elapsed += Time.deltaTime;
            _reelTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsed / _snapDuration);
            yield return null;
        }

        _reelTransform.anchoredPosition = targetPosition;

        int symbol = GetCurrentSymbolIndex();
        Model.Set(CurrentSymbol, symbol);
        Debug.Log((SymbolType)symbol);
    }

    public int GetCurrentSymbolIndex()
    {
        float loopHeight = _symbolsCount * _symbolHeight;
        float y = _reelTransform.anchoredPosition.y;
        float normalizedY = Mathf.Repeat(y, loopHeight);
        float adjustedY = normalizedY - _symbolOffset;
        int index = Mathf.RoundToInt(adjustedY / _symbolHeight);
        index = (index % _symbolsCount + _symbolsCount) % _symbolsCount;
        return index;
    }
}
