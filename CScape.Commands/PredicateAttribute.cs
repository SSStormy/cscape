using System;

namespace CScape.Commands
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class PredicateAttribute : Attribute
    {
        public abstract bool CanExecute(CommandContext ctx, Command command);
    }
}