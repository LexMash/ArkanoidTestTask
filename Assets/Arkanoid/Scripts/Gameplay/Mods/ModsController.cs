using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle;
using Arkanoid.Paddle.FX.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Gameplay
{
    public class ModsController : IModificatorNotifier, IDisposable
    {
        public event Action<ModificatorData> ModAdded;
        public event Action<ModificatorData> ModRemoved;

        private readonly IReadOnlyList<ModificatorData> _modData;
        private readonly IModificators _modificators;
        private readonly IPaddleCollisionNotifier _notifier;
        private readonly Dictionary<ModType, IModificator> _modMap = new();
        
        public ModsController(ModsConfig config, IModificators modificators, IPaddleCollisionNotifier notifier)
        {
            _modData = config.ModDatas;
            _modificators = modificators;
            _notifier = notifier;

            _notifier.PowerUPTaken += OnPowerUPTaken;
        }

        public void Dispose()
        {
            _notifier.PowerUPTaken -= OnPowerUPTaken;

            _modMap.Clear();
        }

        private void OnPowerUPTaken(ModType type) => ApplyModificator(type);

        private void ApplyModificator(ModType type)
        {
            if (TryReapplyMod(type))
                return;

            RemoveConflictingModificators(type);

            AddNewModificator(type);          
        }     

        private bool TryReapplyMod(ModType type)
        {
            if (_modMap.TryGetValue(type, out IModificator applyedMod))
            {
                applyedMod.Apply();

                return true;
            }

            return false;
        }

        private void AddNewModificator(ModType type)
        {
            IModificator newMod = _modificators.Get(type);

            _modMap[type] = newMod;

            newMod.Expired += RemoveMod;

            newMod.Apply();

            ModificatorData data = GetData(type);

            ModAdded?.Invoke(data);
        }

        private void RemoveConflictingModificators(ModType type)
        {
            IEnumerable<ModificatorData> canceledMods = _modData.Where(fx => fx.ConflictModType.Contains(type));

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

        private void RemoveMod(IModificator modificator)
        {
            modificator.Expired -= RemoveMod;

            _modMap.Remove(modificator.Type);

            ModificatorData data = GetData(modificator.Type);

            ModRemoved?.Invoke(data);
        }

        private ModificatorData GetData(ModType type)
            => _modData.FirstOrDefault(data => data.Type == type);
    }
}
