using System.Linq;
using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers.Extentions;
using UnityEngine;

namespace Amsterdam.Managers
{
    public class LevelManager : Singleton<LevelManager>
    {
        
        private static string CurrentLevelIndexKey = "CurrentLevelIndex";
        private static string DisplayingLevelKey = "DisplayingLevel";

        public Level CurrentLevel => _currentLevel;
        private Level _currentLevel;

        public int CurrentLevelIdx => _currentLevelIdx;
        private int _currentLevelIdx;

        public int DisplayingLevelIdx => _displayingLevelIdx;
        private int _displayingLevelIdx;

        #region Life Cycle

        private void Start()
        {
            _displayingLevelIdx = PlayerPrefs.GetInt(DisplayingLevelKey, 0);
            int lastPlayedLevelIdx = PlayerPrefs.GetInt(CurrentLevelIndexKey, 0);

#if UNITY_EDITOR
            if (GameSettings.Current.forceLevel >= 0)
            {
                lastPlayedLevelIdx = GameSettings.Current.forceLevel;
            }
            //later: add editor button
#endif

            LoadLevel(lastPlayedLevelIdx);
        }

        #endregion


        #region Loading

        public void LoadLevel(int levelIdx)
        {
            if (levelIdx >= GameSettings.Current.totalLevelCount)
            {
                var arr = Enumerable.Range(0, GameSettings.Current.totalLevelCount)
                    .ToArray();
                levelIdx = arr.RandomElement(levelIdx);
                //later: change randomElement method
            }

            if (_currentLevel != null)
            {
                DestroyImmediate(_currentLevel.gameObject);
            }

            /*InputManager.Instance.didFirstTouch = false;*/
            //later:add first touch.

            var currentLevelObj = Instantiate(Resources.Load("Levels/Level" + levelIdx, typeof(GameObject)) as GameObject);
            var level = currentLevelObj.GetComponent<Level>();
            _currentLevelIdx = levelIdx;
            _currentLevel = level;

            PlayerPrefs.SetInt(CurrentLevelIndexKey, levelIdx);
            PlayerPrefs.Save();
        }

        public void RestartCurrentLevel()
        {
            LoadLevel(_currentLevelIdx);
        }

        public void LoadNextLevel()
        {
            _displayingLevelIdx = PlayerPrefs.GetInt(DisplayingLevelKey, 0);
            _displayingLevelIdx++;
            PlayerPrefs.SetInt(DisplayingLevelKey, _displayingLevelIdx);

            int lastPlayedLevelIdx = PlayerPrefs.GetInt(CurrentLevelIndexKey, 0);
            bool isUnityEditor = false;
#if UNITY_EDITOR
            isUnityEditor = true;
#endif
            if (!isUnityEditor || !GameSettings.Current.forceSameLevel)
            {
                lastPlayedLevelIdx++;
                PlayerPrefs.SetInt(CurrentLevelIndexKey, lastPlayedLevelIdx);
                PlayerPrefs.Save();
            }

            LoadLevel(lastPlayedLevelIdx);
        }
    }
}

#endregion