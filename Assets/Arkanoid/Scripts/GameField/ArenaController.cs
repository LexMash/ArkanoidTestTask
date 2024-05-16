using UnityEngine;

namespace Arkanoid.GameField
{
    public class ArenaController : IArenaController
    {
        private readonly Arena _arena;
        private readonly BallKeeper _ballKeeper;
        private readonly BackGroundService _backGroundService;

        public ArenaController(Arena arena, BallKeeper ballKeeper, BackGroundService backGroundService)
        {
            _arena = arena;
            _ballKeeper = ballKeeper;
            _backGroundService = backGroundService;           
        }

        public async void ChangeBackground()
        {
            Sprite background = await _backGroundService.GetBackground();

            _arena.SetBackground(background);
        }

        public void KeepBall(bool keep)
        {
            if (keep)
                _ballKeeper.Enable();
            else
                _ballKeeper.Disable();
        }
    }
}
