[System.Serializable]
public class SkillInstance
{
    public SkillData data;
    public int level = 1;
    public float cooldownTimer;

    public SkillInstance(SkillData data) { this.data = data; }
    public bool CanLevelUp => level < data.MaxLevel;
    public void LevelUp() { if (CanLevelUp) level++; }
}