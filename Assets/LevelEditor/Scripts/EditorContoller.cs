using Arkanoid.Bricks;
using Arkanoid.Levels;
using Arkanoid.PowerUPs;
using LevelEditor.UI;
using UnityEngine;

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

        private EditorLevelData _editorData;
        private DataProvider _dataProvider;

        private BrickType _currentBrick = BrickType.None;
        private ModType _currentMod = ModType.None;

        private string _levelName = string.Empty;
        private bool _eraseModeEnable;

        private void Awake()
        {
            _editorData = new(_brickConfig);

            _dataProvider = new DataProvider();
        }

        private void OnEnable()
        {
            _brickPanel.BrickTypeChoused += OnBrickTypeChoused;
            _powerUpPanel.ModTypeChoused += OnModTypeChoused;
            _clickHandler.CellClicked += CellClicked;

            _controllPanel.ResetClicked += OnResetClicked;
            _controllPanel.SaveClicked += OnSaveClicked;
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

            if(dtos.Length == 0)
                return;

            _dataProvider.SaveLevel(new LevelData(_levelName, dtos));
        }

        private void OnEraseModeClicked()
        {
            _eraseModeEnable = true;
        }

        private void OnLevelNameChanged(string name) => _levelName = name;

        private bool BrickChoused() => _currentBrick != BrickType.None && _currentMod == ModType.None;
        private bool ModChoused() => _currentBrick == BrickType.None && _currentMod != ModType.None;
        private bool NothingSelected() => _currentBrick == BrickType.None && _currentMod == ModType.None;
    }
}
