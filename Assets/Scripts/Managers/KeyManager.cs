using UnityEngine;

namespace RollABall
{
    public static class KeyManager
    {
        public const KeyCode SAVE = (KeyCode.RightControl | KeyCode.LeftControl) & KeyCode.S;
        public const KeyCode LOAD = (KeyCode.RightControl | KeyCode.LeftControl) & KeyCode.L;
    }
}
