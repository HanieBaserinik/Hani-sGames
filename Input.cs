using System.Collections.Generic;
using System.Windows.Forms;

namespace GameCollection
{
    public static class Input
    {
        private static readonly Dictionary<Keys, bool> KeyTable = new Dictionary<Keys, bool>();

        public static bool KeyPress(Keys key)
        {
            return KeyTable.TryGetValue(key, out bool value) && value;
        }

        public static void ChangeState(Keys key, bool state)
        {
            KeyTable[key] = state;
        }
    }
}
