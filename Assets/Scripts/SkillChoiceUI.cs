using System.Collections.Generic;
using UnityEngine;

public class SkillChoiceUI : MonoBehaviour
{
    [SerializeField] private SkillManager _skillManager;
    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private SkillChoiceCard[] _cards; // 3–‡•ª‚ðInspector‚ÅŠ„‚è“–‚Ä

    private void Awake() => _panelRoot.SetActive(false);

    private void OnEnable() => _skillManager.OnLevelUpChoicesReady += ShowChoices;
    private void OnDisable() => _skillManager.OnLevelUpChoicesReady -= ShowChoices;

    private void ShowChoices(List<SkillData> choices)
    {
        _panelRoot.SetActive(true);

        for (int i = 0; i < _cards.Length; i++)
        {
            bool hasChoice = i < choices.Count;
            _cards[i].gameObject.SetActive(hasChoice);
            if (hasChoice) _cards[i].Setup(choices[i], OnCardSelected);
        }
    }

    private void OnCardSelected(SkillData data)
    {
        _panelRoot.SetActive(false);
        _skillManager.SelectSkill(data);
    }
}