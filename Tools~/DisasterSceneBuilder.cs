using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

// Builds Assets/Scenes/Disaster.unity. Menu: Disaster > Build Scene.
// Command line: Unity -batchmode -quit -executeMethod DisasterSceneBuilder.BatchBuild
public static class DisasterSceneBuilder
{
    const string ScenePath = "Assets/Scenes/Disaster.unity";

    [MenuItem("Disaster/Build Scene")]
    public static void Build()
    {
        Directory.CreateDirectory("Assets/Scenes");
        Directory.CreateDirectory("Assets/Materials");
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        var ground = Mat("DarkGrey", new Color(0.35f, 0.35f, 0.38f));
        var blue = Mat("Blue", new Color(0.2f, 0.4f, 0.9f));
        var yellow = Mat("Yellow", new Color(0.95f, 0.85f, 0.15f));
        var grey = Mat("Grey", new Color(0.55f, 0.55f, 0.55f));
        var green = Mat("Green", new Color(0.2f, 0.7f, 0.25f));
        var white = Mat("White", Color.white);

        var cam = new GameObject("Main Camera") { tag = "MainCamera" };
        var camera = cam.AddComponent<Camera>();
        cam.AddComponent<AudioListener>();
        cam.transform.position = new Vector3(0, 17, -17);
        cam.transform.rotation = Quaternion.Euler(45, 0, 0);
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.08f, 0.08f, 0.1f);

        var sun = new GameObject("Directional Light").AddComponent<Light>();
        sun.type = LightType.Directional;
        sun.shadows = LightShadows.Soft;
        sun.transform.rotation = Quaternion.Euler(50, -30, 0);

        Prim(PrimitiveType.Plane, "Ground", Vector3.zero, new Vector3(2, 1, 2), ground);

        var player = Prim(PrimitiveType.Capsule, "Player", new Vector3(0, 1, -6), Vector3.one, blue);
        player.tag = "Player";
        player.AddComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        Set(player.AddComponent<PlayerController>(), "speed", 0.01f);

        new GameObject("GameManager").AddComponent<GameManager>();
        new GameObject("Logger").AddComponent<FrameLogger>();
        new GameObject("Countdown").AddComponent<Countdown>();

        Prim(PrimitiveType.Cube, "Spinner", new Vector3(-6, 0.75f, 4), Vector3.one * 1.5f, yellow)
            .AddComponent<Spinner>();

        Prim(PrimitiveType.Cube, "Goal", new Vector3(6, 0.5f, 3), new Vector3(2, 1, 2), green)
            .AddComponent<Goal>();

        Prim(PrimitiveType.Cube, "Switch", new Vector3(-6, 0.5f, -3), Vector3.one, grey)
            .AddComponent<LightSwitch>();

        var lamp = Prim(PrimitiveType.Sphere, "Lamp", new Vector3(-2, 1, -3), Vector3.one, white);
        var reactor = lamp.AddComponent<LightReactor>();
        var pointLight = new GameObject("Point Light");
        pointLight.transform.SetParent(lamp.transform, false);
        pointLight.transform.localPosition = new Vector3(0, 1, 0);
        var light = pointLight.AddComponent<Light>();
        light.type = LightType.Point;
        light.range = 10;
        light.intensity = 15;
        light.enabled = false;
        Set(reactor, "lamp", light);

        EditorSceneManager.SaveScene(scene, ScenePath);
        EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene(ScenePath, true) };
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("Disaster scene built.");
    }

    public static void BatchBuild() => Build();

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

    static void Set(Object target, string field, float v)
    {
        var so = new SerializedObject(target);
        so.FindProperty(field).floatValue = v;
        so.ApplyModifiedProperties();
    }

    static void Set(Object target, string field, Object v)
    {
        var so = new SerializedObject(target);
        so.FindProperty(field).objectReferenceValue = v;
        so.ApplyModifiedProperties();
    }
}
