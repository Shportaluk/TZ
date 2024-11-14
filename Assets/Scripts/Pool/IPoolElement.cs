public interface IPoolElement
{
    bool IsUsing { get; }

    void SetUse();
    void SetUnUse();
}