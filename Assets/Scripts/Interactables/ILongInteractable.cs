using System;
using UnityEngine.Events;

public interface ILongInteractable : IInteractable
{
    UnityEvent<float> ProgressChangedUnityEvent { get; }
    event Action<float> ProgressChanged;
    float Progress { get; }
    void StopInteraction();
}
