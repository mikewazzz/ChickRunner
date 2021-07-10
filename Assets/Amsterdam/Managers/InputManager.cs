using _Game.Prefabs.Resources.Levels.Base;
using Amsterdam.Managers.Extentions;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Amsterdam.Managers
{
    public class InputManager : Singleton<InputManager>, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public Player _player;
        public GameEvent inputBegin;
        public GameEvent inputEnd;
        public bool didFirstTouch=false;
        


        
        #region Pointer

        public void OnFirstTouch()
        {
            if (LevelManager.Instance.CurrentLevel.state   == Level.State.Loaded && !didFirstTouch)
            {
                didFirstTouch = true;
                LevelManager.Instance.CurrentLevel.StartCurrentLevel();
            
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            inputBegin.Raise();
            _player.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _player.OnPointerDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputEnd.Raise();
            _player.OnPointerUp(eventData);
        }

        #endregion
    }
}