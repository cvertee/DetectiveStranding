using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class StringEvent : UnityEvent<string> {}
    public class StringArrayEvent : UnityEvent<string[]> {}
    
    public class GameEvents : Singleton<GameEvents>
    {
        public StringEvent onYarnLineStart = new StringEvent();
        public UnityEvent onYarnDialogueEnd = new UnityEvent();
        public UnityEvent onDialogueNextPhraseRequested = new UnityEvent();

        public UnityEvent onDialogueContainerShowRequested = new UnityEvent();
        public UnityEvent onDialogueContainerHideRequested = new UnityEvent();
        
        public StringEvent onBackgroundChangeRequest = new StringEvent();
        
        public StringArrayEvent onCharacterSetEmotionRequested = new StringArrayEvent();
        public StringEvent onCharacterRemoveRequested = new StringEvent();
        public StringArrayEvent onCharacterMoveRequested = new StringArrayEvent();

        public StringArrayEvent onCharacterSpawnRequested = new StringArrayEvent();

        public UnityEvent onSceneSwitchRequested = new UnityEvent();

        public UnityEvent onStoryResultAdded = new UnityEvent();
    }
}