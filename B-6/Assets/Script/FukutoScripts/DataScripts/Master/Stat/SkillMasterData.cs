using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スキルマスターデータ
/// 
/// スキルデータの骨格のクラス
/// </summary>
[Serializable]
public class SkillMasterData : MonoBehaviour
{
    /// <summary>
    /// エンティティ
    /// 
    /// データの実体
    /// </summary>
    [Serializable]
    public class Entity
    {
        public int level; // レベル
        public int stat; // ステータス
    }

    // データをリスト化する変数
    public List<Entity> entities;
}
