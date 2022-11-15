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

            public string        Next { get { return Tokens[Pos]; } }
                                                
            public List<Command> Commands;



            public Parser(string ssg)
            {
                Tokens    = new string[] { };
                Pos       = 0;

                Scopes    = new List<Scope>();
                Scopes.Add(new Scope());

                CurrentScope = Scopes[0];

                Commands = new List<Command>();


            var tokens = ScanTokens(ssg);
            var parse  = new Parser();


            var overflowProtect = 10;

            while (parse.Pos < parse.Tokens.Length
                && overflowProtect > 0)
            { 
                if (parse.Next[0] == '#')
                    parse.Color = ColorFromHex(parse.Move());
                
                else
                { 
                    switch (parse.Next.ToLower())
                    {
                        case "dsp": if (!ParseDisplays   (parse)) return false; break;
                        //case "sp":  if (!ParseSpace      (parse)) return false; break;
                        
                        case "dt":  if (!ParseDrawTexture(parse)) return false; break;
                            
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
    }
}
