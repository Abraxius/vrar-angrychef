using UnityEngine;
using UnityEngine.Events;

public class OnEnableEventTrigger : MonoBehaviour
{
    // UnityEvent, das du im Inspector konfigurieren kannst
    public UnityEvent OnObjectEnabledEvent;
    
    void OnEnable()
    {
        // Ruft alle verknüpften Methoden über das UnityEvent auf
        if (OnObjectEnabledEvent != null)
        {
            OnObjectEnabledEvent.Invoke();
        }

        AudioSource[] allButtonsSounds = GetComponentsInChildren<AudioSource>();

        foreach (var sound in allButtonsSounds)
        {
            sound.volume = PlayerPrefs.GetFloat("soundVolume");
        }
    }
}
