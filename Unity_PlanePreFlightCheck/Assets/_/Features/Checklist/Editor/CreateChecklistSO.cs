using System.Collections.Generic;
using System.IO;
using Checklist.Runtime;
using UnityEditor;
using UnityEngine;

namespace Checklist.Editor
{
    public class CreateChecklistSO
    {
        [MenuItem("Assets/Create/Mytools/Checklist")]
        public static void CreateOnSelection()
        {
            Object[] selectedObjects = Selection.objects;

            foreach (Object obj in selectedObjects)
            {
                if (obj is TextAsset textAsset)
                {
                    string path = AssetDatabase.GetAssetPath(textAsset);
                    if (path.EndsWith(".csv")) CreateSO(path);
                    else throw new InvalidDataException("TextAsset must have .CSV extension");
                }
                else throw new InvalidDataException("Only TextAsset files are supported");
            }
        }

        private static void CreateSO(string path)
        {
            ChecklistScriptableObject checklistScriptableObject = ScriptableObject.CreateInstance<ChecklistScriptableObject>();
            string assetName = Path.GetFileNameWithoutExtension(path);
            checklistScriptableObject.name = assetName;

            TextAsset textAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
            string[] checknames = textAsset.text.Split('\n');
            
            checklistScriptableObject.m_name = new List<string>();
            for(int i = 1; i < checknames.Length; i ++)
            {
                string[] name = checknames[i].Split(',');
                string cleanName = name[0].Trim('"');
                checklistScriptableObject.m_name.Add(cleanName);
            }

            string assetpath = "Assets/_/Database/ChecklistSO/" + assetName + ".asset";
            EnsureFoldersExist(assetpath);
            AssetDatabase.CreateAsset(checklistScriptableObject,assetpath);
            AssetDatabase.SaveAssets();
        }

        private static void EnsureFoldersExist(string assetPath)
        {

            string directory = Path.GetDirectoryName(assetPath)?.Replace("\\", "/");

            if (string.IsNullOrEmpty(directory))
                return;

            string[] folders = directory.Split('/');

            string currentPath = folders[0];

            for (int i = 1; i < folders.Length; i++)
            {
                string nextPath = $"{currentPath}/{folders[i]}";

                if (!AssetDatabase.IsValidFolder(nextPath))
                {
                    AssetDatabase.CreateFolder(currentPath, folders[i]);
                }

                currentPath = nextPath;
            }
        }
    }
}
