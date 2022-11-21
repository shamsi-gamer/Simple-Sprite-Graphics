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

            public bool          HasNext           { get { return Pos + 1 < Tokens.Length; } }

            public bool          NextIsPunctuation { get { return Punctuation.Contains(Next); } }
            public bool          NextIsKeyword     { get { return Keywords   .Contains(Next); } }

            public bool          NextIsReserved    { get { return NextIsPunctuation
                                                               || NextIsKeyword; } }

            public string        Next              { get { return Tokens[Pos]; } }
         
            
            public List<Display> Displays;
            public List<Command> Commands;



            public Parser(string[] tokens)
            {
                Tokens       = tokens;
                Pos          = 0;
                             
                Scopes       = new List<Scope>();
                Scopes.Add(new Scope());

                CurrentScope = Scopes[0];

                Displays     = new List<Display>();
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



            public bool Match(string token)
            {
                if (   HasNext
                    && Next == token)
                { 
                    Move();
                    return true;
                }

                return false;
            }



            public bool AddCommand(Command cmd)
            {
                if (cmd == null)
                    return false;

                Commands.Add(cmd);

                return true;
            }



            public void EvalCommands()
            {
                foreach (var cmd in Commands)
                    cmd.Eval(this);

                foreach (var dsp in Displays)
                    dsp.FlushSprites();
            }
        }



        public void ParseCode()
        {
            logPanel.WriteText("");

            codeLength = Me.CustomData.Length;
            codeHash   = Me.CustomData.GetHashCode();

            var tokens = Scan(Me.CustomData);
            parser = Parse(tokens);
        }



        public List<string> Scan(string ssg)
        {
            ssg = ssg.Replace("\\\"", "\uFFFC"); // guard escaped quotes


            var tokens = new List<string>();


            var qparts = ssg.Split('\"');

            for (var i = 0; i < qparts.Length; i += 2)
            {
                qparts[i] = qparts[i]
                    .Replace("\r\n", " ")  // new lines and tabs
                    .Replace("\n",   " ")  // are white space
                    .Replace("\t",   " "); // only outside of quotes

                var sparts = qparts[i].Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var part in sparts)
                    tokens.Add(part);

                if (i < qparts.Length - 1) // string literal after first quote
                    tokens.Add(qparts[i+1].Replace("\uFFFC", "\"")); // restore escaped quotes
            }


            //foreach (var token in tokens)
            //    logPanel.WriteText("token = " + token + "\n", true);

            return tokens;
        }



        public Parser Parse(List<string> tokens)
        {
            var parser = new Parser(tokens.ToArray());


            var overflowProtect = 10;

            while (   parser.Pos < parser.Tokens.Length
                   && overflowProtect > 0)
            {

                logPanel.WriteText("next = " + parser.Next + "\n", true);
                switch (parser.Next.ToLower())
                {
                    case SetDisplay      .Keyword: ParseSetDisplay (parser); break;
                    
                    case SetColor        .Keyword: ParseSetColor     (parser); break;
                    //case SetFont         .Keyword: ParseSetFont      (parser); break;
                    //case SetFontSize     .Keyword: ParseSetFontSize  (parser); break;
                    //case SetTextAlignment.Keyword: ParseSetFontSize  (parser); break;
                    //case SetLineWidth    .Keyword: ParseSetLineWidth (parser); break;
                    case SetRotation     .Keyword: ParseSetRotation  (parser); break;

                    case DrawString      .Keyword: ParseDrawString   (parser); break;
                    case DrawTexture     .Keyword: ParseDrawTexture  (parser); break;
                    case FillRectangle   .Keyword: ParseFillRectangle(parser); break;
                    case DrawRectangle   .Keyword: ParseDrawRectangle(parser); break;
                    case FillEllipse     .Keyword: ParseFillEllipse  (parser); break;
                    case DrawLine        .Keyword: ParseDrawLine     (parser); break;


                    default: overflowProtect--; break;
                }
            }


            return parser;
        }



        public XCoord ParseXCoord(Parser parser) { var coord = ParseCoord(parser); return new XCoord(coord.Value, coord.Percent); }
        public YCoord ParseYCoord(Parser parser) { var coord = ParseCoord(parser); return new YCoord(coord.Value, coord.Percent); }
        public WCoord ParseWCoord(Parser parser) { var coord = ParseCoord(parser); return new WCoord(coord.Value, coord.Percent); }
        public HCoord ParseHCoord(Parser parser) { var coord = ParseCoord(parser); return new HCoord(coord.Value, coord.Percent); }



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
