using Amsterdam.Managers.Extentions;
using UnityEngine;

namespace _Game.Prefabs.Resources.Levels.Base
{
    public class Level : MonoBehaviour
    {
        public enum State
        {
            None,
            Loaded,
            Started,
            Success,
            Fail
        }

        public State state = State.None;
        public GameEvent levelLoaded;
        public GameEvent levelStarted;
        public GameEvent levelSuccess;
        public GameEvent levelFail;


        #region Life Cycle

        void Start()
        {
            state = State.Loaded;
            levelLoaded.Raise();
        }

        void Update()
        {
        
        }

        #endregion


        #region Level Events

        public void StartCurrentLevel()
        {
            if (state == State.Loaded)
            {
                state = State.Started;
                Debug.Log("Level started!");
                levelStarted.Raise();
            }
            else
            {
                Debug.LogError("Level is not loaded");
            }
      
        }
        
        public void SuccessCurrentLevel()
        {
            if (state == State.Started)
            {
                state = State.Success;
                Debug.Log("Level success!");
                levelSuccess.Raise();
            }
            else
            {
                Debug.LogError("Level is not started");
            }
        }
    
        public void FailCurrentLevel()
        {
            if (state == State.Started)
            {
                state = State.Fail;
                Debug.Log("Level fail!");
                levelFail.Raise();
            }
            else
            {
                Debug.LogError("Level is not started");
            }
      
        }

        #endregion   
   
    }
}
