#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections.Generic;

public class UnityToHorizontalSheet : EditorWindow
{
    // ★既存の神システムが想定している「1コマの正確なサイズ」を指定してください
    private static readonly Vector2Int cellSize = new Vector2Int(128, 128);

    [MenuItem("Tools/バラバラの画像を横一列のアニメーションシートに結合")]
    public static void PackToHorizontalSheet()
    {
        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (string.IsNullOrEmpty(path) || !AssetDatabase.IsValidFolder(path))
        {
            EditorUtility.DisplayDialog("エラー", "画像をまとめた【フォルダ】を選択してから実行してください。", "OK");
            return;
        }

        string[] GUIDs = AssetDatabase.FindAssets("t:Texture2D", new[] { path });

        // 1. 元のバラバラの画像を確実に読み書き可能（Readable）にする
        foreach (var guid in GUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
            if (importer != null && !importer.isReadable)
            {
                importer.isReadable = true;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.SaveAndReimport();
            }
        }

        // 2. 名前順（連番順）にソートしてテクスチャを取得
        Texture2D[] textures = GUIDs
            .Select(guid => AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(guid)))
            .OrderBy(t => t.name)
            .ToArray();

        if (textures.Length == 0)
        {
            EditorUtility.DisplayDialog("エラー", "フォルダ内に画像が見つかりませんでした。", "OK");
            return;
        }

        int count = textures.Length;
        int atlasWidth = cellSize.x * count;
        int atlasHeight = cellSize.y;

        // 3. 提示されたスクリプトと同じ「正確なサイズの横長シート」をメモリ上に新規作成
        Texture2D newAtlas = new Texture2D(atlasWidth, atlasHeight, TextureFormat.RGBA32, false);
        Color[] blankPixels = Enumerable.Repeat(Color.clear, atlasWidth * atlasHeight).ToArray();
        newAtlas.SetPixels(blankPixels);

        // 4. 名前順に、左から順番に元画像を1マスずつコピペしていく
        for (int i = 0; i < count; i++)
        {
            Texture2D sourceTexture = textures[i];

            // 元画像からピクセルデータを取得（サイズが違ってもcellSizeで安全に切り取る）
            int readWidth = Mathf.Min(sourceTexture.width, cellSize.x);
            int readHeight = Mathf.Min(sourceTexture.height, cellSize.y);
            Color[] pixels = sourceTexture.GetPixels(0, 0, readWidth, readHeight);

            // もし元画像が指定サイズより小さければ透明で埋めるための処理
            if (readWidth < cellSize.x || readHeight < cellSize.y)
            {
                Color[] adjustedPixels = Enumerable.Repeat(Color.clear, cellSize.x * cellSize.y).ToArray();
                for (int y = 0; y < readHeight; y++)
                {
                    for (int x = 0; x < readWidth; x++)
                    {
                        adjustedPixels[y * cellSize.x + x] = pixels[y * readWidth + x];
                    }
                }
                pixels = adjustedPixels;
            }

            // 横一列の指定位置（i番目のマス）にピクセルを書き込む
            newAtlas.SetPixels(i * cellSize.x, 0, cellSize.x, cellSize.y, pixels);
        }

        newAtlas.Apply();

        // 5. フォルダ名と同じ名前のPNGとして保存
        string folderName = Path.GetFileName(path);
        string savePath = Path.Combine(path, $"{folderName}.png").Replace("\\", "/");
        byte[] bytes = newAtlas.EncodeToPNG();
        File.WriteAllBytes(savePath, bytes);

        DestroyImmediate(newAtlas);
        AssetDatabase.Refresh();

        // 6. 出力されたPNGを自動で「Multiple」にして、等間隔に完璧スライスする
        TextureImporter atlasImporter = AssetImporter.GetAtPath(savePath) as TextureImporter;
        if (atlasImporter != null)
        {
            atlasImporter.spriteImportMode = SpriteImportMode.Multiple;
            atlasImporter.isReadable = true;

            var spriteRectList = new List<SpriteRect>();
            for (int i = 0; i < count; i++)
            {
                SpriteRect spriteRect = new SpriteRect();
                // 既存の神システムが迷わないよう、元の画像名（連番）をそのままスプライト名に割り振る
                spriteRect.name = textures[i].name;

                // 左から順番に等間隔（cellSize分）の切り取り枠を設定
                spriteRect.rect = new Rect(i * cellSize.x, 0, cellSize.x, cellSize.y);
                spriteRect.alignment = SpriteAlignment.Center;
                spriteRect.pivot = new Vector2(0.5f, 0.5f);
                spriteRectList.Add(spriteRect);
            }

            var factory = new UnityEditor.U2D.Sprites.SpriteDataProviderFactories();
            factory.Init();
            var dataProvider = factory.GetSpriteEditorDataProviderFromObject(atlasImporter);
            dataProvider.InitSpriteEditorDataProvider();

            dataProvider.SetSpriteRects(spriteRectList.ToArray());
            dataProvider.Apply();

            atlasImporter.SaveAndReimport();
        }

        EditorUtility.DisplayDialog("成功", $"横一列のシートへの結合と、自動スライスが完了しました！\n{savePath}", "OK");
    }
}
#endif

