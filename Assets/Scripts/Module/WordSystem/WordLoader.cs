/*
 * Author: CharSui
 * Created On: 2024.02.13
 * Description: 
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

        public abstract string GetWord(int wordIndex);
    }
}