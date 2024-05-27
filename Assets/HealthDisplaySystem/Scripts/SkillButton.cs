using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class SkillButton : MonoBehaviour
{
    [SerializeField] private Sprite _normalStateSprite;
    [SerializeField] private VampireSkill _skill;

    private Button _button;
    private Image _buttonImage;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _buttonImage = GetComponent<Image>();
        _skill.SkillStopped += ChangeCondition;
    }

    private void OnDisable()
    {
        _skill.SkillStopped -= ChangeCondition;
    }

    public void ChangeCondition()
    {
        _button.interactable = true;
        _buttonImage.sprite = _normalStateSprite;
    }
}
