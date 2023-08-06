using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class ButtonSceneSwitch : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] GameSceneManager.Scene _switchScene;

    [Space(10)]
    [SerializeField] private Color _colorSellectedButton;
    [SerializeField] private Color _colorSellectedTextButton;
    [SerializeField] private Vector3 _scaleTextSellectedButton;

    [SerializeField] private Ease easeScaleText;
    [SerializeField] private Ease easeChangeColor;

    private Image _backgroundButton;
    private TextMeshProUGUI _textButton;
    private Color _defaultColorButton;
    private Color _defaultColorTextButton;
    private Vector3 _defaultScaleTextButton;

    private GameSceneManager _gameSceneManager;
    private AudioManager _audioManager;

    [Inject]
    private void Construct(GameSceneManager gameSceneManager, AudioManager audioManager)
    {
        _gameSceneManager = gameSceneManager;
        _audioManager = audioManager;
    }

    private void Start()
    {
        _backgroundButton = GetComponent<Image>();
        _textButton = GetComponentInChildren<TextMeshProUGUI>();

        _defaultColorButton = _backgroundButton.color;
        _defaultScaleTextButton = _textButton.transform.localScale;
        _defaultColorTextButton = _textButton.color;
    }

    private void Select()
    {
        _audioManager?.PlaySound(AudioManager.SoundType.ButtonSelected);

        _backgroundButton.DOColor(_colorSellectedButton, 0.15f).SetEase(easeChangeColor);
        _textButton.transform.DOScale(_scaleTextSellectedButton, 0.15f).SetEase(easeScaleText);
        _textButton.DOColor(_colorSellectedTextButton, 0.15f).SetEase(easeChangeColor);
    }

    private void Deselect()
    {
        _backgroundButton.DOColor(_defaultColorButton, 0.15f).SetEase(easeChangeColor);
        _textButton.transform.DOScale(_defaultScaleTextButton, 0.15f).SetEase(easeScaleText);
        _textButton.DOColor(_defaultColorTextButton, 0.15f).SetEase(easeChangeColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Deselect();

        _audioManager?.PlaySound(AudioManager.SoundType.ButtonClick);

        _gameSceneManager?.SwitchToScene(_switchScene);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select();
    }

    public void OnPointerExit(PointerEventData eventData)
    {   
        Deselect();
    }

}
