using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR
public class RebootUnityEditor
{
    [MenuItem("CharSuiMDTool/Reboot &r", false, 1)]
    static void ExcuteReopenProject()
    {
        EditorApplication.OpenProject(Application.dataPath.Replace("Assets", string.Empty));
    }
}
#endif