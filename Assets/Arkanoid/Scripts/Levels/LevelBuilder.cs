using Arkanoid.Bricks;
using System;
using UnityEngine;

namespace Arkanoid.Levels
{
    public class LevelBuilder : IDisposable
    {
        private readonly BrickFactory _factory;

        private BrickView[] _bricksObjects = null;

        public LevelBuilder(BrickFactory factory)
        {
            _factory = factory;
        }

        public BrickView[] GetBuildedData(BrickDTO[] dtos)
        {
            _bricksObjects = new BrickView[dtos.Length];

            for (int i = 0; i < dtos.Length; i++)
            {
                BrickDTO brickData = dtos[i];

                var position = new Vector2(brickData.XPosition, brickData.YPosition);

                BrickView brick = _factory.Create(brickData.Type, position);

                _bricksObjects[i] = brick;
            }

            return _bricksObjects;
        }

        public void DestroyAllBuilded()
        {
            for (int i = 0; i < _bricksObjects.Length; i++)
            {
                BrickView brick = _bricksObjects[i];

                if (brick.gameObject.activeSelf)
                {
                    brick.Destroy(false);   
                }
            }

            _bricksObjects = null;
        }

        public void Dispose()
        {
            _bricksObjects = null;
        }
    }
}
