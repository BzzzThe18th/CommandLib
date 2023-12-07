using BepInEx;
using HarmonyLib;
using System.Reflection;
using UnityEngine;

namespace CommandLib
{
    public class ModInfo
    {
        public const string name = "Command Library";
        public const string guid = "buzzbb.cmdlib";
        public const string version = "1.0.0";
    }

    [BepInPlugin(ModInfo.guid, ModInfo.name, ModInfo.version)]
    public class Commands : BaseUnityPlugin
    {
        private Harmony harmonyInstance = new Harmony(ModInfo.guid);
        void Awake()
        {
            Logger.LogInfo($"Reached awake point for {ModInfo.name}@{ModInfo.version}, applying patches");
            harmonyInstance.PatchAll(Assembly.GetExecutingAssembly());
            Logger.LogInfo("Applied patches");
        }

        /*
            if you want to just make a command, use this, word is the word that triggers it, return text is what it spits back, event name is the terminal event name (exmaple: https://github.com/BzzzThe18th/CustomTerminal/blob/main/Patches/TerminalPatches.cs)
        */
        public static void RegisterCommand(string word, string returnText, string eventName, Terminal terminal)
        {
            TerminalKeyword keyword = MakeKeyword(word.ToLower());
            TerminalNode node = MakeNode(returnText, eventName);
            keyword.specialKeywordResult = node;
            RegisterKeyword(keyword, terminal);
        }

        /*
            both of these are just creating scriptable object instances and setting the variables
        */
        public static TerminalKeyword MakeKeyword(string word)
        {
            
            TerminalKeyword keyword = ScriptableObject.CreateInstance<TerminalKeyword>();
            keyword.word = word;
            return keyword;
        }
        public static TerminalNode MakeNode(string returnText, string associatedEvent) {
            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            node.displayText = returnText;
            node.clearPreviousText = true;
            node.terminalEvent = associatedEvent;
            return node;
        }
        
        /*
            just add the keyword into the terminal list
        */
        public static void RegisterKeyword(TerminalKeyword keyword, Terminal terminal)
        {
            if (terminal != null)
            {
                terminal.terminalNodes.allKeywords = terminal.terminalNodes.allKeywords.AddToArray(keyword);
            }
            else
            {
                Debug.LogError("Tried to register keyword with null terminal");
            }
        }
    }
}
