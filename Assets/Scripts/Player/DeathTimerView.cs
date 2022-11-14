using TMPro;
using UnityEngine;

public class DeathTimerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;


    public void SetTime(float value)
    {
        _timeText.text = value.ToString();
    }

    public void IsHide(bool isHide)
    {
        _timeText.gameObject.SetActive(!isHide);
    }
}
