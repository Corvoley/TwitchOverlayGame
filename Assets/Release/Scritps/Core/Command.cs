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
        public static event Action<string, int> OnItemBuy;
        public BuyItem(string name) : base(name)
        {
        }
        public override void CallFunction()
        {
            id = CommandParametersHandler.param;
            args = CommandParametersHandler.param3;
            number = int.Parse(args);
            OnItemBuy?.Invoke(id, number);
        }
        public override string GetMessage(string id, string name, string args)
        {
            return $"{name} comprou {number} itens!";
        }
    }


    public class CollectorJob : Command
    {
        public string id;
        public string args;
        public string type;
        public Player.Jobs job;
        public Collector.ResourceType resourceType;
        public static event Action<string,Player.Jobs,Collector.ResourceType> OnJobChanged;

        public CollectorJob(string name) : base(name)
        {

        }

        public override void CallFunction()
        {
            id = CommandParametersHandler.param;
            type = CommandParametersHandler.param3;
            switch (type)
            {
                case "wood":
                    resourceType = Collector.ResourceType.Wood; 
                    break;
                case "food":
                    resourceType = Collector.ResourceType.Food;
                    break;
                case "gold":
                    resourceType = Collector.ResourceType.Gold;
                    break;
                case "stone":
                    resourceType = Collector.ResourceType.Stone;
                    break;
                default:
                    break;
            }

            job = Player.Jobs.Collector;
            
            OnJobChanged?.Invoke(id, job, resourceType);
           

        }
        public override string GetMessage(string id, string name, string args)
        {
            if (type == "wood" || type == "food" || type == "gold" || type == "stone")
            {
                return $"{name} mudou sua profissão para {type} collector!";
            }
            else
            {
                return $"{name} por favor escolha entre '!collector wood' ou '!collector food'";
            }
            
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
