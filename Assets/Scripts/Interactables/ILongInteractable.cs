using System;

public interface ILongInteractable : IInteractable
{
    event Action<float> ProgressChanged;
    float Progress { get; }
    void StopInteraction();
}
