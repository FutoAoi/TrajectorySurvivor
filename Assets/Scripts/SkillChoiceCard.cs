using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillChoiceCard : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _Explanation;

    private SkillData _data;
    private Action<SkillData> _onSelected;

    public void Setup(SkillData data, Action<SkillData> onSelected)
    {
        _data = data;
        _onSelected = onSelected;

        _icon.sprite = data.icon;
        _nameText.text = data.displayName;
        _Explanation.text = data.skillExplanation;

        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener(() => _onSelected?.Invoke(_data));
    }
}