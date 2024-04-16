using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EventDefine
{
    public const uint SERVER_START_DISCOVER = 0;
    
    public const uint SERVER_STOP_DISCOVER = 1;
    
    // Word 相关1000开始
    
    
    // Battle相关2000开始
    public const uint BATTLE_PLAYER_CONFIRM_WORDLIST = 2000;

    public const uint BATTLE_TURN_END = 2001;

    // 传递方另一个人等到对方提交了才可以提交
    public const uint BATTLE_SENDER_TEAM_MASK = 2002;
    
    public const uint BATTLE_SENDER_SPEAK = 2003;

    public const uint BATTLE_GAME_END = 2004;
}
