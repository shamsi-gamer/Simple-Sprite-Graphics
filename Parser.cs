using Sandbox.Game.EntityComponents;
using Sandbox.ModAPI.Ingame;
using Sandbox.ModAPI.Interfaces;
using SpaceEngineers.Game.ModAPI.Ingame;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using VRage;
using VRage.Collections;
using VRage.Game;
using VRage.Game.Components;
using VRage.Game.GUI.TextPanel;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ModAPI.Ingame.Utilities;
using VRage.Game.ObjectBuilders.Definitions;
using VRageMath;

namespace IngameScript
{
    partial class Program
    {
        public class Parser
        {
            public string[]      Tokens;
            public int           Pos;
                                 
            public List<Scope>   Scopes;
            public Scope         CurrentScope;

            public bool          HasNext       { get { return Pos+1 < Tokens.Length; } }
            public bool          NextIsKeyword { get { return Keywords.Contains(Next); } }

            public string        Next          { get { return Tokens[Pos]; } }
                                                
            public List<Command> Commands;



            public Parser(string[] tokens)
            {
                Tokens       = tokens;
                Pos          = 0;
                             
                Scopes       = new List<Scope>();
                Scopes.Add(new Scope());

                CurrentScope = Scopes[0];

                Commands     = new List<Command>();
            }



            public string Move()
            {
                if (Pos < Tokens.Length)
                    return Tokens[Pos++];

                else
                {
                    Pos++;
                    return "";
                }
            }



            public void EvalCommands()
            {
                foreach (var cmd in Commands)
                    cmd.Eval(this);
            }
        }



        public Parser Scan(string ssg)
        {
            ssg = ssg.Replace("\\\"", "\uFFFC"); // guard escaped quotes


            var tokens = new List<string>();


            var qparts = ssg.Split('\"');

            for (var i = 0; i < qparts.Length; i += 2)
            {
                qparts[i] = qparts[i]
                    .Replace("\n", " ")  // new lines and tabs
                    .Replace("\t", " "); // are white space only outside of quotes

                var sparts = qparts[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in sparts)
                    tokens.Add(part);

                if (i < qparts.Length - 1)                           // string literal after first quote
                    tokens.Add(qparts[i+1].Replace("\uFFFC", "\"")); // restore escaped quotes
            }


            return new Parser(tokens.ToArray());
        }



        public bool Parse(Parser parser)
        {
            var overflowProtect = 10;


            while (   parser.Pos < parser.Tokens.Length
                   && overflowProtect > 0)
            {
                if (parser.Next[0] == '#')
                    parser.CurrentScope.Color = ColorFromHex(parser.Move());

                else
                {
                    switch (parser.Next.ToUpper())
                    {
                        case SetDisplay .Keyword: if (!ParseSetDisplay (parser)) return false; break;
                        //case "sp":  if (!ParseSpace      (parse)) return false; break;

                        case DrawTexture.Keyword: if (!ParseDrawTexture(parser)) return false; break;

                        //case "fr":  if (!ParseFillRect   (parse)) return false; break;

                        //case "fe":  if (!ParseFillEllipse(parse)) return false; break;
                        //case "fc":  if (!ParseFillCircle (parse)) return false; break;

                        //case "lw":  if (!ParseLineWidth  (parse)) return false; break;
                        //case "dl":  if (!ParseDrawLine   (parse)) return false; break;

                        //case "ds":  if (!ParseDrawString (parse)) return false; break;

                        default: overflowProtect--; break;
                    }
                }
            }


            return true;
        }



        public Coord ParseCoord(Parser parser)
        {
            var token   = parser.Move();
            var percent = false;

            if (   token.Length > 0
                && token[token.Length-1] == '%')
            {
                percent = true;
                token   = token.Substring(0, token.Length-1);
            }

            var val = float.Parse(token);

            return new Coord(val, percent);
        }
    }
}
