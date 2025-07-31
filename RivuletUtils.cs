using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wawa
{
    public class RivuletUtils
    {
        public static void Log(string message)
        {
            if (Chat.Instance != null)
                Chat.Instance.Send("[color=#999999]" + message + "[/color]", false);
            else
                Chat.pendingMessages.Add("[color=#999999]" + message + "[/color]");
        }
    }
}
