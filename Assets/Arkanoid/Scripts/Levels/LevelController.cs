using Arkanoid.Bricks;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class LevelController : ILevelController, ILevelsEventNotifier, IBrickEventNotifier, IDisposable
    {
        public event Action<string> LevelLoaded;
        public event Action<string> LevelRestarted;
        public event Action LevelCompleted;

        public event Action OnAllBricksRemoved;
        public event Action<HitBrickData> OnBrickDestroyed;
        public event Action<HitBrickData> OnBrickHitted;

        private readonly ILevelDataService _dataService;
        private readonly LevelBuilder _builder;
        private readonly BrickService _brickService;

        private List<string> _levels;

        private Level _currentLevel;

        public LevelController(ILevelDataService dataService, LevelBuilder builder, BrickService brickService)
        {
            _dataService = dataService;
            _builder = builder;
            _brickService = brickService;          

            _brickService.BrickHitted += BrickHitted;
            _brickService.BrickDestroyed += BrickDestroyed;
            _brickService.AllBricksRemoved += AllBricksRemoved;
        }

        public async void Init()
        {
            _levels = await _dataService.LoadLevelList();
        }

        public async void Load(int index)
        {
            index = Mathf.Clamp(index, 0, _levels.Count - 1); //залушка, что бы не падало

            string levelName = _levels[index];

            if (_currentLevel != null)
            {
                _builder.DestroyAllBuilded();
            }

            if (TryLoadSameLevel(levelName))
            {
                Restart();

                Debug.Log($"You are trying to load the same level {levelName} again");
                return;
            }

            _currentLevel = await _dataService.LoadLevelData(levelName);

            BrickView[] buildedData = _builder.GetBuildedData(_currentLevel.BricksData);

            _brickService.Init(buildedData, _currentLevel.BricksData);

            LevelLoaded?.Invoke(levelName);
        }

        public void Restart()
        {
            BrickView[] buildedData = _builder.RebuildLevel();

            _brickService.Init(buildedData, _currentLevel.BricksData);

            LevelRestarted?.Invoke(_currentLevel.Name);
        }

        public void MakeBricksHollow() => _brickService.EnableTriggerMode(true);

        public void MakeBricksSolid() => _brickService.EnableTriggerMode(false);

        public void Dispose()
        {
            _currentLevel = null;

            _brickService.BrickHitted -= BrickHitted;
            _brickService.BrickDestroyed -= BrickDestroyed;
            _brickService.AllBricksRemoved -= AllBricksRemoved;
        }

        private bool TryLoadSameLevel(string levelName) => _currentLevel.Name == levelName;

        private void AllBricksRemoved()
        {
            LevelCompleted?.Invoke();
            OnAllBricksRemoved?.Invoke();
        }

        private void BrickDestroyed(HitBrickData hitData) => OnBrickDestroyed?.Invoke(hitData);

        private void BrickHitted(HitBrickData hitData) => OnBrickHitted?.Invoke(hitData);
    }
}
