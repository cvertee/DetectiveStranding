using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    public class StringEvent : UnityEvent<string> {}
    public class StringArrayEvent : UnityEvent<string[]> {}
    public class CharacterEvent : UnityEvent<Character> {}
    
    public class GameEvents : Singleton<GameEvents>
    {
        public StringEvent onYarnLineStart = new StringEvent();
        public UnityEvent onYarnDialogueEnd = new UnityEvent();

        // This event is invoked when for example player clicks on dialogue box
        // or presses SPACE key to move to next dialogue/line/phrase
        public UnityEvent onDialogueNextPhraseRequested = new UnityEvent();

        // When game asks to Show or Hide dialogue box
        public UnityEvent onDialogueContainerShowRequested = new UnityEvent();
        public UnityEvent onDialogueContainerHideRequested = new UnityEvent();
        
        public StringEvent onBackgroundChangeRequest = new StringEvent();
        
        public StringArrayEvent onCharacterSetEmotionRequested = new StringArrayEvent();
        public StringEvent onCharacterRemoveRequested = new StringEvent();
        public StringArrayEvent onCharacterMoveRequested = new StringArrayEvent();

        public StringArrayEvent onCharacterSpawnRequested = new StringArrayEvent();
        public CharacterEvent onCharacterSpawned = new CharacterEvent();

        // Basically "scene" is not the unity scene (it should be renamed btw)
        // "scene" is the prefab with clickable items in it
        // which is loaded when needed and unloaded when other "scene" load is requested
        public StringEvent onSceneSwitchRequested = new StringEvent();

        // DEPRECATED for now
        public UnityEvent onStoryResultAdded = new UnityEvent();

        public StringEvent onImageShowRequested = new StringEvent();
        public UnityEvent onImageHideRequested = new UnityEvent();
    }
}