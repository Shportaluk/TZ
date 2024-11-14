using UnityEngine;

public class BasePoolElement : MonoBehaviour, IPoolElement
{
    public bool IsUsing { get; private set; }

    public virtual void SetUse()
    {
        IsUsing = true;
    }

    public virtual void SetUnUse()
    {
        IsUsing = false;
    }
}