using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor.UI
{
    public class ControllPanel : MonoBehaviour
    {
        [SerializeField] private Button _resetBttn;
        [SerializeField] private Button _saveLevelBttn;
        [SerializeField] private Button _loadLevelBttn;
        [SerializeField] private Button _eraseModeBttn;
        [SerializeField] private TMP_InputField _inputField;

        public event Action ResetClicked;
        public event Action SaveClicked;
        public event Action LoadClicked;
        public event Action EraseClicked;
        public event Action<string> LevelNameChanged;

        private void OnEnable()
        {
            _resetBttn.onClick.AddListener(OnResetClicked);
            _saveLevelBttn.onClick.AddListener(OnSaveClicked);
            _loadLevelBttn.onClick.AddListener(OnLoadClicked);
            _eraseModeBttn.onClick.AddListener(OnReaseBttnClicked);
            _inputField.onValueChanged.AddListener(OnLevelNameChanged);
        }

        private void OnDisable()
        {
            _resetBttn.onClick.RemoveListener(OnResetClicked);
            _saveLevelBttn.onClick.RemoveListener(OnSaveClicked);
            _loadLevelBttn.onClick.RemoveListener(OnLoadClicked);
            _eraseModeBttn.onClick.RemoveListener(OnReaseBttnClicked);
            _inputField.onValueChanged.RemoveListener(OnLevelNameChanged);
        }       

        private void OnResetClicked() => ResetClicked?.Invoke();
        private void OnSaveClicked() => SaveClicked?.Invoke();
        private void OnLoadClicked() => LoadClicked?.Invoke();
        private void OnReaseBttnClicked() => EraseClicked?.Invoke();
        private void OnLevelNameChanged(string name) => LevelNameChanged?.Invoke(name);
    }
}
