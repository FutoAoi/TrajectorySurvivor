public enum Faction
{
    Player,
    Enemy,
    Neutral,
    PlayerSummon
}

public enum SkillCategory
{
    Passive,  // 能力上昇系
    Attack    // 攻撃系
}

public enum AttackTriggerType
{
    AlwaysActive,   // 常時発動（インターバル）
    OnBlinkStart,   // ブリンク発動時
    OnBlinkEnd      // ブリンク終了時
}

public enum StatType
{
    MaxHealth,
    MoveSpeed,
    Defense,
    Damage
    // 必要に応じて追加
}