/*
 * Author: CharSui
 * Created On: 2024.03.07
 * Description: 词库定义
 */

using System;
using System.Collections.Generic;


namespace Module.WordSystem
{
    public struct WordLibrary
    {
        /// <summary>
        /// 词库名
        /// </summary>
        public string name;
    
        /// <summary>
        /// 词库描述
        /// </summary>
        public string description;

        /// <summary>
        /// 语言缩写:cn、en、jp
        /// </summary>
        public string language;

        /// <summary>
        /// 词库版本
        /// </summary>
        public Version version;
        
        /// <summary>
        /// 词库内容
        /// </summary>
        public List<WordData> wordDatas;
    }
}
