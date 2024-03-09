/*
 * Author: CharSui
 * Created On: 2024.03.03
 * Description: 项目重启
 */
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace Editor.EditorTool
{
    public class RebootUnityEditor
    {
        [MenuItem("CharSuiMDTool/Reboot &r", false, 1)]
        static void ExcuteReopenProject()
        {
            EditorApplication.OpenProject(Application.dataPath.Replace("Assets", string.Empty));
        }
    }
}
#endif