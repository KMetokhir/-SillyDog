using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BladderView : MonoBehaviour
{

    [SerializeField] private TMP_Text _peeLevel;
    [SerializeField] private Slider _slider;

    private Tween _cureentTween;

    public void SetPeeLevel(int level, int maxLevel, float velocity)
    {
        _peeLevel.text = $" Pee Level {level}    Max {maxLevel}";

        if (_cureentTween != null)
        {
            _cureentTween.Kill();
        }
        _slider.maxValue = maxLevel;
        _cureentTween = _slider.DOValue(level, velocity).SetEase(Ease.Linear);

    }

    private void OnDisable()
    {
        _cureentTween.Kill();
    }
}
