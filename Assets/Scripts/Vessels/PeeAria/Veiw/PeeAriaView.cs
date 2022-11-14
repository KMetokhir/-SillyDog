using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PeeAriaView : MonoBehaviour
{
    [SerializeField] private TMP_Text _peeLevel;
    [SerializeField] private Image _filler;

    private Tween _cureentTween;
    private Vector3 _fillerMaxScale;

    private void Start()
    {
        _fillerMaxScale = _filler.rectTransform.localScale;
        _filler.rectTransform.localScale = Vector3.zero;
    }

    private void OnDisable()
    {
        _cureentTween.Kill();
    }

    public void SetPeeLevel(int level, int maxLevel, float velocity)
    {
        if (_cureentTween != null)
        {
            _cureentTween.Kill();
        }

        _peeLevel.text = $" {level} / {maxLevel}";

        _cureentTween = _filler.rectTransform.DOScale(new Vector3(_fillerMaxScale.x / maxLevel * level, 
            _fillerMaxScale.y / maxLevel * level, _fillerMaxScale.z / maxLevel * level), velocity).SetEase(Ease.Linear);

    }

    
}
