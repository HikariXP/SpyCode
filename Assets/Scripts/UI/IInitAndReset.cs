/*
 * Author: CharSui
 * Created On: 2024.03.29
 * Description: 给UI使用，提供初始化和基于战局重设UI的接口
 */

public interface IInitAndReset
{
    /// <summary>
    /// 需要自己写镜像卸载对于事件
    /// </summary>
    public void UI_Init();

    /// <summary>
    /// 战局结束后回到房间的操作
    /// </summary>
    public void UI_Reset();
}
