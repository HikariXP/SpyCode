/*
 * Author: CharSui
 * Created On: 2024.03.03
 * Description:  一键查看目前Unity已经使用多少内存，即使提醒重启。
 */

using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;

namespace Editor.EditorTool
{
    public class MemoryChecker : MonoBehaviour
    {
        [MenuItem("CharSuiMDTool/MemoryCheck &v", false,13)]
        public static void CheckMemoryUsed()
        {
            long memorySize = Profiler.GetTotalReservedMemoryLong() / (1024 * 1024);
            Debug.Log($"<color=cyan>目前Unity已分配内存: {memorySize} MB</color>");

            if (memorySize > 4096) // 超过 4GB
            {
                Debug.LogError("<color=#FFFF00>Unity已经被分配超过 4GB 的内存!请注意重启Editor (Alt + R) 避免</color>");
            }
        }
    }
}