using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace CartoonFX
{
    [InitializeOnLoad]
    public class CFXR_WelcomeScreen : EditorWindow
    {
        static CFXR_WelcomeScreen()
        {
            EditorApplication.delayCall += () =>
            {
                if (SessionState.GetBool("CFXR_WelcomeScreen_Shown", false))
                {
                    return;
                }
            SessionState.SetBool("CFXR_WelcomeScreen_Shown", true);

                var importer = AssetImporter.GetAtPath(AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb"));
                if (importer != null && importer.userData == "dontshow")
                {
                    return;
                }

                Open();
            };
        }

        [MenuItem("Tools/Cartoon FX Remaster FREE - Welcome Screen")]
        static void Open()
        {
            var window = GetWindow<CFXR_WelcomeScreen>(true, "Cartoon FX Remaster FREE", true);
            window.minSize = new Vector2(516, 370);
            window.maxSize = new Vector2(516, 370);
        }

        private void CreateGUI()
        {
            VisualElement root = rootVisualElement;
            root.style.height = new StyleLength(new Length(100, LengthUnit.Percent));

            // UXML
            var uxmlPath = AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb");
            var uxmlDocument = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);

            if (uxmlDocument == null)
            {
                Debug.LogWarning("CartoonFX Welcome Screen ÇÃUXMLÇ™å©Ç¬Ç©ÇËÇ‹ÇπÇÒÅB");
                return;
            }

            root.Add(uxmlDocument.Instantiate());

            // USS
            var stylePath = AssetDatabase.GUIDToAssetPath("f8b971f10a610844f968f582415df874");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(stylePath);

            if (styleSheet != null)
            {
                root.styleSheets.Add(styleSheet);
            }

            // Background image
            var bgPath = AssetDatabase.GUIDToAssetPath("fed1b64fd853f994c8d504720a0a6d44");
            var bgTexture = AssetDatabase.LoadAssetAtPath<Texture2D>(bgPath);

            if (bgTexture != null)
            {
                root.style.backgroundImage = new StyleBackground(bgTexture);
            }

            root.style.unityBackgroundScaleMode = ScaleMode.ScaleAndCrop;

            // Logo image
            var titleImage = root.Q<Image>("img_title");

            if (titleImage != null)
            {
                var titlePath = AssetDatabase.GUIDToAssetPath("a665b2e53088caa4c89dd09f9c889f62");
                titleImage.image = AssetDatabase.LoadAssetAtPath<Texture2D>(titlePath);
            }

            // Buttons
            var btn1 = root.Q<Label>("btn_cfxr1");
            if (btn1 != null)
                btn1.AddManipulator(new Clickable(evt => Application.OpenURL("https://assetstore.unity.com/packages/slug/4010")));

            var btn2 = root.Q<Label>("btn_cfxr2");
            if (btn2 != null)
                btn2.AddManipulator(new Clickable(evt => Application.OpenURL("https://assetstore.unity.com/packages/slug/4274")));

            var btn3 = root.Q<Label>("btn_cfxr3");
            if (btn3 != null)
                btn3.AddManipulator(new Clickable(evt => Application.OpenURL("https://assetstore.unity.com/packages/slug/10172")));

            var btn4 = root.Q<Label>("btn_cfxr4");
            if (btn4 != null)
                btn4.AddManipulator(new Clickable(evt => Application.OpenURL("https://assetstore.unity.com/packages/slug/23634")));

            var bundle = root.Q<Label>("btn_cfxrbundle");
            if (bundle != null)
                bundle.AddManipulator(new Clickable(evt => Application.OpenURL("https://assetstore.unity.com/packages/slug/232385")));

            var closeDontShow = root.Q<Button>("close_dontshow");
            if (closeDontShow != null)
            {
                closeDontShow.RegisterCallback<ClickEvent>(evt =>
                {
                    Close();

                    var importer = AssetImporter.GetAtPath(
                        AssetDatabase.GUIDToAssetPath("bfd03f272fe010b4ba558a3bc456ffeb")
                    );

                    if (importer != null)
                    {
                        importer.userData = "dontshow";
                        importer.SaveAndReimport();
                    }
                });
            }

            var close = root.Q<Button>("close");
            if (close != null)
            {
                close.RegisterCallback<ClickEvent>(evt =>
                {
                    Close();
                });
            }
        }
    }
}
