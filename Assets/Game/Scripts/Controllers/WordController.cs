using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Controllers
{
    public class WordController
    {
        private List<string> _library = new List<string>();
        private List<string> _letters= new List<string>();

        #region Injection

        private SignalBus _signalBus;
        private LibrarySettings _librarySettings;

        [Inject]
        private void Construct(SignalBus signalBus,
            LibrarySettings librarySettings)
        {
            _signalBus = signalBus;
            _librarySettings = librarySettings;
        }

        #endregion
        
      

     
        public void Init()
        {
            _signalBus.Subscribe<SubmitButtonClicked>(CheckWordIsCompleted);
            _signalBus.Subscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Subscribe<UndoButtonClicked>(RemoveClickedLetter);
            
            foreach (string s in _librarySettings.words.text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                _library.Add(s);
            }
        }


        private void CheckWordIsCompleted()
        {
            string word = _letters.Aggregate("", (current, s) => current + (s));
            var result = _library.Contains(word.ToLower());
            
            if(result) _signalBus.Fire(new WordSubmittedSignal(word));
        }

        private void AddClickedLetter(TileClickedSignal signal)
        {
            _letters.Add(signal.TileData.character);
        }
        private void RemoveClickedLetter()
        {
            
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<SubmitButtonClicked>(CheckWordIsCompleted);
            _signalBus.Unsubscribe<TileClickedSignal>(AddClickedLetter);
            _signalBus.Unsubscribe<UndoButtonClicked>(RemoveClickedLetter);
        }

       
    }
}