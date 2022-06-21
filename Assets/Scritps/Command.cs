using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TwitchBot.Commands
{
    public static class CommandParametersHandler
    {
        public static string? param;
        public static string? param2;
        internal static string? param3;
    }

    public class Command
    {
        public string CommandName { get; set; }
        public Command(string name)
        {
            CommandName = name;
        }

        public virtual void CallFunction()
        {
        }
        public virtual string GetMessage(string id, string name, string args)
        {
            return "";
        }
    }

    public class BuyItem : Command
    {        
        public int number;
        public string id;
        public string args;
        public static event Action<string,int> OnItemBuy;
        public BuyItem(string name) : base(name)
        {
        }
        public override void CallFunction()
        {

            id = CommandParametersHandler.param;
            args = CommandParametersHandler.param3;
            number = int.Parse(args);

            OnItemBuy?.Invoke(id,number);
            


        }
        public override string GetMessage(string id, string name, string args)
        {
            return $"{name} comprou {number} itens!";
        }


    }

    public class Join : Command
    {

        public Join(string name) : base(name)
        {

        }

        public override void CallFunction()
        {
            string id = CommandParametersHandler.param;
            string name = CommandParametersHandler.param2;

        }

    }


    public static class CommandList
    {
        public static List<Command> commands = new List<Command>();

        public static void AddNewCommand(string name)
        {
            var command = new Command(name);

            commands.Add(command);

        }
    }

}
