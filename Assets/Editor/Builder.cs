#if UNITY_IOS || UNITY_TVOS
#define UNITY_XCODE_EXTENSIONS_AVAILABLE
#endif
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

using UnityEngine.Rendering;
using System.Linq;
using UnityEngine.U2D;
using UnityEditor.U2D;

using Cysharp.Threading.Tasks;
using IngameDebugConsole;
using Unity.Linq;
using UnityEditor.SceneManagement;
using System.Xml;

using Sirenix.Utilities;
using UnityEditor.AddressableAssets.Build;
#if UNITY_XCODE_EXTENSIONS_AVAILABLE
using UnityEditor.iOS.Xcode;
#endif

[InitializeOnLoad]
public static class Builder
{
    public static BuildTargetGroup GetActiveBuildTargetGroup(BuildTarget buildTarget)
    {
        return buildTarget switch
        {
            BuildTarget.Android => BuildTargetGroup.Android,
            BuildTarget.iOS => BuildTargetGroup.iOS,
            _ => BuildTargetGroup.Standalone
        };
    }

    //public static void SET_Dev()
    //{
    //    EditorBuildSettingsScene[] ebssArray = new EditorBuildSettingsScene[EditorBuildSettings.scenes.Length];

    //    for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
    //        ebssArray[i] = EditorBuildSettings.scenes[i];

    //    foreach (EditorBuildSettingsScene scene in ebssArray)
    //    {
    //        Debug.Log(scene.path);
    //        if (scene.path == "Assets/Scenes/ResourceDownloadScene.unity" ||
    //            scene.path == "Assets/Scenes/StartScene.unity" ||
    //            scene.path == "Assets/Scenes/ResourceReleaseScene.unity")
    //            scene.enabled = true;
    //        else
    //            scene.enabled = false;
    //    }

    //    EditorBuildSettings.scenes = ebssArray;

    //    {
    //        var selectedBuildTargetGroup = GetActiveBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
    //        var defineSymbol =
    //            PlayerSettings.GetScriptingDefineSymbolsForGroup(selectedBuildTargetGroup);
    //        defineSymbol = defineSymbol.Replace(";LOG_ENABLE", "");
    //        defineSymbol = defineSymbol.Replace(";MULTI_CONNECT", "");
    //        defineSymbol = defineSymbol.Replace(";CHEAT", "");
    //        defineSymbol = defineSymbol.Replace(";TEST_DOWNLOAD", "");
    //        defineSymbol = defineSymbol.Replace(";DEDICATED_SERVER", "");
    //        defineSymbol = defineSymbol + ";LOG_ENABLE";
    //        defineSymbol = defineSymbol + ";TEST_DOWNLOAD";
    //        PlayerSettings.SetScriptingDefineSymbolsForGroup(selectedBuildTargetGroup,
    //            defineSymbol);
    //    }
    //}
    public static void Build()
    {
        var selectedBuildTargetGroup = GetActiveBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
        var defineSymbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(selectedBuildTargetGroup);

        defineSymbol = defineSymbol.Replace(";LOG_ENABLE", "");

        string arg = GetArg("-DEV_BUILD");
        bool isDev = "true" == arg;

        //버전 설정
        string version = GetArg("-BUNDLEVERSION");
        PlayerSettings.bundleVersion = version;

        //버전 코드 설정
        string bundle = GetArg("-VERSIONCODE");
        var intVersion = int.Parse(bundle);
        PlayerSettings.Android.bundleVersionCode = intVersion;

        arg = GetArg("-APPBUNDLE");
        bool isAppBundle = "true" == arg;

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            EditorUserBuildSettings.buildAppBundle = isAppBundle;
        }

        if (isDev)
        {
           //개발용 빌드
        }
        else
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.iOS)
            {
                if ("true" == GetArg("-CONSOLELOG"))
                {
                    defineSymbol += ";LOG_ENABLE";
                }  
            }

            AssetDatabase.Refresh();
            PlayerSettings.SetScriptingDefineSymbolsForGroup(selectedBuildTargetGroup, defineSymbol);
        }

        string target_dir = $"Builds/AOS_{version}.apk";

        BuildOptions buildOption = BuildOptions.CompressWithLz4HC;
        RunBuild(target_dir, isDev, buildOption);
    }

    private static void RunBuild(string path, bool dev, BuildOptions buildOption)
    {
        Console.Out.WriteLine("[LOG]Build start: " + path);

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
        {
            PlayerSettings.keystorePass = "gkstmd@12";
            PlayerSettings.keyaliasPass = "gkstmd@12";
        }


        if (dev)
        {
            buildOption = BuildOptions.CompressWithLz4HC | BuildOptions.ConnectWithProfiler | BuildOptions.Development | BuildOptions.AllowDebugging;
            EditorUserBuildSettings.connectProfiler = true;
            EditorUserBuildSettings.development = true;
        }

        if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS)
        {
            buildOption = buildOption | BuildOptions.AcceptExternalModificationsToPlayer;
            GenericBuild(FindEnabledEditorScenes(), "build_ios", BuildTargetGroup.iOS, BuildTarget.iOS, buildOption);
        }
        else
        {
            var selectedBuildTargetGroup = GetActiveBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget);
            GenericBuild(FindEnabledEditorScenes(), path, selectedBuildTargetGroup,
                EditorUserBuildSettings.activeBuildTarget, buildOption);
        }
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled)
                continue;
            EditorScenes.Add(scene.path);
        }

        return EditorScenes.ToArray();
    }

    static void GenericBuild(string[] scenes, string target_dir, BuildTargetGroup build_group, BuildTarget build_target,
        BuildOptions build_options)
    {
        foreach (var mit in scenes)
        {
            Console.Out.WriteLine("[LOG]Build scene: " + mit);
        }

        EditorUserBuildSettings.SwitchActiveBuildTarget(build_group, build_target);


        var res = BuildPipeline.BuildPlayer(scenes, target_dir, build_target, build_options);
        if (res.summary.result == BuildResult.Succeeded)
        {
#if UNITY_IOS
            EditorUtility.DisplayDialog("buildplayer complete", "완료", "확인");
#else
            Console.Out.WriteLine("[LOG]Build succeeded: " + res.summary.totalSize.ToString() + " bytes");
            if (Application.isBatchMode)
            {
                EditorApplication.Exit(0);
            }

#endif
        }

        if (res.summary.result == BuildResult.Failed)
        {
#if UNITY_IOS
            EditorUtility.DisplayDialog("buildplayer fail", "실패", "확인");
#else
            Console.Out.WriteLine("[LOG]Build failed");
            if (Application.isBatchMode)
            {
                EditorApplication.Exit(1);
            }
#endif
        }
    }

    public static string GetArg(string name)
    {
        string[] arguments = System.Environment.GetCommandLineArgs();

        for (int nIndex = 0; nIndex < arguments.Length; ++nIndex)
        {
            if (arguments[nIndex] == name && arguments.Length > nIndex + 1)
            {
                return arguments[nIndex + 1];
            }
        }

        return null;
    }
    public static void BuildSetting()
    {
        var market = GetArg("-PLATFORMTYPE");
        Console.Out.WriteLine($"[LOG]:마켓플렛폼이 {market} 입니다.");

        switch (market)
        {
       
            case "ANDROID":
                {
                    Console.Out.WriteLine($"[LOG]: Set with Android platform option..");
                    if (PlayerSettings.Android.targetArchitectures != AndroidArchitecture.ARM64)
                        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
                    if (EditorUserBuildSettings.androidBuildSubtarget != MobileTextureSubtarget.ASTC)
                        EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
                    if (PlayerSettings.openGLRequireES31 != true)
                        PlayerSettings.openGLRequireES31 = true;
                    if (PlayerSettings.openGLRequireES31AEP != true)
                        PlayerSettings.openGLRequireES31AEP = true;
                    if (PlayerSettings.openGLRequireES32 != true)
                        PlayerSettings.openGLRequireES32 = true;
                    if (PlayerSettings.Android.minSdkVersion != AndroidSdkVersions.AndroidApiLevel25)
                        PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel25;
                    /*                if (PlayerSettings.Android.targetSdkVersion != AndroidSdkVersions.AndroidApiLevel30)
                                        PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel30;*/
                    //PlayerSettings.productName = "tog";
                    PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.wookgames.evolutionWar");

                    //PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
                    //EditorUserBuildSettings.androidBuildSubtarget = MobileTextureSubtarget.ASTC;
                    //PlayerSettings.openGLRequireES31 = true;
                    //PlayerSettings.openGLRequireES31AEP = true;
                    //PlayerSettings.openGLRequireES32 = true;
                    //outputBuilderName = androidBuilder;
                    //readPath += androidManifest;
                    //buildOptions = BuildOptions.CompressWithLz4HC;

                //    string appManifestPath = Path.Combine(Application.dataPath, androidManifestPath);
                    XmlDocument manifestFile = new XmlDocument();
                  //  manifestFile.Load(appManifestPath);
                    bool isSave = false;
                    foreach (var mit in manifestFile.DocumentElement.Attributes)
                    {
                        if (mit is XmlAttribute attr)
                        {
                            if (attr.Name == "package")
                            {
                                attr.Value = "com.wookgames.evolutionWar";
                                isSave = true;
                                break;
                            }
                        }
                    }

                    bool isEnd = false;

                    //안드로이드 파이어베이스 AAR 정리
                    //isEnd = PlayServicesResolver.ResolveSync(false);
                    //Debug.Log($"[LOG] isEnd = {isEnd}");
                    //await UniTask.WaitWhile(() => isEnd);

                    if (isEnd)
                    {
                        //if (isSave)
                        //    manifestFile.Save(appManifestPath);
                    }
                    else
                    {
                        Console.Out.WriteLine($"[LOG]: StopBuild (PlayStore)");
                        EditorApplication.Exit(1);
                    }
                }
                break;
            case "IOS":
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, "com.wookgames.evolutionWar");
                EditorApplication.Exit(0);
                break;
            default:
                PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, "com.wookgames.evolutionWar");
                EditorApplication.Exit(0);
                break;
        }



    }
}