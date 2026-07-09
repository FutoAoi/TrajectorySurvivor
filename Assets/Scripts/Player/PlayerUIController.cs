using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private List<Image> DashImage;

    private int _currentBlinkCharges;

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
}
