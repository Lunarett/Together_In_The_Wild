using UnityEngine;
using PulsarDevKit.Scripts.Debug;

public class Item : MonoBehaviour, IInteractable
{
    public void Interact(PlayerController controller)
    {
        PulseLogger.Success(gameObject.name);
        Destroy(gameObject);
    }
}