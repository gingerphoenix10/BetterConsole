using BepInEx;
using HarmonyLib;

namespace BetterConsole;

[ContentWarningPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, true)]
[BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private static readonly Harmony Patcher = new(MyPluginInfo.PLUGIN_GUID);
    private void Awake()
    {
        Patcher.PatchAll();
    }
}