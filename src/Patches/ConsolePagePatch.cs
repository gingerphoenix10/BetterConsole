using System.Reflection;
using HarmonyLib;
using UnityEngine;
using Zorro.Core.CLI;

namespace BetterConsole.Patches;

[HarmonyPatch(typeof(ConsolePage))]
public class ConsolePagePatch
{
    private static bool pasted = false;
    private static bool copied = false;
    
    [HarmonyPrefix]
    [HarmonyPatch(nameof(ConsolePage.Update))]
    public static void Update(ConsolePage __instance)
    {
        string m_currentInput = (string)typeof(ConsolePage)
            .GetField("m_currentInput", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(__instance);
        if (UnityEngine.Input.GetKey(KeyCode.LeftControl) || UnityEngine.Input.GetKey(KeyCode.RightControl))
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.V))
            {
                if (!pasted)
                {
                    typeof(ConsolePage).GetField("m_currentInput", BindingFlags.NonPublic | BindingFlags.Instance)
                        .SetValue(__instance, m_currentInput + GUIUtility.systemCopyBuffer);
                    pasted = true;
                }
            }
            else if (pasted) pasted = false;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.C))
            {
                if (!copied)
                {
                    TextEditor te = new TextEditor();
                    te.text = m_currentInput;
                    te.SelectAll();
                    te.Copy();
                    copied = true;
                }
            }
            else if (copied) copied = false;
            
        }
    }
}