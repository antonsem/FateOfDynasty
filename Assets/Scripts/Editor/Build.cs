using System.Diagnostics;
using System.IO;
using ExtraTools;
using UnityEditor;
using UnityEngine;
//https://forum.unity.com/threads/c-compression-zip-missing.577492/
using System.IO.Compression;

namespace GGJ21
{
    public static class Build
    {
        private const string itchAccountName = "antonsem";
        private const string itchGameName = "fate-of-dynasty";
        private const string winChannel = "win";
        private const string linuxChannel = "linux";
        private const string macChannel = "osx";

        private const string winBuild = "FateOfDynasty";
        private const string winDirectory = "Win";
        private const string linuxBuild = "FateOfDynasty";
        private const string linuxDirectory = "Linux";
        private const string macBuild = "FateOfDynasty";
        private const string macDirectory = "Mac";

        [MenuItem("Build/Build All")]
        public static void BuildAll()
        {
            string deployPath =
                EditorUtility.OpenFolderPanel("Select relevant folder", $"{Application.dataPath}/Build", "Builds");
            if (!deployPath.IsValid()) return;

            string[] scenes =
            {
                "Assets/Scenes/3DMenu.unity",
                "Assets/Scenes/Main.unity",
            };

            WindowsDeployAndPush(scenes, $"{deployPath}/{winDirectory}");
            LinuxDeployAndPush(scenes, $"{deployPath}/{linuxDirectory}");
            MacDeployAndPush(scenes, $"{deployPath}/{macDirectory}");
        }

        private static void WindowsDeployAndPush(in string[] scenes, in string deployPath)
        {
            Deploy(scenes, deployPath, $"{winBuild}.exe", BuildTargetGroup.Standalone,
                BuildTarget.StandaloneWindows64);
            
            Zip(deployPath);
            
            PushToItch($"{deployPath}.zip", itchAccountName, itchGameName, winChannel);
        }
        
        private static void LinuxDeployAndPush(in string[] scenes, in string deployPath)
        {
            Deploy(scenes, deployPath, $"{linuxBuild}.x86_64", BuildTargetGroup.Standalone,
                BuildTarget.StandaloneLinux64);
            
            Zip(deployPath);
            
            PushToItch($"{deployPath}.zip", itchAccountName, itchGameName, linuxChannel);
        }
        
        private static void MacDeployAndPush(in string[] scenes, in string deployPath)
        {
            Deploy(scenes, deployPath, $"{macBuild}.app", BuildTargetGroup.Standalone,
                BuildTarget.StandaloneOSX);
            
            Zip(deployPath);
            
            PushToItch($"{deployPath}.zip", itchAccountName, itchGameName, macChannel);
        }
        
        private static void Deploy(in string[] scenes, in string directory, in string buildName,
            BuildTargetGroup buildTargetGroup, BuildTarget buildTarget)
        {
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
            Directory.CreateDirectory(directory);

            EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup, buildTarget);
            BuildPipeline.BuildPlayer(scenes, $"{directory}/{buildName}", buildTarget, BuildOptions.None);
        }

        private static void Zip(in string path)
        {
            if(File.Exists($"{path}.zip"))
                File.Delete($"{path}.zip");
            
            ZipFile.CreateFromDirectory(path, $"{path}.zip");
        }

        private static void PushToItch(in string zipFile, in string accountName, in string gameName, in string channel)
        {
            string strCmdText = $"/C butler push {zipFile} {accountName}/{gameName}:{channel}";
            Process process = Process.Start("CMD.exe",strCmdText);
            while (true)
            {
                process.Refresh();
                if (EditorUtility.DisplayCancelableProgressBar("Pushing to itch...",
                    $"Pushing {zipFile} to {accountName}/{gameName}:{channel}", 0))
                {
                    process.Kill();
                    break;
                }
                if (process.HasExited) break;
            }
            EditorUtility.ClearProgressBar();
        }
    }
}