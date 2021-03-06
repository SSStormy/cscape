using System;
using CScape.Models.Game.Entity;

namespace CScape.Commands
{
    public static class Paramaters
    {
        public static bool Read(this CommandContext ctx, Action<ParamaterLexer> builder)
        {
            try
            {
                var lexer = new ParamaterLexer(ctx.Data);
                builder(lexer);

                if (lexer.DidFail)
                {
                    if (lexer.FailParamExpected != null)
                    {
                        ctx.Callee.Parent.SystemMessage(
                            $"Invalid type for argument {lexer.FailedOnParam}. Expected: {lexer.FailParamExpected.Name}.", (ulong)SystemMessageFlags.Normal | CommandSystemMessageType.Id);
                    }
                    else
                        ctx.Callee.Parent.SystemMessage($"Missing argument: {lexer.FailedOnParam}.", (ulong)SystemMessageFlags.Normal | CommandSystemMessageType.Id);

                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                ctx.Callee.Parent.SystemMessage("Command paramater error.", (ulong)SystemMessageFlags.Normal | CommandSystemMessageType.Id);
                ctx.Callee.Parent.SystemMessage($"Paramaters.Read exception: {ex}", (ulong)SystemMessageFlags.Debug | CommandSystemMessageType.Id);
            }
            return false;
        }
    }
}