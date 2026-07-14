using UnityEngine;

[CreateAssetMenu(menuName = "Skills/AttackSkill")]
public class AttackSkillData : SkillData
{
    [Header("発動条件")]
    public AttackTriggerType triggerType;

    [Header("使用するダメージ判定プレハブ")]
    public GameObject attackPrefab; // SingleHit/Piercing/Aoe等のDamageDealerBase派生を想定

    [System.Serializable]
    public class AttackLevelData
    {
        public int damage;
        public float interval = 1f; // AlwaysActiveのみ使用
    }

    public AttackLevelData[] levels; // 要素数 = MaxLevel

    public AttackLevelData GetLevelData(int level)
    {
        int index = Mathf.Clamp(level - 1, 0, levels.Length - 1);
        return levels[index];
    }
}