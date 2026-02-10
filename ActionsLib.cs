using System.Drawing;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    static class ActionsLib
    {

        public static string ActionEquateDisplay(string ActionName, string ActionArguments)
        {
            string Returnable = string.Empty;
            foreach (string Y in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                if (Y.StartsWith("DISPLAY "))
                {
                    Returnable = Y.Substring(8);
                    break;
                }
            }
            if (Returnable.Length == 0)
                return ActionName;
            foreach (string Y in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                for (int Z = 0, loopTo = (int)DS_Game_Maker.DSGMlib.HowManyChar(ActionArguments, ";"); Z <= loopTo; Z++)
                {
                    string Argument = DS_Game_Maker.DSGMlib.iGet(ActionArguments, (byte)Z, ";");
                    string StringThing = Argument;
                    if (StringThing == "1")
                        StringThing = "true";
                    if (StringThing == "0")
                        StringThing = "false";
                    if (StringThing == "<" | StringThing == ">" | StringThing == ">=" | StringThing == "<=" | StringThing == "==")
                    {
                        StringThing = DS_Game_Maker.ScriptsLib.ComparativeToString(StringThing);
                    }
                    string ScreenThing = Argument;
                    if (ScreenThing == "1")
                        ScreenThing = "Top Screen";
                    if (ScreenThing == "0")
                        ScreenThing = "Bottom Screen";
                    Returnable = Returnable.Replace("$" + (Z + 1).ToString() + "$", StringThing);
                    Returnable = Returnable.Replace("%" + (Z + 1).ToString() + "%", ScreenThing);
                    Returnable = Returnable.Replace("!" + (Z + 1).ToString() + "!", Argument);
                }
            }
            Returnable = Returnable.Replace("<com>", ",");
            return Returnable;
        }

        public static bool ActionIsConditional(object ActionName)
        {
            foreach (string X_ in File.ReadAllLines(Conversions.ToString(Operators.AddObject(Operators.AddObject(DS_Game_Maker.DSGMlib.AppPath + @"Actions\", ActionName), ".action"))))
            {
                string X = X_;
                if (X.StartsWith("CONDITION "))
                {
                    X = X.Substring("CONDITION".Length + 1);
                    return X == "1";
                }
            }
            return false;
        }

        public static string ActionGetIconPath(string ActionName, bool UseFullPath)
        {
            string Returnable = "Empty.png";
            foreach (string X in File.ReadAllLines(DS_Game_Maker.DSGMlib.AppPath + @"Actions\" + ActionName + ".action"))
            {
                if (X.StartsWith("ICON "))
                    Returnable = X.Substring(5);
            }
            if (!File.Exists(DS_Game_Maker.DSGMlib.AppPath + @"ActionIcons\" + Returnable))
                Returnable = "Empty.png";
            if (UseFullPath)
                Returnable = DS_Game_Maker.DSGMlib.AppPath + @"ActionIcons\" + Returnable;
            return Returnable;
        }

        public static Bitmap ActionGetIcon(string ActionName)
        {
            var TBMP = new Bitmap(32, 32);
            var TBMPGFX = Graphics.FromImage(TBMP);
            Bitmap TIcon = (Bitmap)DS_Game_Maker.DSGMlib.PathToImage(ActionGetIconPath(ActionName, true));
            if (TIcon.Width == 32)
            {
                TBMPGFX.DrawImageUnscaled(DS_Game_Maker.My.Resources.Resources.ActionBacker, 0, 0);
                TBMPGFX.DrawImageUnscaled(TIcon, new Point(0, 0));
            }
            else
            {
                if (ActionIsConditional(ActionName))
                {
                    TBMPGFX.DrawImageUnscaled(DS_Game_Maker.DSGMlib.ActionConditionalBG, 0, 0);
                }
                else
                {
                    TBMPGFX.DrawImageUnscaled(DS_Game_Maker.DSGMlib.ActionBG, 0, 0);
                }
                TBMPGFX.DrawImageUnscaled(TIcon, new Point(8, 8));
            }
            TBMPGFX.Dispose();
            TIcon.Dispose();
            return TBMP;
        }

    }
}