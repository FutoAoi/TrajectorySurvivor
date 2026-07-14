public enum StatModifierMode { Additive, Multiplicative }

public class StatModifier
{
    public object source; // どのスキルが付与したかを識別するためのキー
    public StatType stat;
    public StatModifierMode mode;
    public float value;
}