using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _imgFiller;
    
    public void SetValue(float valueNormalize)
    {
        _imgFiller.fillAmount = valueNormalize;
    }
}
