using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private List<Image> DashImage;
    [SerializeField] private Image _expImage;
    [SerializeField] private ExpManager _expManager;
    [SerializeField] private TMP_Text _levelText;

    private int _currentBlinkCharges;
    private float _ratio;

    public int CurrentBlinkCharges
    {
        get => _currentBlinkCharges;
        set
        {
            _currentBlinkCharges = Mathf.Clamp(value, 0, DashImage.Count);

            for (int i = 0; i < DashImage.Count; i++)
            {
                DashImage[i].gameObject.SetActive(i < _currentBlinkCharges);
            }
        }
    }

    private void OnEnable()
    {
        _expManager.OnExpChanged += ChangeExpUI;
        _expManager.OnLevelUp += ChangeLevelUI;
    }

    private void OnDisable()
    {
        _expManager.OnExpChanged -= ChangeExpUI;
    }

    private void ChangeExpUI(int currentExp, int recExp)
    {
        _ratio = (float)currentExp / recExp;
        _expImage.fillAmount = _ratio;
    }

    private void ChangeLevelUI(int currentLevel)
    {
        _levelText.text = "Lv" + currentLevel.ToString();
    }
}
