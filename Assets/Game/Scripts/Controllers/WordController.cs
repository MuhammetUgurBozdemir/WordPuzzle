using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Tile;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class WordController
    {
        private List<string> _dictionary = new List<string>();
        private List<string> _letters = new List<string>();

        private List<TileFacade> _submittedChars = new List<TileFacade>();
        public List<string> allChars = new List<string>();

        private int tempScore;

        #region Injection

        private SignalBus _signalBus;
        private DictionarySettings _dictionarySettings;
        private LetterBoxHolder _letterBoxHolder;
        private LevelModel _levelModel;
        private GameView _gameView;

        [Inject]
        private void Construct(SignalBus signalBus,
            DictionarySettings dictionarySettings,
            [Inject(Id = "LetterBoxHolder")] LetterBoxHolder letterBoxHolder,
            LevelModel levelModel,
            [Inject(Id = "GameView")] GameView gameView)
        {
            _signalBus = signalBus;
            _dictionarySettings = dictionarySettings;
            _letterBoxHolder = letterBoxHolder;
            _levelModel = levelModel;
            _gameView = gameView;
        }

        #endregion


        public void Init()
        {
            _signalBus.Subscribe<SubmitButtonClickedSignal>(WordSubmitClicked);
            _signalBus.Subscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Subscribe<UndoButtonClickedSignal>(RemoveClickedLetter);

            foreach (string s in _dictionarySettings.words.text.Split(new[] { Environment.NewLine },
                         StringSplitOptions.None))
            {
                _dictionary.Add(s);
            }
        }


        private WordState CheckWordIsCompleted()
        {
            string word = _letters.Aggregate("", (current, s) => current + (s));
            var result = _dictionary.Contains(word.ToLower());

            WordState wordState = new WordState
            {
                word = word,
                state = result
            };

            _gameView.SetSubmitButtonVisibility(result);

            return wordState;
        }

        private void WordSubmitClicked()
        {
            var wordState = CheckWordIsCompleted();

            if (!wordState.state) return;

            _signalBus.Fire(new WordSubmittedSignal(wordState.word));
            _letterBoxHolder.MakeAvailableAllBoxes();

            foreach (var letter in _letters)
            {
                allChars.Remove(letter);
            }

            _gameView.UpdateScoreView(CalculateScore());
            tempScore += CalculateScore();

            _letters.Clear();
            CheckForLevelAnd();
        }

        private int CalculateScore()
        {
            int totalLetterPoint = _letters.Sum(t =>
                _dictionarySettings.LetterDatas
                    .Find(x => String.Equals(x.Letter, t, StringComparison.CurrentCultureIgnoreCase)).Point);
            return _letters.Count * totalLetterPoint * 10;
        }

        private void AddClickedLetter(TileClickedSignal signal)
        {
            _letters.Add(signal.TileFacade.tileData.character);
            _submittedChars.Add(signal.TileFacade);
            CheckWordIsCompleted();
        }

        private void RemoveClickedLetter()
        {
            if (_letters.Count == 0) return;
            if (_submittedChars.Count == 0) return;

            _submittedChars[^1].SetStateMoveToInitialPos();
            _signalBus.Fire(new TileRemovedFromSubmittedSignal(_submittedChars[^1]));
            _submittedChars.RemoveAt(_submittedChars.Count - 1);
            _letters.RemoveAt(_letters.Count - 1);
            CheckWordIsCompleted();
        }

        private bool CheckForLevelAnd()
        {
            List<string> tempList = new List<string>();

            foreach (var s in _dictionary)
            {
                tempList.AddRange(allChars.Where(letter => s.Contains(letter.ToLower())));

                if (s.Length > 0)
                {
                    if (tempList.Count == s.Length)
                    {
                        return false;
                    }
                    else tempList.Clear();
                }
                else tempList.Clear();
            }

            ES3.Save("Level" + _levelModel.CurrentLevel, tempScore);

            tempScore = 0;

            if (_levelModel.CurrentLevel == _levelModel.ReachedLevel)
            {
                _levelModel.ReachedLevel++;
                ES3.Save("ReachedLevel", _levelModel.ReachedLevel);
            }

            _signalBus.Fire<LevelEndSignal>();
            _gameView.ShowMainView();

            return true;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SubmitButtonClickedSignal>(WordSubmitClicked);
            _signalBus.Unsubscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Unsubscribe<UndoButtonClickedSignal>(RemoveClickedLetter);
        }
    }
}

public class WordState
{
    public string word;
    public bool state;
}