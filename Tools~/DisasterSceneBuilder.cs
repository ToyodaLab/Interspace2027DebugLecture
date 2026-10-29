using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;
using UnityEngine.UI;

// Builds Assets/Scenes/Disaster.unity. Menu: Disaster > Build Scene.
// Runs once automatically when the scene does not exist yet.
public static class DisasterSceneBuilder
{
    const string ScenePath = "Assets/Scenes/Disaster.unity";
    const string PrefabPath = "Assets/Prefabs/Coin.prefab";
    const string FontPath = "Assets/Fonts/NotoSansJP.ttf";
    const string FontAssetPath = "Assets/Fonts/NotoSansJP SDF.asset";
    const string TmpSettings = "Assets/TextMesh Pro/Resources/TMP Settings.asset";
    const string TmpPackage = "Packages/com.unity.ugui/Package Resources/TMP Essential Resources.unitypackage";

    static TMP_FontAsset jpFont;

    [InitializeOnLoadMethod]
    static void AutoRun()
    {
        if (Application.isBatchMode || File.Exists(ScenePath)) return;
        EditorApplication.delayCall += Begin;
    }

    // Command line: Unity -batchmode -quit -executeMethod DisasterSceneBuilder.BatchBuild
    public static void BatchBuild() => Build();

    // Command line (needs graphics): fills the font atlas and saves a camera snapshot to DISASTER_SHOT
    public static void BatchPopulate()
    {
        var fa = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontAssetPath);
        var scene = EditorSceneManager.OpenScene(ScenePath);
        string chars = "";
        foreach (var t in Object.FindObjectsByType<TMP_Text>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            chars += t.text;
        Debug.Log("Populate returned " + fa.TryAddCharacters(chars + "Score: Lives: 0123456789", out var missing)
                  + ", missing: " + missing);
        EditorUtility.SetDirty(fa);
        AssetDatabase.SaveAssets();

        ShaderUtil.allowAsyncCompilation = false;
        var cam = Camera.main;
        var rt = new RenderTexture(1280, 720, 24);
        cam.rect = new Rect(0, 0, 1, 1);
        cam.targetTexture = rt;
        cam.Render();
        cam.targetTexture = null;
        cam.rect = new Rect(0, 0, 0.65f, 1);
        RenderTexture.active = rt;
        var tex = new Texture2D(1280, 720);
        tex.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0);
        File.WriteAllBytes(System.Environment.GetEnvironmentVariable("DISASTER_SHOT"), tex.EncodeToPNG());
        RenderTexture.active = null;
    }

    [MenuItem("Disaster/Build Scene")]
    static void MenuBuild()
    {
        if (!File.Exists(TmpSettings)) { Begin(); return; }
        Build();
    }

    static void Begin()
    {
        if (File.Exists(ScenePath)) return;
        if (EditorApplication.isCompiling || EditorApplication.isUpdating)
        {
            EditorApplication.delayCall += Begin;
            return;
        }
        if (!File.Exists(TmpSettings))
        {
            if (SessionState.GetBool("DisasterImportingTmp", false)) return;
            SessionState.SetBool("DisasterImportingTmp", true);
            AssetDatabase.importPackageCompleted += OnTmpImported;
            AssetDatabase.ImportPackage(TmpPackage, false);
            return;
        }
        Build();
    }

    static void OnTmpImported(string packageName)
    {
        AssetDatabase.importPackageCompleted -= OnTmpImported;
        EditorApplication.delayCall += Begin;
    }

    static void Build()
    {
        Directory.CreateDirectory("Assets/Scenes");
        Directory.CreateDirectory("Assets/Prefabs");
        Directory.CreateDirectory("Assets/Materials");

        jpFont = MakeFont();
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var grey = Mat("Grey", new Color(0.55f, 0.55f, 0.55f));
        var dark = Mat("DarkGrey", new Color(0.35f, 0.35f, 0.38f));
        var blue = Mat("Blue", new Color(0.2f, 0.4f, 0.9f));
        var yellow = Mat("Yellow", new Color(0.95f, 0.85f, 0.15f));
        var red = Mat("Red", new Color(0.85f, 0.15f, 0.15f));
        var green = Mat("Green", new Color(0.2f, 0.7f, 0.25f));
        var brown = Mat("Brown", new Color(0.45f, 0.28f, 0.12f));
        var white = Mat("White", Color.white);

        // Camera and light
        var cam = new GameObject("Main Camera") { tag = "MainCamera" };
        var camera = cam.AddComponent<Camera>();
        cam.AddComponent<AudioListener>();
        cam.transform.position = new Vector3(0, 26, -26);
        cam.transform.rotation = Quaternion.Euler(45, 0, 0);
        camera.fieldOfView = 60;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.08f, 0.08f, 0.1f);
        camera.rect = new Rect(0, 0, 0.65f, 1);

        var lightGo = new GameObject("Directional Light");
        var dir = lightGo.AddComponent<Light>();
        dir.type = LightType.Directional;
        dir.shadows = LightShadows.Soft;
        lightGo.transform.rotation = Quaternion.Euler(50, -30, 0);

        // Ground
        Prim(PrimitiveType.Plane, "Ground", Vector3.zero, new Vector3(3, 1, 3), dark);

        // Player
        var player = Prim(PrimitiveType.Capsule, "Player", new Vector3(0, 1, -10), Vector3.one, blue);
        player.tag = "Player";
        var prb = player.AddComponent<Rigidbody>();
        prb.constraints = RigidbodyConstraints.FreezeRotation;
        var pc = player.AddComponent<PlayerController>();
        Set(pc, "speed", 0f);
        player.AddComponent<Health>();
        Label(player, 2.2f);

        // Managers
        var gm = Empty("GameManager", new Vector3(12, 0.5f, 12));
        gm.AddComponent<GameManager>();
        gm.AddComponent<SceneReloader>();
        Label(gm, 1f);

        var logger = Empty("Logger", new Vector3(-12, 0.5f, 9));
        logger.AddComponent<FrameLogger>();
        Label(logger, 1f);

        var spinner = Prim(PrimitiveType.Cube, "Spinner", new Vector3(12, 0.75f, -12), Vector3.one * 1.5f, yellow);
        spinner.AddComponent<Spinner>();
        Label(spinner, 1.6f);

        var board = Empty("ScoreBoard", new Vector3(-8, 0.5f, 12));
        board.AddComponent<ScoreBoard>();
        Label(board, 1f);

        var countdown = Empty("Countdown", new Vector3(-4, 0.5f, 9));
        countdown.AddComponent<Countdown>();
        Label(countdown, 1f);

        // Enemy and watcher
        var enemy = Prim(PrimitiveType.Cube, "Enemy", new Vector3(-5, 0.5f, -6), Vector3.one, red);
        enemy.AddComponent<Enemy>();
        Label(enemy, 1.2f);

        var watcher = Empty("Watcher", new Vector3(0, 0.5f, 12));
        var ew = watcher.AddComponent<EnemyWatcher>();
        Set(ew, "enemy", enemy);
        Label(watcher, 1f);

        // Box (no Rigidbody)
        var box = Prim(PrimitiveType.Cube, "Box", new Vector3(6, 0.5f, -5), new Vector3(1.5f, 1, 1.5f), brown);
        box.AddComponent<BoxPusher>();
        Label(box, 1.4f);

        // Coin prefab and instance
        var coinTemp = Prim(PrimitiveType.Cylinder, "Coin", Vector3.zero, new Vector3(0.8f, 0.1f, 0.8f), yellow);
        Object.DestroyImmediate(coinTemp.GetComponent<Collider>());
        var coinCol = coinTemp.AddComponent<BoxCollider>();
        coinCol.isTrigger = true;
        coinCol.size = new Vector3(1, 10, 1);
        coinTemp.AddComponent<CoinPickup>();
        var prefab = PrefabUtility.SaveAsPrefabAssetAndConnect(coinTemp, PrefabPath, InteractionMode.AutomatedAction);
        Object.DestroyImmediate(coinTemp);
        var coinInst = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        coinInst.transform.position = new Vector3(8, 0.6f, -9);
        LabelAt("Coin", coinInst.transform.position + Vector3.up * 1.3f);

        var spawner = Empty("CoinSpawner", new Vector3(8, 0.5f, 9));
        var cs = spawner.AddComponent<CoinSpawner>();
        Set(cs, "coinPrefab", prefab);
        Label(spawner, 1f);

        // Door and button
        var door = Prim(PrimitiveType.Cube, "Door", new Vector3(12, 2, 3), new Vector3(3, 4, 0.5f), yellow);
        Label(door, 2.6f);
        var button = Prim(PrimitiveType.Cube, "DoorButton", new Vector3(7, 0.15f, 3), new Vector3(1.5f, 0.3f, 1.5f), grey);
        button.GetComponent<BoxCollider>().isTrigger = true;
        button.AddComponent<DoorButton>();
        Label(button, 1.2f);

        // Goals
        var goal = Prim(PrimitiveType.Cube, "Goal", new Vector3(12, 0.5f, -7), new Vector3(2, 1, 2), green);
        goal.AddComponent<Goal>();
        Label(goal, 1.4f);

        // Switch and lamp
        var sw = Prim(PrimitiveType.Cube, "Switch", new Vector3(-8, 0.5f, 2), Vector3.one, grey);
        var ls = sw.AddComponent<LightSwitch>();
        Label(sw, 1.2f);
        var lampGo = Prim(PrimitiveType.Sphere, "Lamp", new Vector3(-4, 1, 2), Vector3.one, white);
        var lr = lampGo.AddComponent<LightReactor>();
        var pl = new GameObject("Point Light");
        pl.transform.SetParent(lampGo.transform, false);
        pl.transform.localPosition = new Vector3(0, 1, 0);
        var light = pl.AddComponent<Light>();
        light.type = LightType.Point;
        light.range = 10;
        light.intensity = 15;
        light.enabled = false;
        Set(lr, "lamp", light);
        Label(lampGo, 1.5f);

        BuildCanvas();

        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Disaster scene built.");
    }

    static void BuildCanvas()
    {
        var canvasGo = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        var canvas = canvasGo.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        var scaler = canvasGo.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        scaler.matchWidthOrHeight = 0.5f;

        var scoreText = UiText(canvasGo.transform, "ScoreText", "Score: 0", 44,
            new Vector2(0, 1), new Vector2(0, 1), new Vector2(30, -30), new Vector2(600, 60), Color.white);
        var scoreUi = scoreText.gameObject.AddComponent<ScoreUI>();
        Set(scoreUi, "label", scoreText);

        // Panel background on the right 35% of the screen (the camera draws in the left 65%)
        var bg = new GameObject("InstructionBackground", typeof(RectTransform), typeof(Image));
        bg.transform.SetParent(canvasGo.transform, false);
        var bgRect = bg.GetComponent<RectTransform>();
        bgRect.anchorMin = new Vector2(0.65f, 0);
        bgRect.anchorMax = new Vector2(1, 1);
        bgRect.offsetMin = bgRect.offsetMax = Vector2.zero;
        bg.GetComponent<Image>().color = new Color(0.05f, 0.05f, 0.08f, 1);

        const string text =
            "<b>Controls / 操作</b>\n\n" +
            "Arrow keys: move / 矢印キー: 移動\n" +
            "Space: jump / スペース: ジャンプ\n" +
            "E: use switch / E: スイッチを使う\n" +
            "2: spawn coin / 2: コインを出す\n" +
            "3: check enemy / 3: 敵を確認\n" +
            "4: total scores / 4: スコア合計\n" +
            "5: countdown / 5: カウントダウン\n" +
            "<color=#FF3030>Save before pressing 5 / 5 を押す前に保存</color>\n" +
            "6: take damage / 6: ダメージを受ける\n" +
            "7: push box / 7: 箱を押す\n" +
            "R: reload scene / R: シーンを再読み込み";
        var panel = UiText(bg.transform, "InstructionPanel", text, 30,
            Vector2.zero, Vector2.one, Vector2.zero, Vector2.zero, Color.white);
        var pr = panel.rectTransform;
        pr.offsetMin = new Vector2(30, 30);
        pr.offsetMax = new Vector2(-30, -30);
        panel.alignment = TextAlignmentOptions.TopLeft;
        panel.textWrappingMode = TextWrappingModes.Normal;
        panel.richText = true;
        jpFont.TryAddCharacters(text + "Score: Lives: 0123456789");
        EditorUtility.SetDirty(jpFont);
    }

    static TextMeshProUGUI UiText(Transform parent, string name, string text, float size,
        Vector2 anchorMin, Vector2 anchorMax, Vector2 pos, Vector2 sizeDelta, Color color)
    {
        var go = new GameObject(name, typeof(RectTransform), typeof(TextMeshProUGUI));
        go.transform.SetParent(parent, false);
        var rt = go.GetComponent<RectTransform>();
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.pivot = new Vector2(anchorMin.x, anchorMin.y);
        rt.anchoredPosition = pos;
        rt.sizeDelta = sizeDelta;
        var t = go.GetComponent<TextMeshProUGUI>();
        t.font = jpFont;
        t.text = text;
        t.fontSize = size;
        t.color = color;
        t.alignment = TextAlignmentOptions.TopLeft;
        return t;
    }

    static TMP_FontAsset MakeFont()
    {
        var existing = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>(FontAssetPath);
        if (existing != null) return existing;
        var font = AssetDatabase.LoadAssetAtPath<Font>(FontPath);
        var fa = TMP_FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024,
            AtlasPopulationMode.Dynamic, true);
        fa.name = "NotoSansJP SDF";
        AssetDatabase.CreateAsset(fa, FontAssetPath);
        fa.atlasTextures[0].name = "NotoSansJP Atlas";
        AssetDatabase.AddObjectToAsset(fa.atlasTextures[0], fa);
        fa.material.name = "NotoSansJP Material";
        AssetDatabase.AddObjectToAsset(fa.material, fa);
        AssetDatabase.SaveAssets();
        return fa;
    }

    static Material Mat(string name, Color color)
    {
        var path = "Assets/Materials/" + name + ".mat";
        var m = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (m != null) return m;
        m = new Material(Shader.Find("Universal Render Pipeline/Lit"));
        m.SetColor("_BaseColor", color);
        AssetDatabase.CreateAsset(m, path);
        return m;
    }

    static GameObject Prim(PrimitiveType type, string name, Vector3 pos, Vector3 scale, Material mat)
    {
        var go = GameObject.CreatePrimitive(type);
        go.name = name;
        go.transform.position = pos;
        go.transform.localScale = scale;
        go.GetComponent<Renderer>().sharedMaterial = mat;
        return go;
    }

    static GameObject Empty(string name, Vector3 pos)
    {
        var go = new GameObject(name);
        go.transform.position = pos;
        return go;
    }

    // World-space name tag, parented so it follows the object, facing the camera
    static void Label(GameObject target, float heightAbove)
    {
        var t = LabelAt(target.name, target.transform.position + Vector3.up * heightAbove);
        // Only unscaled, non-spinning objects can carry their tag without skewing it
        if (target.transform.lossyScale == Vector3.one && target.GetComponent<Spinner>() == null)
            t.SetParent(target.transform, true);
    }

    static Transform LabelAt(string text, Vector3 pos)
    {
        var go = new GameObject("Label_" + text);
        go.transform.position = pos;
        go.transform.rotation = Quaternion.Euler(45, 0, 0);
        var tmp = go.AddComponent<TextMeshPro>();
        tmp.text = text;
        tmp.fontSize = 6;
        tmp.fontStyle = FontStyles.Bold;
        tmp.alignment = TextAlignmentOptions.Center;
        tmp.textWrappingMode = TextWrappingModes.NoWrap;
        tmp.color = Color.white;
        tmp.rectTransform.sizeDelta = new Vector2(8, 1.5f);
        return go.transform;
    }

    static void Set(Object target, string field, float v) => Apply(target, field, p => p.floatValue = v);
    static void Set(Object target, string field, int v) => Apply(target, field, p => p.intValue = v);
    static void Set(Object target, string field, Object v) => Apply(target, field, p => p.objectReferenceValue = v);

    static void Apply(Object target, string field, System.Action<SerializedProperty> set)
    {
        var so = new SerializedObject(target);
        var p = so.FindProperty(field);
        set(p);
        so.ApplyModifiedProperties();
    }
}
