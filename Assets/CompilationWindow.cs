using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

public class CompilationWindow : EditorWindow
{
    [MenuItem("Window/" + nameof(CompilationWindow))]
    private static void ShowWindow()
    {
        GetWindow<CompilationWindow>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Request Script Compilation"))
        {
            CompilationPipeline.RequestScriptCompilation();
        }
    }
}