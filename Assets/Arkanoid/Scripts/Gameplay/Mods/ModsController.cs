using Arkanoid.PowerUPs;
using Arkanoid.Gameplay.Modificators;
using Arkanoid.Paddle;
using Arkanoid.Paddle.FX.Configs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Arkanoid.Gameplay
{
    public class ModsController : IModsController, IDisposable
    {
        public event Action<ModificatorData> ModAdded;
        public event Action<ModificatorData> ModRemoved;

        private readonly ModsConfig _config;
        private readonly IModsFactory _modFactory;
        private readonly IPaddleCollisionNotifier _notifier;
        private readonly Dictionary<ModType, IModificator> _modMap = new();
     
        public ModsController(ModsConfig config, IModsFactory modsFactory, IPaddleCollisionNotifier notifier)
        {
            _config = config;
            _modFactory = modsFactory;
            _notifier = notifier;

            _notifier.ModTaken += OnModTaken;
        }

        public void Dispose()
        {
            _notifier.ModTaken -= OnModTaken;

            _modMap.Clear();
        }

        private void OnModTaken(ModType type) => ApplyModificator(type);

        private void ApplyModificator(ModType type)
        {
            if (TryReapplyMod(type))
                return;

            RemoveConflictingModificators(type);
            AddNewModificator(type);

            ModificatorData data = GetData(type);
            ModAdded?.Invoke(data);
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
            IEnumerable<ModificatorData> canceledMods = _config.ModDatas.Where(fx => fx.ConflictModType.Contains(type));

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

        private void RemoveMod(IModificator modificator)
        {
            modificator.Expired -= RemoveMod;
            _modMap.Remove(modificator.Type);

            ModificatorData data = GetData(modificator.Type);
            ModRemoved?.Invoke(data);
        }

        private ModificatorData GetData(ModType type)
            => _config.ModDatas.FirstOrDefault(data => data.Type == type);
    }
}
