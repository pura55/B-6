using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// アトラスディバイダ―
/// 
/// 一枚のアトラステクスチャーから種類ごとの
/// アトラステクスチャーに分離するスクリプト
/// 
/// 注：このスクリプトはゲーム内で動くのではなく
/// 　　エディターに対して効果がある。
/// 　　フルAIスクリプト
/// 　　
/// 使い方：①セルサイズを指定して分離
/// 　　　　②分離したい画像で右クリック
/// 　　　　③メニュー内にある本スクリプトを選択
/// 　　　　④画像は既にスライスされている前提
/// </summary>
public class AtlasDivider : EditorWindow
{
    // ★大元のスプライトシートの1コマの正確なサイズを指定してください
    private static readonly Vector2Int cellSize = new Vector2Int(140, 93); // 例: 140x93 など

    [MenuItem("Assets/スプライトを役割ごとのアトラス(.png)に再構築して書き出し", false, 11)]
    private static void DivideAndPackAtlas()
    {
        Texture2D sourceTexture = Selection.activeObject as Texture2D;
        if (sourceTexture == null)
        {
            Debug.LogError("アトラス画像（Texture2D）を選択した状態で実行してください。");
            return;
        }

        string atlasPath = AssetDatabase.GetAssetPath(sourceTexture);
        string baseDirectory = Path.GetDirectoryName(atlasPath);

        // テクスチャ設定を一時的に読み書き可能に変更
        TextureImporter textureImporter = AssetImporter.GetAtPath(atlasPath) as TextureImporter;
        if (textureImporter == null) return;

        bool originalIsReadable = textureImporter.isReadable;
        TextureImporterCompression originalCompression = textureImporter.textureCompression;

        textureImporter.isReadable = true;
        textureImporter.textureCompression = TextureImporterCompression.Uncompressed;
        textureImporter.SaveAndReimport();

        // Sprite Editorで設定されているスプライト情報をすべて取得
        Object[] assets = AssetDatabase.LoadAllAssetsAtPath(atlasPath);
        List<Sprite> allSprites = assets.OfType<Sprite>().ToList();

        if (allSprites.Count == 0)
        {
            Debug.LogError("画像がスライスされていません。Sprite Editorでスライスした状態で実行してください。");
            return;
        }

        // 1. 各スプライトの中心座標(正規化ではなくピクセル単位)を計算し、左上から右下への順番で並び替える
        // Unityのテクスチャ座標は「左下が原点(0,0)」なので、Y座標は大きい順（上から下）、X座標は小さい順（左から右）
        var sortedSprites = allSprites.OrderByDescending(s => s.bounds.center.y)
                                      .ThenBy(s => s.bounds.center.x)
                                      .ToList();

        // 2. 順番に走査し、名前の役割（プレフィックス）が変わるごとにグループ分けする
        List<List<Sprite>> groups = new List<List<Sprite>>();
        List<Sprite> currentGroup = new List<Sprite>();
        string currentCategory = "";

        foreach (var sprite in sortedSprites)
        {
            string category = sprite.name.Split('_')[0]; // "Attack" や "Idle" など

            if (currentGroup.Count == 0)
            {
                currentCategory = category;
                currentGroup.Add(sprite);
            }
            else if (category == currentCategory)
            {
                currentGroup.Add(sprite);
            }
            else
            {
                // 名前の種類が変わったら現在のグループを確定し、新しいグループを作る
                groups.Add(currentGroup);
                currentGroup = new List<Sprite> { sprite };
                currentCategory = category;
            }
        }
        if (currentGroup.Count > 0) groups.Add(currentGroup);

        // 3. 各グループごとに、元のグリッド空間のマス目をそのまま維持して新しいアトラスにコピーする
        foreach (var group in groups)
        {
            string categoryName = group[0].name.Split('_')[0];
            int count = group.Count;

            // 横一列の新しいアトラスを作成
            int atlasWidth = cellSize.x * count;
            int atlasHeight = cellSize.y;

            Texture2D newAtlas = new Texture2D(atlasWidth, atlasHeight, TextureFormat.RGBA32, false);
            Color[] blankPixels = Enumerable.Repeat(Color.clear, atlasWidth * atlasHeight).ToArray();
            newAtlas.SetPixels(blankPixels);

            for (int i = 0; i < count; i++)
            {
                Sprite sprite = group[i];

                // 元のSprite Editorの「切り出し枠（Rect）」ではなく、
                // そのスプライトの中心が属している「元画像の均等なグリッドの1マス」の左下座標を逆算する
                Vector2 posInTexture = sprite.rect.position + sprite.rect.size * 0.5f; // スプライトの中心点

                int gridX = Mathf.FloorToInt(posInTexture.x / cellSize.x) * cellSize.x;
                int gridY = Mathf.FloorToInt(posInTexture.y / cellSize.y) * cellSize.y;

                // 元画像から1マス分（cellSize）の空間を丸ごとコピー
                if (gridX >= 0 && gridY >= 0 && gridX + cellSize.x <= sourceTexture.width && gridY + cellSize.y <= sourceTexture.height)
                {
                    Color[] pixels = sourceTexture.GetPixels(gridX, gridY, cellSize.x, cellSize.y);
                    newAtlas.SetPixels(i * cellSize.x, 0, cellSize.x, cellSize.y, pixels);
                }
                else
                {
                    Debug.LogWarning($"スプライト {sprite.name} のグリッド座標が元画像からはみ出ているためスキップしました。");
                }
            }

            newAtlas.Apply();

            // PNGとして保存
            byte[] bytes = newAtlas.EncodeToPNG();
            string savePath = Path.Combine(baseDirectory, $"{categoryName}_Atlas.png").Replace("\\", "/");
            File.WriteAllBytes(savePath, bytes);

            DestroyImmediate(newAtlas);
            Debug.Log($"【成功】位置ズレなしアトラスを書き出しました: {savePath} (計 {count} 枚)");
        }

        // 設定を元に戻す
        textureImporter.isReadable = originalIsReadable;
        textureImporter.textureCompression = originalCompression;
        textureImporter.SaveAndReimport();

        AssetDatabase.Refresh();
    }
}