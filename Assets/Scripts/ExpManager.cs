using System;
using UnityEngine;

public class ExpManager : MonoBehaviour
{
    public static ExpManager Instance { get; private set; }

    public event Action<int> OnLevelUp;              // 新しいレベルを渡す
    public event Action<int, int> OnExpChanged;       // currentExp, 次のレベルまでの必要量

    [Header("レベルごとの必要経験値（index0 = Lv1→2）")]
    [SerializeField] private int[] _requiredExpPerLevel;

    [Header("現在の状態（実行中に確認可）")]
    [SerializeField] private int _currentLevel = 1;
    [SerializeField] private int _currentExp;

    public int CurrentLevel => _currentLevel;
    public int CurrentExp => _currentExp;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void AddExp(int amount)
    {
        _currentExp += amount;

        // 大量経験値で複数レベル一気に上がるケースに対応
        while (_currentLevel - 1 < _requiredExpPerLevel.Length &&
               _currentExp >= _requiredExpPerLevel[_currentLevel - 1])
        {
            _currentExp -= _requiredExpPerLevel[_currentLevel - 1];
            _currentLevel++;
            OnLevelUp?.Invoke(_currentLevel);
        }

        int required = _currentLevel - 1 < _requiredExpPerLevel.Length
            ? _requiredExpPerLevel[_currentLevel - 1]
            : _requiredExpPerLevel[_requiredExpPerLevel.Length - 1];

        OnExpChanged?.Invoke(_currentExp, required);
    }
}