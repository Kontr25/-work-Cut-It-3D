using I2.Loc;
using UnityEngine;
using UnityEngine.UI;
using Lofelt.NiceVibrations;
using System.Collections.Generic;
using _Scripts.Actions;
using _Scripts.UI;
using DG.Tweening;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private GameObject _fpsGameObject;
    [SerializeField] private GameObject _closePanel;
    [SerializeField] private Transform _defaultButtonsPosition;
    [SerializeField] private float _openTime = 1f;
    [Header("Buttons")]
    [SerializeField] private Button _soundButton;
    [SerializeField] private Sprite _enabledSoundSprite;
    [SerializeField] private Sprite _disabledSoundSprite;
    [SerializeField] private Sprite _disabledFPSSprite;
    [SerializeField] private Sprite _enabledFPSSprite;

    [SerializeField] private Image[] _buttonImages;
    [SerializeField] private TMP_Text[] _buttonTexts;
    
    [SerializeField] private Button _ppButton;
    [SerializeField] private Button _fpsButton;
    [SerializeField] private Transform _levelBoards;
    [Space]
    [Header("Points")]
    [SerializeField] private List<Transform> _pointsForButtons;

    [SerializeField] private Hint _hint;

    private bool _fpsEnabled = false;
    private int _attempt;
    private bool _isOpen = false;
    private bool _volumeEnable = true;
    private bool _vibroEnable = true;
    private LanguageType _currentLanguage = LanguageType.English;

    public void Attempt()
    {
        _attempt++;
        if (_attempt == 2)
        {
            _hint.gameObject.SetActive(true);
        }
    }


    public void SwitchSettingsMenu()
    {
        if (_isOpen == false)
        {
            for (int i = 0; i < _buttonImages.Length; i++)
            {
                _buttonImages[i].DOFade(1f, _openTime - .4f);
            }

            for (int i = 0; i < _buttonTexts.Length; i++)
            {
                _buttonTexts[i].DOFade(1f, _openTime - .4f);
            }
            
            _soundButton.transform.DOMove(_pointsForButtons[0].transform.position, _openTime);
            _ppButton.transform.DOMove(_pointsForButtons[1].transform.position, _openTime);
            _fpsButton.transform.DOMove(_pointsForButtons[2].transform.position, _openTime);
            _levelBoards.transform.DOMove(_pointsForButtons[3].transform.position, _openTime);
            _closePanel.SetActive(true);
        }
        else
        {
            for (int i = 0; i < _buttonImages.Length; i++)
            {
                _buttonImages[i].DOFade(0f, _openTime - .4f);
            }

            for (int i = 0; i < _buttonTexts.Length; i++)
            {
                _buttonTexts[i].DOFade(0f, _openTime - .4f);
            }
            
            _soundButton.transform.DOMove(_defaultButtonsPosition.position, _openTime);
            _ppButton.transform.DOMove(_defaultButtonsPosition.position, _openTime);
            _fpsButton.transform.DOMove(_defaultButtonsPosition.position, _openTime);
            _levelBoards.transform.DOMoveY(_levelBoards.position.y - 1200, _openTime);
            _closePanel.SetActive(false);
        }

        _isOpen = !_isOpen;
    }


    public void SwitchSound()
    {
        _volumeEnable = !_volumeEnable;

        if (_volumeEnable == false)
        {
            AudioListener.volume = 0f;
            _soundButton.image.sprite = _disabledSoundSprite;
        }
        else
        {
            AudioListener.volume = 1f;
            _soundButton.image.sprite = _enabledSoundSprite;
        }
    }
    
    public void SwitchFPS()
    {
        _fpsEnabled = !_fpsEnabled;

        if (_fpsEnabled == false)
        {
            _fpsGameObject.SetActive(false);
            _fpsButton.image.sprite = _disabledFPSSprite;
        }
        else
        {
            _fpsGameObject.SetActive(true);
            _fpsButton.image.sprite = _enabledFPSSprite;
        }
    }

    public void SwitchVibro()
    {
        _vibroEnable = !_vibroEnable;

        if (_vibroEnable == false)
        {
            //_vibroButton.image.sprite = _disabledVibroSprite;
            HapticController.hapticsEnabled = false;
        }
        else
        {
            //_vibroButton.image.sprite = _enabledVibroSprite;
            HapticController.hapticsEnabled = true;
        }
    }

    public void SwitchLanguage()
    {
        string en = "English";
        string rus = "Russian";

        _currentLanguage++;

        int totalLangCount = System.Enum.GetNames(typeof(LanguageType)).Length;

        if ((int)_currentLanguage >= totalLangCount)
        {
            _currentLanguage = LanguageType.English;
        }

        switch (_currentLanguage)
        {
            case LanguageType.English:
                if (LocalizationManager.HasLanguage(en))
                    LocalizationManager.CurrentLanguage = en;

                //_langButton.image.sprite = _engLangSprite;
                break;
            case LanguageType.Russian:
                if (LocalizationManager.HasLanguage(rus))
                    LocalizationManager.CurrentLanguage = rus;

               // _langButton.image.sprite = _rusLangSprite;
                break;
            default:
                break;
        }

    }

    public void ShowPolicy()
    {
        Application.OpenURL("https://ink-quokka-842.notion.site/Privacy-Policy-e2837eafdbe04769b7f913ebef507164");
    }

    private enum LanguageType
    {
        English,
        Russian
    }
}