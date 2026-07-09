public interface IPooledObject
{
    /// <summary>
    /// プールから取得され、アクティブになった直後に呼ばれる
    /// </summary>
    void OnSpawned();

    /// <summary>
    /// プールに返却され、非アクティブになる直前に呼ばれる
    /// </summary>
    void OnDespawned();
}