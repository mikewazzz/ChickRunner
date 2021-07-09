using Amsterdam.Managers;
using Amsterdam.Managers.Extentions;
using UnityEngine;
using UnityEngine.UI;

namespace Amsterdam.UI
{
    public class UIManager : Singleton<UIManager>
    {
        public GameObject tutorialScreen;
        public GameObject failScreen;
        public GameObject successScreen;
        public Text levelText;

        public void LoadUIElements()
        {
            int levelIdx = LevelManager.Instance.DisplayingLevelIdx;
            levelText.text = "Level " + levelIdx;
            
            tutorialScreen.SetActive(true);
            failScreen.SetActive(false);
            successScreen.SetActive(false);
        }
    
    
        public void HideTutorialPanel()
        {
            tutorialScreen.SetActive(false);
        }
        public void ShowSuccesPanel()
        {
            successScreen.SetActive(true);
        }
        public void ShowFailPanel()
        {
            failScreen.SetActive(true);
        }
    }
}
