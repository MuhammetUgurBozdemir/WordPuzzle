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

        #region Injection

        private SignalBus _signalBus;
        private LibrarySettings _librarySettings;
        private LetterBoxHolder _letterBoxHolder;

        [Inject]
        private void Construct(SignalBus signalBus,
            LibrarySettings librarySettings,
            [Inject(Id = "LetterBoxHolder")] LetterBoxHolder letterBoxHolder)
        {
            _signalBus = signalBus;
            _librarySettings = librarySettings;
            _letterBoxHolder = letterBoxHolder;
        }

        #endregion


        public void Init()
        {
            _signalBus.Subscribe<SubmitButtonClickedSignal>(CheckWordIsCompleted);
            _signalBus.Subscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Subscribe<UndoButtonClickedSignal>(RemoveClickedLetter);

            foreach (string s in _librarySettings.words.text.Split(new[] { Environment.NewLine },
                         StringSplitOptions.None))
            {
                _dictionary.Add(s);
            }
        }


        private void CheckWordIsCompleted()
        {
            string  word = _letters.Aggregate("", ( current, s) => current + (s));
            var result = _dictionary.Contains(word.ToLower());

            if (result)
            {
                _signalBus.Fire(new WordSubmittedSignal(word));
                _letterBoxHolder.MakeAvailableAllBoxes();
                
                foreach (var letter in _letters)
                {
                    allChars.Remove(letter);
                }
                
                _letters.Clear();
                CheckForLevelAnd();
            }
        }

        private void AddClickedLetter(TileClickedSignal signal)
        {
            _letters.Add(signal.TileFacade.tileData.character);
            _submittedChars.Add(signal.TileFacade);
        }

        private void RemoveClickedLetter()
        {
            if (_submittedChars.Count == 0) return;

            _submittedChars[^1].SetStateMoveToInitialPos();
            _signalBus.Fire(new TileRemovedFromSubmittedSignal(_submittedChars[^1]));
            _submittedChars.RemoveAt(_submittedChars.Count - 1);
            _letters.RemoveAt(_letters.Count - 1);
        }

        private bool CheckForLevelAnd()
        {
            List<string> tempList = new List<string>();

            foreach (var s in _dictionary)
            {
                foreach (var letter in allChars)
                {
                    if (s.Contains(letter.ToLower()))
                    {
                        tempList.Add(letter);
                    }
                }

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

            _signalBus.Fire<LevelEndSignal>();
            return true;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SubmitButtonClickedSignal>(CheckWordIsCompleted);
            _signalBus.Unsubscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Unsubscribe<UndoButtonClickedSignal>(RemoveClickedLetter);
        }
    }
}