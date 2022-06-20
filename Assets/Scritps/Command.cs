using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace TwitchBot.Commands
{
    public class Command
    {
        public string CommandName { get; set; }
        public Command(string name)
        {
            CommandName = name;         
        }
        public virtual void CallFunction(string name, string param)
        {
           
        }
        public virtual string GetMessage(string name, string param)
        {
            return "";
        }
    } 

    public class BuyItem : Command
    {
        private int number;
        public BuyItem(string name) : base(name)
        {
        }
        public override void CallFunction(string name, string param)
        {
            number = int.Parse(param);
        }
        public override string GetMessage(string name, string param)
        {
            return $"{name} comprou {number} itens!";
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
