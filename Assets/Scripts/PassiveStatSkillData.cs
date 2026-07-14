using UnityEngine;

[CreateAssetMenu(menuName = "Skills/PassiveStatSkill")]
public class PassiveStatSkillData : SkillData
{
    public enum ModifierMode { Additive, Multiplicative }

    [Header("効果設定")]
    public StatType targetStat;
    public ModifierMode modifierMode;

    [Tooltip("レベル1から順に、そのレベルまでの累計値を入れる")]
    public float[] valuePerLevel; // 配列の要素数 = MaxLevel

    public float GetValue(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, valuePerLevel.Length - 1);
        return valuePerLevel[index];
    }
}