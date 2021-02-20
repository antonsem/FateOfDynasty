using UnityEngine;
#if UNITY_EDITOR
using System.Collections.Generic;
using GGJ21;
using UnityEditor;

#endif

public static class JsonFromPrefab
{
#if UNITY_EDITOR

    [MenuItem("FoD/Get texts")]
    public static void GetJson()
    {
        string saveFolder = EditorUtility.OpenFolderPanel("text Folder", Application.dataPath, "JSONS");

        Object[] selected = Selection.objects;
        float progress = 0;
        string report = "";
        string json = "";
        foreach (Object obj in selected)
        {
            EditorUtility.DisplayProgressBar("Creating text...", obj.name, 1);
            if (WriteRecursively(ref json, obj as ItemData))
                json += "\n\n";

            progress++;
            if (!EditorUtility.DisplayCancelableProgressBar("Creating text", $"Working on {obj.name}",
                progress / selected.Length)) continue;
            report += "Canceled generation of text";
            break;
        }

        string fullPath = $"{saveFolder}/texts.txt";
        System.IO.File.WriteAllText(fullPath, json);

        EditorUtility.ClearProgressBar();
        EditorUtility.DisplayDialog("Report", report, "Cool!");
    }

    private static bool WriteRecursively(ref string str, ItemData obj)
    {
        string name = obj.name;
        string cantUse = "";
        string use = "";
        string used = "";
        string description = "";
        bool hasText = false;
        if (!string.IsNullOrEmpty(obj.cantUse))
        {
            cantUse = $"\n***---***\n{obj.cantUse}";
            hasText = true;
        }

        if (!string.IsNullOrEmpty(obj.use))
        {
            use = $"\n***---***\n{obj.use}";
            hasText = true;
        }

        if (!string.IsNullOrEmpty(obj.used))
        {
            used = $"\n***---***\n{obj.used}";
            hasText = true;
        }

        if (!string.IsNullOrEmpty(obj.description))
        {
            description = $"\n***---***\n{obj.description}";
            hasText = true;
        }

        if (!hasText) return false;
        str += $"----------\n{name}{cantUse}{use}{used}{description}";
        return true;
    }

#endif
}