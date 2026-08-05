using UnityEngine;

/// <summary>
/// Jsonローダー
/// 
/// Jsonファイルを読み込みデータを格納するクラス
/// </summary>
public class JsonLoader : MonoBehaviour
{
    [SerializeField] private DataRegister dataRegister; // データレジスター
    [SerializeField] private PlayerMasterData playerMasterData; // プレイヤーのマスター
    [SerializeField] private EnemyMasterData enemyMasterData; // エネミーマスターデータ

    void Start()
    {
        // プレイヤーデータ読み込み
        LoadPlayerData();
    }

    /// @brief プレイヤーのデータをロードする関数
    private void LoadPlayerData()
    {
        // テキストアセットとして読み込み
        TextAsset textAsset = Resources.Load<TextAsset>("Json/player_data");

        // アセットがnullではない場合に実行
        if(textAsset != null)
        {
            // 文字列を取得
            string jsonString = textAsset.text;

            // クラスに変換
            PlayerMasterData data = JsonUtility.FromJson<PlayerMasterData>(jsonString);

            // データの登録を行う
            dataRegister.RegistPlayerData(1, data);
        }
        else
        {
            // エラーを出力
            Debug.LogError("JSONファイルの読み込みに失敗しました。");
        }
    }

    /// @brief エネミーのデータをロードする関数
    private void LoadEnemyData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("Json/enemy_data");

        // アセットがnullではない場合に実行
        if (textAsset != null)
        {
            // 文字列を取得
            string jsonString = textAsset.text;

            // クラスに変換
            EnemyMasterData data = JsonUtility.FromJson<EnemyMasterData>(jsonString);

            // データの登録を行う
            dataRegister.RegistEnemyData(1, data);
        }
        else
        {
            // エラーを出力
            Debug.LogError("JSONファイルの読み込みに失敗しました。");
        }
    }
}
