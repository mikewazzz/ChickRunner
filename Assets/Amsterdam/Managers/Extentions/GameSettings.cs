using System.IO;
using UnityEngine;

namespace Amsterdam.Managers.Extentions
{
    [CreateAssetMenu(fileName = "_Game", menuName = "Base/Create GameSettings", order = 4)]
    public class GameSettings : BaseSettings<GameSettings>
    {
        [Header("Level Settings")] 
        [Tooltip("Levels are counted automatically")]
        public int totalLevelCount;
        
        [Tooltip("Only works on editor")] 
        public int forceLevel = -1;
        public bool forceSameLevel;
        
        private void OnEnable()
        {
            UpdateLevelCount();
        }

        private void OnValidate()
        {
            UpdateLevelCount();
        }

        private void UpdateLevelCount()
        {
            totalLevelCount = new DirectoryInfo("Assets/_Game/Prefabs/Resources/Levels")
                .GetFiles("Level*.prefab")
                .Length;
        }
    }
}