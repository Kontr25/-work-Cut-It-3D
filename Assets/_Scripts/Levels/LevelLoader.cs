using BayatGames.SaveGameFree;
using System.Collections.Generic;
using _Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private List<Level> _levels;
    [SerializeField] private int _levelForLoad;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private LevelCounter _levelCounter;
    [SerializeField] private bool _clearMode = false;

    private int _currentLevel;
    private int _totalLevel;

    public int CurrentLevel => _currentLevel;

    public int LevelForLoad
    {
        get => _levelForLoad;
        set => _levelForLoad = value;
    }

    public List<Level> Levels
    {
        get => _levels;
        set => _levels = value;
    }

    public int TotalLevel => _totalLevel;

    private void Start()
    {

//#if UNITY_EDITOR
        if (_levelForLoad != 0)
        {
            SetLevel();
        }

        if (_clearMode)
        {
            SaveGame.Clear();
        }
//#endif
        //if (PlayerPrefs.HasKey("CurrentLevel") == false)
        //{
        //    _currentLevel = 0;
        //    PlayerPrefs.SetInt("CurrentLevel", _currentLevel);
        //    PlayerPrefs.SetInt("TotalLevel", _currentLevel + 1);
        //}
        //else
        //{
        //PlayerPrefs.GetInt("CurrentLevel");
        //}

        _currentLevel = SaveGame.Load(Keys.CurrentLevel, 0);
        _totalLevel = SaveGame.Load(Keys.TotalLevel, 0);
        _levelText.text = (_totalLevel + 1).ToString();
        LoadLevel();
    }

    public void NextButton()
    {
        if (_levelForLoad < _levels.Count)
        {
            _levelForLoad++;
        }

        _levelText.text = _levelForLoad.ToString();
        SaveGame.Save(Keys.CurrentLevel, _currentLevel - 1);
    }
    
    public void PreviousButton()
    {
        if (_levelForLoad > 1)
        {
            _levelForLoad--;
        }
        _levelText.text = _levelForLoad.ToString();
        SaveGame.Save(Keys.CurrentLevel, _currentLevel - 1);
    }
    
    

    private void LoadLevel()
    {
        if (_levels.Count > 0)
        {
            foreach (var level in _levels)
                level.gameObject.SetActive(false);

            _levels[_currentLevel].gameObject.SetActive(true);
        }
        Time.timeScale = 1;
        
        _levelCounter.Move(_totalLevel + 1);
    }

    private void SetLevel()
    {
        if (_levelForLoad != 0)
            SaveGame.Save(Keys.CurrentLevel, _levelForLoad - 1);

        //PlayerPrefs.SetInt("CurrentLevel", _levelForLoad - 1);
    }

    public void LoadNextLevel()
    {
        _currentLevel++;
        _totalLevel++;

        if (_levels.Count <= _currentLevel)
        {
            _currentLevel = 3;
        }

        Debug.Log(_totalLevel);
        SaveGame.Save(Keys.TotalLevel, _totalLevel);
        SaveGame.Save(Keys.CurrentLevel, _currentLevel);
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }
}
