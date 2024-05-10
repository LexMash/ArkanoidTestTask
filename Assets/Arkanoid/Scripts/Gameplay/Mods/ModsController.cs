using Arkanoid.Capsules;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle.FX.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Gameplay
{
    public class ModsController : IModsController, IDisposable
    {
        public event Action<ModType> ModAdded;
        public event Action<ModType> ModRemoved;

        private readonly ModsConfig _config;
        private readonly IModsFactory _modFactory;
        private readonly Dictionary<ModType, IModificator> _modMap = new();
     
        public ModsController(ModsConfig config, IModsFactory commandFactory)
        {
            _config = config;
            _modFactory = commandFactory;
        }

        public void ApplyFx(ModType type)
        {
            if (TryReapplyMod(type))
                return;

            RemoveConflictingModificators(type);
            AddNewModificator(type);

            ModAdded?.Invoke(type);
        }

        private bool TryReapplyMod(ModType type)
        {
            if (_modMap.TryGetValue(type, out IModificator applyedMod))
            {
                applyedMod.Reapply();
                return true;
            }

            return false;
        }

        private void RemoveConflictingModificators(ModType type)
        {
            IEnumerable<ModificatorData> canceledMods = _config.FXs.Where(fx => fx.ConflictModType.Contains(type));

            if (canceledMods.Count() == 0)
                return;

            foreach (var mod in canceledMods)
            {
                ModType canceledType = mod.Type;

                if (_modMap.TryGetValue(canceledType, out IModificator canceled))
                {
                    canceled.Rollback();
                    RemoveMod(canceled);
                }
            }
        }

        private void AddNewModificator(ModType type)
        {
            IModificator newMod = _modFactory.Create(type);
            _modMap[type] = newMod;

            newMod.Expired += RemoveMod;
            newMod.Apply();
        }

        private void RemoveMod(IModificator command)
        {
            command.Expired -= RemoveMod;
            _modMap.Remove(command.Type);

            ModRemoved?.Invoke(command.Type);
        }

        public void Dispose()
        {
            _modMap.Clear();
        }
    }
}
