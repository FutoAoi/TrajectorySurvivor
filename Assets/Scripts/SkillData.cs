using UnityEngine;

public abstract class SkillData : ScriptableObject
{
    [Header("Šî–{î•ñ")]
    public string skillId;
    public string displayName;
    public string skillExplanation;
    public Sprite icon;
    public SkillCategory category;

    [Header("ƒŒƒxƒ‹Ý’è")]
    [SerializeField] private int _maxLevel = 5;
    public int MaxLevel => _maxLevel;
}