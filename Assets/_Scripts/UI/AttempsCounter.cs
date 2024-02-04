using System;
using System.Collections.Generic;
using _Scripts.TakenObjects;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace _Scripts.UI
{
    public class AttempsCounter : MonoBehaviour
    {
        public static AttempsCounter Instance;

        [SerializeField] private TMP_Text _starsText;
        [SerializeField] private TMP_Text _attemptText;
        
        private List<TakenObject> _takenObjects = new List<TakenObject>();
        private int _levelAttemps;
        private int _currentAttemps = 0;

        public int LevelAttemps
        {
            get => _levelAttemps;
            set
            {
                _levelAttemps = value;
                UpdateText();
            }
        }

        private void Start()
        {
            Invoke(nameof(UpdateText), .1f);
        }

        private void UpdateText()
        {
            _starsText.text = _takenObjects.Count.ToString();
            _attemptText.text = _currentAttemps + "/" + _levelAttemps;
        }

        public List<TakenObject> TakenObjectsList
        {
            get => _takenObjects;
            set => _takenObjects = value;
        }

        public int CurrentAttemps
        {
            get => _currentAttemps;
            set
            {
                _currentAttemps = value;
                UpdateText();
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                transform.SetParent(null);
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void RemoveFromList(TakenObject takenObject)
        {
            _takenObjects.Remove(takenObject);
            UpdateText();
            if (_takenObjects.Count == 0)
            {
                if (_currentAttemps == _levelAttemps)
                {
                    StarManager.Instance.GiveStar(3);
                }else if (_currentAttemps == _levelAttemps + 1)
                {
                    StarManager.Instance.GiveStar(2);
                }
                else
                {
                    StarManager.Instance.GiveStar(1);
                }
                
                FinishAction.Finish.Invoke(FinishAction.FinishType.Win);
                return;
            }
            
            if (_currentAttemps == 0)
            {
                FinishAction.Finish.Invoke(FinishAction.FinishType.Lose);
            }
        }
    }
}