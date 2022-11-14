using UnityEngine;
using UnityEngine.Events;

public class TimeInvoker : MonoBehaviour
{
    public event UnityAction<float> UpdateTimeTickedEvent;
    public event UnityAction<float> UpdateTimeUnscaledTickedEvent;
    public event UnityAction OneSecondTickedEvent;
    public event UnityAction OneSecondUnscaledTickedEvent;

    public static TimeInvoker Instance
    {
        get
        {
            if (_instance == null)
            {
                var go = new GameObject("[TIME INVOKER]");
                _instance = go.AddComponent<TimeInvoker>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    private static TimeInvoker _instance;
    private float _oneSecTimer;
    private float _oneSecUnscaledTimer;


    private void Update()
    {
        var deltaTime = Time.deltaTime;
        UpdateTimeTickedEvent?.Invoke(deltaTime);

        _oneSecTimer += deltaTime;
        if (_oneSecTimer >= 1f)
        {
            _oneSecTimer -= 1f;
            OneSecondTickedEvent?.Invoke();
        }

        var unScaledeltaTime = Time.unscaledDeltaTime;
        UpdateTimeUnscaledTickedEvent?.Invoke(unScaledeltaTime);

        _oneSecUnscaledTimer += unScaledeltaTime;
        if (_oneSecUnscaledTimer >= 1f)
        {
            _oneSecUnscaledTimer -= 1f;
            OneSecondUnscaledTickedEvent?.Invoke();
        }
    }
}
