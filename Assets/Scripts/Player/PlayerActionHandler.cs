using System.Collections;
using UnityEngine;
using PulsarDevKit.Scripts.Debug;

// List of all actions player could execute
public enum EActionState
{
    Idle,
    Walk,
    Run,
    Pickup,
    Chop
}

// A struct holding data necessary to executing Action
[System.Serializable]
public struct ActionStateData
{
    public EActionState ActionState;
    public float Duration;
    public Color TempColor;
}

public class PlayerActionHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    [SerializeField] private ActionStateData[] _actionStateData;

    private bool _isInAction;

    public bool IsInAction => _isInAction;

    public void ExecuteAction(EActionState state)
    {
        foreach(ActionStateData data in _actionStateData)
        {
            if(!_isInAction && data.ActionState == state)
            {
                StartCoroutine(ActionDelay(data));
                break;
            }
        }
    }

    public IEnumerator ActionDelay(ActionStateData data)
    {
        _isInAction = true;
        _playerSpriteRenderer.color = data.TempColor;
        yield return new WaitForSeconds(data.Duration);
        _isInAction = false;
    }
}
