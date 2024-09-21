using UnityEngine;
using UnityEditor;

public class FolderCreator : EditorWindow
{
    private string m_FolderName = "";
    private bool m_NeedJson = false;
    private bool m_NeedModels = false;

    [MenuItem("Flig/CreateFolder")]
    public static void CreateFolder()
    {
        var window = GetWindow<FolderCreator>("FolderCreator");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Folder name: ", EditorStyles.boldLabel);

        m_FolderName = EditorGUILayout.TextField(m_FolderName);
        m_NeedJson = GUILayout.Toggle(m_NeedJson, "You need json files?");
        m_NeedModels = GUILayout.Toggle(m_NeedModels, "You need 3d model files?");

        if (GUILayout.Button("Create core folder!"))
        {
            CreateNewFolder(m_FolderName);
            SetDefault();
        }

    }

    private void SetDefault()
    { 
        m_FolderName = "";
        m_NeedJson = false;
    }

    private void CreateNewFolder(string folderName)
    {
        string basePath = "Assets/Runner3D/Core";
        string path = basePath + "/" + folderName;

        if (AssetDatabase.IsValidFolder(path))
        {
            Debug.LogError($"Folder {folderName} exists!");
            return;
        }

        AssetDatabase.CreateFolder(basePath, folderName);

        AssetDatabase.CreateFolder(path, "Content");
        AssetDatabase.CreateFolder(path, "Scripts");

        AssetDatabase.CreateFolder(path + "/Content", "Prefabs");
        AssetDatabase.CreateFolder(path + "/Content", "Sprites");

        if (m_NeedJson)
        { 
            AssetDatabase.CreateFolder(path + "/Content", "Json");
        }

        if (m_NeedModels)
        {
            AssetDatabase.CreateFolder(path + "/Content", "Models");
        }


        AssetDatabase.CreateFolder(path + "/Scripts", "Runtime");
        AssetDatabase.CreateFolder(path + "/Scripts", "External");
        AssetDatabase.Refresh();
        Debug.Log($"{folderName} was create");
    }
}
