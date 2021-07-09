using Amsterdam.Managers.Extentions;
using UnityEngine.EventSystems;

namespace Amsterdam.Managers
{
    public class InputManager : Singleton<InputManager>, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private Player _player;
        public GameEvent inputBegin;
        public GameEvent inputEnd;


        #region Life Cycle

        private void Start()
        {
            // _player = FindObjectOfType<Player>();
        }

        #endregion


        #region Pointer

        public void OnPointerDown(PointerEventData eventData)
        {
            inputBegin.Raise();
            // _player.OnPointerDown(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            // _player.OnPointerDrag(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            inputEnd.Raise();
            // _player.OnPointerUp(eventData);
        }

        #endregion
    }
}