/*
 * Author: CharSui
 * Created On: 2024.03.03
 * Description: 快速场景跳转快捷键
 */

using UnityEditor;
using UnityEditor.SceneManagement;

#if true


namespace Editor.EditorTool
{
    public static class SceneNav
    {
        [MenuItem("CharSuiMDTool/ForceSceneJump/GamePlayerOffline &1", false, 101)]
        static void SceneJump_Offline()
        {
            EditorSceneManager.OpenScene("Assets/ProjectAsset/Scene/GamePlayerOffline.unity");
        }
    
        [MenuItem("CharSuiMDTool/ForceSceneJump/GamePlayerOnline &2", false, 101)]
        static void SceneJump_Online()
        {
            EditorSceneManager.OpenScene("Assets/ProjectAsset/Scene/GamePlayerOnline.unity");
        }
    }
}
#endif