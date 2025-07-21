using UnityEngine;

public abstract class Gimmick : MonoBehaviour
{
    [SerializeField] int _unlockWhen = 1;
    public int UnLockWhen => _unlockWhen;
    float _activeTime;
    [SerializeField] Vector2 randomCDRange;
    public bool IsActive { get; private set; } = false;
    float currentCD;

    public void AddCD()
    {
        currentCD += Time.deltaTime;
        IsActive = currentCD >= _activeTime;
    }
    public void ResetCD()
    {
        _activeTime = Random.Range(randomCDRange.x, randomCDRange.y);
        currentCD = 0;
    }

    public abstract void Active();
}
