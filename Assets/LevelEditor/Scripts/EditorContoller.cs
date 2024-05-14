using Arkanoid.Bricks;
using Arkanoid.Levels;
using Arkanoid.PowerUPs;
using Cysharp.Threading.Tasks;
using LevelEditor.UI;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace LevelEditor.Editor
{
    public class EditorContoller : MonoBehaviour
    {
        [SerializeField] private BrickUIPanel _brickPanel;
        [SerializeField] private PowerUpUIPanel _powerUpPanel;
        [SerializeField] private ClickHandler _clickHandler;
        [SerializeField] private ControllPanel _controllPanel;
        [SerializeField] private EditorVisualizer _visualizer;
        [SerializeField] private BrickConfig _brickConfig;
        //[SerializeField] private TextMeshProUGUI _log;

        private EditorLevelData _editorData;
        private EditorDataService _dataProvider;

        private BrickType _currentBrick = BrickType.None;
        private ModType _currentMod = ModType.None;

        private List<string> _levels = new List<string>();
        private string _levelName = string.Empty;
        private bool _eraseModeEnable;

        private void Awake()
        {
            _editorData = new(_brickConfig);

            _dataProvider = new EditorDataService();

            _levels = _dataProvider.LoadLevelList();
        }

        private void OnEnable()
        {
            _brickPanel.BrickTypeChoused += OnBrickTypeChoused;
            _powerUpPanel.ModTypeChoused += OnModTypeChoused;
            _clickHandler.CellClicked += CellClicked;

            _controllPanel.ResetClicked += OnResetClicked;
            _controllPanel.SaveClicked += OnSaveClicked;
            _controllPanel.LoadClicked += OnLoadClicked;
            _controllPanel.EraseClicked += OnEraseModeClicked;
            _controllPanel.LevelNameChanged += OnLevelNameChanged;
        }

        private void OnDisable()
        {
            _brickPanel.BrickTypeChoused -= OnBrickTypeChoused;
            _powerUpPanel.ModTypeChoused -= OnModTypeChoused;
            _clickHandler.CellClicked -= CellClicked;

            _controllPanel.ResetClicked -= OnResetClicked;
            _controllPanel.SaveClicked -= OnSaveClicked;
            _controllPanel.LoadClicked -= OnLoadClicked;
            _controllPanel.EraseClicked -= OnEraseModeClicked;
            _controllPanel.LevelNameChanged -= OnLevelNameChanged;
        }

        private void OnBrickTypeChoused(BrickType type)
        {
            _currentBrick = type;

            _currentMod = ModType.None;

            _eraseModeEnable = false;

            _visualizer.SetBrick(type);
        }

        private void OnModTypeChoused(ModType type)
        {
            _currentMod = type;

            _currentBrick = BrickType.None;

            _eraseModeEnable = false;

            _visualizer.SetMod(type);
        }

        private void CreateNewLevel()
        {
            _editorData.ResetData();

            _levelName = string.Empty;

            _visualizer.ResetCurrentObject();
        }

        private void CellClicked(Vector3 cellPosition)
        {
            if (NothingSelected())
            {
                if (_eraseModeEnable)
                {
                    _editorData.RemoveBrick(cellPosition);

                    _visualizer.RemoveObject(cellPosition);
                }                   

                return;
            }

            if (ModChoused())
            {
                if (_editorData.TryAddPowerUp(cellPosition, _currentMod))
                { 
                    _currentMod = ModType.None;

                    _visualizer.PlaceObject(cellPosition);

                    return;
                }
            }

            if (BrickChoused())
            {
                _editorData.RemoveBrick(cellPosition);
                _editorData.AddBrick(cellPosition, _currentBrick);               

                _currentBrick = BrickType.None;

                _visualizer.RemoveObject(cellPosition);
                _visualizer.PlaceObject(cellPosition);

                return;
            }
        }

        private void OnResetClicked()
        {
            CreateNewLevel();

            _visualizer.DestroyAll();
            _visualizer.ResetCurrentObject();
        }

        private void OnSaveClicked()
        {
            BrickDTO[] dtos = _editorData.GetData();

            if (dtos.Length == 0)
                return;

            _dataProvider.SaveLevel(new Level(_levelName, dtos));

            if (LevelAvailableInList())
            {
                return;
            }

            _levels.Add(_levelName);

            _dataProvider.SaveLevelList(_levels);
        }

        private bool LevelAvailableInList() => _levels.Any(levelName => levelName.Equals(_levelName));

        private async void OnLoadClicked()
        {
            //_log.text = "start load";

            var levelData = await _dataProvider.LoadLevelData(_levelName);

            _levelName = levelData.Name;

            _editorData.ResetData();
            _visualizer.DestroyAll();

            for (int i = 0; i < levelData.BricksData.Length; i++)
            {
                BrickDTO brickData = levelData.BricksData[i];

                var position = new Vector2(brickData.XPosition, brickData.YPosition);

                _editorData.AddBrick(position, brickData.Type);
                _editorData.TryAddPowerUp(position, brickData.FxType);
            }

            _visualizer.Setup(levelData);
        }

        //private void ShowResult(UnityWebRequest.Result result, string levelName)
        //{
        //    _log.text = $"{result} for level {levelName}";
        //}

        private void OnEraseModeClicked()
        {
            _eraseModeEnable = true;
        }

        private void OnLevelNameChanged(string name)
        {
            _levelName = name;
        }

        private bool BrickChoused() => _currentBrick != BrickType.None && _currentMod == ModType.None;
        private bool ModChoused() => _currentBrick == BrickType.None && _currentMod != ModType.None;
        private bool NothingSelected() => _currentBrick == BrickType.None && _currentMod == ModType.None;
    }
}
