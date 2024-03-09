/*
 * Author: CharSui
 * Created On: 2024.02.13
 * Description: 为了让房主可以同步词库，Server端需要缓存词库
 * 也就是服务端根据词库大小，以及战局参数，缓存词库进行游戏
 * 这一段只负责加载，实际服务器缓存是在别的地方做。
 */

namespace Module.WordSystem
{
    public abstract class WordLoader
    {
        protected int count;

        public virtual int GetCount()
        {
            return count;
        }

        public abstract bool Init();

        public abstract bool Release();

        public abstract bool Load(string context);

        public abstract bool TryGetWord(int index, out WordData word);
    }
}