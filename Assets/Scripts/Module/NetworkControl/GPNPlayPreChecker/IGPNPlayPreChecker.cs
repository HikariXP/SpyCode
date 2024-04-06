/*
 * Author: CharSui
 * Created On: 2024.04.04
 * Description: 游戏前状态检查器，比如人够不够，队伍齐不齐
 */

namespace Module.NetworkControl.GPNPlayPreChecker
{
    public interface IGPNPlayPreChecker
    {
        public bool CheckIsReady();
    }
}
