using UnityEngine;

/// <summary>
/// プールオブジェクトのインターフェース
/// 
/// プールオブジェクトに以下の機能を持たせます
/// </summary>
public interface IObjectPool
{
    /// @brief プールのセットアップを行う関数
    void PoolSetUp(uint index);

    /// @brief プールにから取得する関数
    APooledObject GetFromPool();

    /// @brief プールに返却する関数
    void ReturnToPool(APooledObject pooledObject);
}
