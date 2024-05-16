using Arkanoid.Ball;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Input;
using Arkanoid.PowerUPs;
using System;

namespace Arkanoid.Gameplay.Mods.Implementation
{
    public class MagnetModificator : IModificator
    {
        public ModType Type => ModType.Magnet;

        public event Action<IModificator> Expired;

        private readonly IBallController _ballController;
        private readonly IInput _input;

        public MagnetModificator(IBallController ballController, IInput input)
        {
            _ballController = ballController;
            _input = input;
        }

        public void Apply()
        {
            _input.ActionPerformed += OnActionPerformed;

            _ballController.MagnetModeEnable(true);
        }

        public void Rollback()
        {
            _input.ActionPerformed -= OnActionPerformed;

            _ballController.MagnetModeEnable(false);
        }

        private void OnActionPerformed()
        {
            _ballController.ReleaseMagnet();
        }
    }
}
