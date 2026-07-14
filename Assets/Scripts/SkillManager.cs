using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Header("スロット数")]
    [SerializeField] private int _maxPassiveSlots = 3;
    [SerializeField] private int _maxAttackSlots = 3;

    [Header("習得候補となる全スキル")]
    [SerializeField] private List<SkillData> _allSkillPool = new();

    [Header("参照")]
    [SerializeField] private PlayerStatus _status;
    [SerializeField] private PlayerBlinkController _blinkController;
    [SerializeField] private Transform _attackOrigin;
    [SerializeField] private ExpManager _expManager;

    [Header("現在の所持スキル（実行中にここで確認・編集可）")]
    [SerializeField] private List<SkillInstance> _passiveSkills = new();
    [SerializeField] private List<SkillInstance> _attackSkills = new();

    public event Action<List<SkillData>> OnLevelUpChoicesReady;

    private void OnEnable()
    {
        _blinkController.OnBlinkStart += HandleBlinkStart;
        _blinkController.OnBlinkEnd += HandleBlinkEnd;
        _expManager.OnLevelUp += HandleLevelUp;
    }

    private void OnDisable()
    {
        _blinkController.OnBlinkStart -= HandleBlinkStart;
        _blinkController.OnBlinkEnd -= HandleBlinkEnd;
        _expManager.OnLevelUp -= HandleLevelUp;
    }

    private void Update()
    {
        TickAlwaysActiveSkills();
    }

    // ===== レベルアップ時の選択肢生成 =====

    private void HandleLevelUp(int newLevel)
    {
        var choices = GenerateChoices(3);

        if (choices.Count == 0)
        {
            Debug.Log("習得可能なスキルがありません（全スロット埋まり済み）");
            return;
        }

        GamePauseManager.SetPaused(true); // 選択中はポーズ
        OnLevelUpChoicesReady?.Invoke(choices);
    }

    private List<SkillData> GenerateChoices(int count)
    {
        var eligible = _allSkillPool.Where(IsEligible).ToList();
        var result = new List<SkillData>();
        int safeCount = Mathf.Min(count, eligible.Count);

        for (int i = 0; i < safeCount; i++)
        {
            int index = UnityEngine.Random.Range(0, eligible.Count);
            result.Add(eligible[index]);
            eligible.RemoveAt(index);
        }

        return result;
    }

    private bool IsEligible(SkillData data)
    {
        var existing = FindInstance(data);
        if (existing != null) return existing.CanLevelUp;

        return data.category == SkillCategory.Passive
            ? _passiveSkills.Count < _maxPassiveSlots
            : _attackSkills.Count < _maxAttackSlots;
    }

    /// <summary>
    /// UI側のボタンから呼ばれる選択確定処理
    /// </summary>
    public void SelectSkill(SkillData data)
    {
        AcquireOrLevelUp(data);
        GamePauseManager.SetPaused(false);
    }

    // ===== スキル取得・レベルアップ本体 =====

    public void AcquireOrLevelUp(SkillData data)
    {
        var existing = FindInstance(data);
        if (existing != null)
        {
            existing.LevelUp();
            if (data.category == SkillCategory.Passive)
                ApplyPassiveModifier((PassiveStatSkillData)data, existing.level);
            return;
        }

        var instance = new SkillInstance(data);

        if (data.category == SkillCategory.Passive)
        {
            _passiveSkills.Add(instance);
            ApplyPassiveModifier((PassiveStatSkillData)data, instance.level);
        }
        else
        {
            _attackSkills.Add(instance);
        }
    }

    private SkillInstance FindInstance(SkillData data)
    {
        return _passiveSkills.Find(s => s.data == data)
            ?? _attackSkills.Find(s => s.data == data);
    }

    private void ApplyPassiveModifier(PassiveStatSkillData data, int level)
    {
        _status.RemoveModifiersFromSource(data);

        float value = data.GetValue(level);
        var mode = data.modifierMode == PassiveStatSkillData.ModifierMode.Additive
            ? StatModifierMode.Additive
            : StatModifierMode.Multiplicative;

        _status.AddModifier(new StatModifier
        {
            source = data,
            stat = data.targetStat,
            mode = mode,
            value = value
        });
    }

    private void TickAlwaysActiveSkills()
    {
        foreach (var skill in _attackSkills)
        {
            var attackData = (AttackSkillData)skill.data;
            if (attackData.triggerType != AttackTriggerType.AlwaysActive) continue;

            skill.cooldownTimer -= Time.deltaTime;
            if (skill.cooldownTimer <= 0f)
            {
                var levelData = attackData.GetLevelData(skill.level);
                skill.cooldownTimer = levelData.interval;
                FireAttack(attackData, levelData);
            }
        }
    }

    private void HandleBlinkStart() => FireTriggeredAttacks(AttackTriggerType.OnBlinkStart);
    private void HandleBlinkEnd() => FireTriggeredAttacks(AttackTriggerType.OnBlinkEnd);

    private void FireTriggeredAttacks(AttackTriggerType trigger)
    {
        foreach (var skill in _attackSkills)
        {
            var attackData = (AttackSkillData)skill.data;
            if (attackData.triggerType != trigger) continue;

            var levelData = attackData.GetLevelData(skill.level);
            FireAttack(attackData, levelData);
        }
    }

    private void FireAttack(AttackSkillData data, AttackSkillData.AttackLevelData levelData)
    {
        var obj = PoolManager.Instance.Get(data.attackPrefab, _attackOrigin.position, _attackOrigin.rotation);

        if (obj.TryGetComponent<DamageDealerBase>(out var dealer))
        {
            dealer.SetDamage(levelData.damage);
            dealer.SetOwnerFaction(Faction.Player);
        }

        if (obj.TryGetComponent<ProjectileMoveController>(out var mover))
        {
            Vector3 dir = _attackOrigin.forward; // 必要ならターゲット方向に変更
            mover.Launch(dir, _attackOrigin);
        }
    }

    // ===== Inspectorでレベルを直接書き換えたときに効果を再計算する（任意・便利機能） =====
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (!Application.isPlaying) return;

        foreach (var skill in _passiveSkills)
        {
            if (skill.data is PassiveStatSkillData passiveData)
                ApplyPassiveModifier(passiveData, skill.level);
        }
    }
#endif
}