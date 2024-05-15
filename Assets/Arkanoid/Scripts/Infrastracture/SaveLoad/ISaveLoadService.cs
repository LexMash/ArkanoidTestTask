using System;

namespace Arkanoid
{
    public interface ISaveLoadService
    {
        T Load<T>(string fileName) where T : class;
        void Save(object data, string fileName, Action onSaveCallback = null);
    }
}