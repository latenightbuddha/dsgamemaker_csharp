using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DS_Game_Maker
{
    static class ScriptsLib
    {

        public static List<string> ApplyFinders = new List<string>();

        public static string ScriptParseFromContent(string ScriptName, string ScriptContent, string Arguments, string ArgumentTypes, bool IsPreview, bool IsLocal, bool IsC)
        {
            string FinalString = string.Empty;
            if (!IsLocal)
            {
                FinalString = "int " + ScriptName + "(";
                string ArgumentString = string.Empty;
                byte CommaCount = (byte)DS_Game_Maker.DSGMlib.HowManyChar(Arguments, ",");
                if (Arguments.Length > 0)
                {
                    for (byte i = 0, loopTo = CommaCount; i <= loopTo; i++)
                    {
                        string ArgumentType = ScriptsLib.GenerateCType(DS_Game_Maker.DSGMlib.iGet(ArgumentTypes, i, ","));
                        string ArgumentName = DS_Game_Maker.DSGMlib.iGet(Arguments, i, ",");
                        ArgumentString += ArgumentType + " ";
                        if (ArgumentType == "char")
                            ArgumentString += "*";
                        ArgumentString += ArgumentName;
                        ArgumentString += ", ";
                    }
                    ArgumentString = ArgumentString.Substring(0, ArgumentString.Length - 2);
                    FinalString += ArgumentString + ") {" + Constants.vbCrLf;
                }
                else
                {
                    FinalString += "void) {" + Constants.vbCrLf;
                }
            }
            if (IsC)
            {
                foreach (string P in DS_Game_Maker.DSGMlib.StringToLines(ScriptContent))
                {
                    if (P.Length == 0)
                        continue;
                    FinalString = Conversions.ToString(FinalString + Operators.AddObject(Operators.AddObject(DS_Game_Maker.DSGMlib.MakeSpaces((byte)2), P), Constants.vbCrLf));
                }
                FinalString += "  return 0;" + Constants.vbCrLf;
                FinalString += "}";
                return FinalString;
            }
            byte IndentChange = 0;
            byte CurrentIndent = (byte)(IsLocal ? 0 : 1);
            var StringVariables = new List<string>();
            foreach (string X_ in DS_Game_Maker.DSGMlib.StringToLines(ScriptContent))
            {
                string X = X_;
                if (X.Length == 0)
                    continue;
                X = X.Replace(Conversions.ToString(ControlChars.Tab), " ");
                IndentChange = 0;
                for (byte i = 0; i <= 200; i++)
                {
                    if (!(X[0].ToString() == " "))
                        break;
                    X = X.Substring(1);
                }
                while (X.EndsWith(" "))
                    X = X.Substring(0, X.Length - 1);
                string L = X.ToLower();
                string Output = string.Empty;
                string TS = string.Empty;
                if (IsLocal)
                {
                    foreach (string P in ApplyFinders)
                    {
                        string NoBrackets = P.Substring(1);
                        NoBrackets = NoBrackets.Substring(0, NoBrackets.Length - 1);
                        X = X.Replace(P, "Instances[DAppliesTo]." + NoBrackets);
                    }
                    X = X.Replace("[Me]", "DAppliesTo");
                }
                if (L.StartsWith("dim "))
                {
                    // If IsLocal Then Continue For
                    X = X.Substring(4);
                    L = L.Substring(4);
                    if (L.Contains(" as "))
                    {
                        string VariableName = X.Substring(0, L.IndexOf(" as "));
                        // MsgError("name is """ + VariableName + """")
                        string VariableType = X.Substring(L.IndexOf(" as ") + 4);
                        // MsgError("type is """ + VariableType + """")
                        string LVariableType = L.Substring(L.IndexOf(" as ") + 4);
                        // MsgError("lo type is """ + VariableType + """")
                        if (VariableType.Contains(" = "))
                        {
                            VariableType = VariableType.Substring(0, VariableType.LastIndexOf(" = "));
                            LVariableType = LVariableType.Substring(0, LVariableType.LastIndexOf(" = "));
                        }
                        string CVariableType = GenerateCType(LVariableType);
                        // MsgError("c var type is " + CVariableType)
                        // If CVariableType = "pie" Then CVariableType = VariableType
                        string VariableValue = string.Empty;
                        if (X.Contains(VariableType + " = "))
                        {
                            VariableValue = X.Substring(X.LastIndexOf(" = ") + 3);
                            if (LVariableType == "boolean")
                            {
                                if (VariableValue.ToLower() == "yes")
                                    VariableValue = "true";
                                if (VariableValue.ToLower() == "no")
                                    VariableValue = "false";
                            }
                        }
                        TS = CVariableType + " " + VariableName;
                        if (LVariableType == "string")
                            TS += "[128]";
                        if (VariableValue.Length > 0 & !(VariableType == "string"))
                        {
                            TS += " = ";
                            TS += VariableValue;
                        }
                        TS += ";";
                        if (LVariableType == "string")
                        {
                            StringVariables.Add(VariableName);
                            if (VariableValue.Length == 0)
                            {
                                TS += " strcpy(" + VariableName + ", \"\");";
                            }
                            else
                            {
                                TS += " strcpy(" + VariableName + ", " + VariableValue + ");";
                            }
                        }
                        Output = TS;
                    }
                    else
                    {
                        TS = "int " + X + ";";
                        Output = TS;
                    }
                }
                else if (X.StartsWith("'"))
                {
                    Output = "// " + X.Substring(1);
                }
                else if (L.StartsWith("rem "))
                {
                    Output = "// " + X.Substring(4);
                }
                else if (L.StartsWith("return "))
                {
                    Output = "return " + X.Substring("return ".Length) + ";";
                }
                else if (L.StartsWith("for "))
                {
                    X = X.Substring(4);
                    L = L.Substring(4);
                    string VariableName = X.Substring(0, X.IndexOf(" = "));
                    string LVariableName = L.Substring(0, L.IndexOf(" = "));
                    string DFrom = X.Substring(VariableName.Length + 3);
                    string LDFrom = L.Substring(LVariableName.Length + 3);
                    DFrom = DFrom.Substring(0, LDFrom.IndexOf(" to "));
                    string DTo = X.Substring(L.LastIndexOf(" to ") + 4);
                    // Dim LDTo As String = L.Substring(L.LastIndexOf(" to ") + 4)
                    Output = "for (" + VariableName + " = " + DFrom + "; " + VariableName;
                    Output += " < (" + DTo + " + 1); " + VariableName + "++) {";
                    IndentChange = 2;
                }
                else if (L == "next" | L.StartsWith("next "))
                {
                    Output = "}";
                    IndentChange = 1;
                }
                else if (L.StartsWith("if "))
                {
                    X = X.Substring(3);
                    L = L.Substring(3);
                    bool IsString = false;
                    string Value = X;
                    if (Value.Contains(" = "))
                    {
                        Value = Value.Substring(Value.IndexOf(" = ") + 3);
                        if (Value.StartsWith("\"") & Value.EndsWith("\""))
                            IsString = true;
                    }
                    string DName = X;
                    if (DName.Contains(" = "))
                    {
                        DName = DName.Substring(0, DName.IndexOf(" = "));
                        if (StringVariables.Contains(DName))
                            IsString = true;
                    }
                    if (!IsString)
                    {
                        X = X.Replace(" = ", " == ");
                        X = X.Replace(" And ", " && ");
                        X = X.Replace(" Or ", " || ");
                    }
                    string Condition = X;
                    L = Condition.ToLower();
                    if (IsString)
                    {
                        if (L.StartsWith("not "))
                        {
                            X = X.Substring(4);
                            L = L.Substring(4);
                            Condition = "!(strcmp(" + DName + ", " + Value + ") == 0)";
                        }
                        else
                        {
                            Condition = "strcmp(" + DName + ", " + Value + ") == 0";
                        }
                    }
                    else if (L.StartsWith("not "))
                    {
                        Condition = Condition.Substring(4);
                        Output = "if (!(" + Condition + ")) {";
                    }
                    else
                    {
                        Output = "if (" + Condition + ") {";
                    }
                    IndentChange = 2;
                }
                else if (L == "else")
                {
                    Output = "} else {";
                }
                else if (L == "end if")
                {
                    Output = "}";
                    IndentChange = 1;
                }
                // ElseIf X.StartsWith("|") Then
                // X = X.Substring(1)
                // If X.StartsWith(" ") Then
                // For i As Byte = 0 To 100
                // If Not X.Substring(0, 1) = " " Then Exit For
                // X = X.Substring(1)
                // Next
                // End If
                // Output = X
                else if (X.Contains(" = "))
                {
                    if (!(X.Substring(X.IndexOf(" "), 3) == " = "))
                        continue;
                    string VariableName = X.Substring(0, X.IndexOf(" "));
                    string VariableValue = X.Substring(X.IndexOf(" = ") + 3);
                    if (VariableValue.ToLower() == "true" | VariableValue.ToLower() == "false")
                        VariableValue = VariableValue.ToLower();
                    bool IsString = false;
                    if (VariableValue.StartsWith("\"") & VariableValue.EndsWith("\""))
                        IsString = true;
                    if (StringVariables.Contains(VariableName))
                        IsString = true;
                    if (IsString)
                    {
                        if (VariableName.Contains("."))
                        {
                            Output = VariableName + " = \"\"; strcpy(" + VariableName + ", " + VariableValue + ");";
                        }
                        else
                        {
                            Output = " strcpy(" + VariableName + ", " + VariableValue + ");";
                        }
                    }
                    else
                    {
                        Output = VariableName + " = " + VariableValue + ";";
                    }
                }
                if (IndentChange == 1)
                {
                    if (CurrentIndent > 1)
                    {
                        for (int i = 0, loopTo1 = CurrentIndent - 2; i <= loopTo1; i++)
                            FinalString += "  ";
                    }
                    if (CurrentIndent > 0)
                    {
                        CurrentIndent = (byte)(CurrentIndent - 1);
                    }
                }
                else if (CurrentIndent > 0)
                {
                    for (int i = 0, loopTo2 = CurrentIndent - 1; i <= loopTo2; i++)
                        FinalString += "  ";
                }
                if (Output.Length == 0)
                {
                    if (X.EndsWith(";"))
                    {
                        Output = X;
                    }
                    else
                    {
                        Output = X + ";";
                    }
                }
                FinalString += Output + Constants.vbCrLf;
                if (IndentChange == 2)
                    CurrentIndent = (byte)(CurrentIndent + 1);
            }
            if (!IsLocal)
                FinalString += "  return 0;" + Constants.vbCrLf;
            if (!IsLocal)
                FinalString += "}";
            // If FinalString.Length > 0 Then
            // FinalString = FinalString.Substring(0, FinalString.Length - 2)
            // End If
            // MsgError("""" + FinalString + """")5
            return FinalString;
        }

        public static string ScriptParse(string ScriptName, bool IsC)
        {
            string ArgumentsString = string.Empty;
            string ArgumentTypesString = string.Empty;
            foreach (string X_ in DS_Game_Maker.DSGMlib.GetXDSFilter("SCRIPTARG " + ScriptName + ","))
            {
                string X = X_;
                X = X.Substring(10);
                ArgumentsString += DS_Game_Maker.DSGMlib.iGet(X, (byte)1, ",") + ",";
                ArgumentTypesString += DS_Game_Maker.DSGMlib.iGet(X, (byte)2, ",") + ",";
            }
            if (ArgumentsString.Length > 0)
            {
                ArgumentsString = ArgumentsString.Substring(0, ArgumentsString.Length - 1);
                ArgumentTypesString = ArgumentTypesString.Substring(0, ArgumentTypesString.Length - 1);
            }
            return ScriptsLib.ScriptParseFromContent(ScriptName, DS_Game_Maker.DSGMlib.PathToString(DS_Game_Maker.SessionsLib.SessionPath + @"Scripts\" + ScriptName + ".dbas"), ArgumentsString, ArgumentTypesString, false, false, IsC);
        }

        public static string ActionTypeToString(byte ActionType)
        {
            if (ActionType == 0)
                return "Objects";
            if (ActionType == 1)
                return "Media";
            if (ActionType == 2)
                return "Control";
            if (ActionType == 3)
                return "Display";
            if (ActionType == 4)
                return "Score";
            if (ActionType == 5)
                return "Advanced";
            return "Extra";
        }

        public static byte ActionStringToType(object ActionString)
        {
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Objects", false)))
                return 0;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Media", false)))
                return 1;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Control", false)))
                return 2;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Display", false)))
                return 3;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Score", false)))
                return 4;
            if (Conversions.ToBoolean(Operators.ConditionalCompareObjectEqual(ActionString, "Advanced", false)))
                return 5;
            return 5;
        }

        public static string ArgumentTypeToString(byte Type)
        {
            if (Type == 1)
                return "Screen";
            if (Type == 2)
                return "TrueFalse";
            if (Type == 3)
                return "Variable";
            if (Type == 4)
                return "Object";
            if (Type == 5)
                return "Background";
            if (Type == 6)
                return "Sound";
            if (Type == 7)
                return "Room";
            if (Type == 8)
                return "Path";
            if (Type == 9)
                return "Script";
            if (Type == 10)
                return "Comparative";
            if (Type == 11)
                return "Font";
            if (Type == 12)
                return "Unrestrictive";
            if (Type == 13)
                return "Sprite";
            if (Type == 14)
                return "CScript";
            return "Undefined";
        }

        public static byte ArgumentStringToType(string Type)
        {
            if (Type == "Screen")
                return 1;
            if (Type == "TrueFalse")
                return 2;
            if (Type == "Variable")
                return 3;
            if (Type == "Object")
                return 4;
            if (Type == "Background")
                return 5;
            if (Type == "Sound")
                return 6;
            if (Type == "Room")
                return 7;
            if (Type == "Path")
                return 8;
            if (Type == "Script")
                return 9;
            if (Type == "Comparative")
                return 10;
            if (Type == "Font")
                return 11;
            if (Type == "Unrestrictive")
                return 12;
            if (Type == "Sprite")
                return 13;
            if (Type == "CScript")
                return 14;
            if (Type == "Array")
                return 15;
            if (Type == "Structure")
                return 16;
            return 0;
        }

        public static string MainClassTypeToString(byte TheMainClass)
        {
            if (TheMainClass == 0)
                return "Unused";
            if (TheMainClass == 1)
                return "Create";
            if (TheMainClass == 2)
                return "Button Press";
            if (TheMainClass == 3)
                return "Button Held";
            if (TheMainClass == 4)
                return "Button Released";
            if (TheMainClass == 5)
                return "Touch";
            if (TheMainClass == 6)
                return "Collision";
            if (TheMainClass == 7)
                return "Step";
            if (TheMainClass == 8)
                return "Other";
            return "Unused";
        }

        public static byte MainClassStringToType(string TheMainClass)
        {
            if (TheMainClass == "Unused")
                return 0;
            if (TheMainClass == "Create")
                return 1;
            if (TheMainClass == "Button Press")
                return 2;
            if (TheMainClass == "Button Held")
                return 3;
            if (TheMainClass == "Button Released")
                return 4;
            if (TheMainClass == "Touch")
                return 5;
            if (TheMainClass == "Collision")
                return 6;
            if (TheMainClass == "Step")
                return 7;
            if (TheMainClass == "Other")
                return 8;
            return 0;
        }

        public static List<string> VariableTypes = new List<string>();

        public static string GenerateCType(string HumanVT)
        {
            switch (HumanVT.ToLower() ?? "")
            {
                case "integer":
                    {
                        return "int";
                    }
                case "boolean":
                    {
                        return "bool";
                    }
                case "float":
                    {
                        return "float";
                    }
                case "unsigned byte":
                    {
                        return "u8";
                    }
                case "signed byte":
                    {
                        return "s8";
                    }
                case "string":
                    {
                        return "char";
                    }
            }
            return HumanVT;
        }

        // Function ScriptArgumentStringToType(ByVal Type As String) As Byte
        // If Type = "Integer" Then Return 0
        // If Type = "Boolean" Then Return 1
        // If Type = "Float" Then Return 2
        // If Type = "Signed Byte" Then Return 3
        // If Type = "Unsigned Byte" Then Return 4
        // If Type = "String" Then Return 5
        // Return 0
        // End Function

        public static string StringToComparative(string InputString)
        {
            if (InputString == "Less than")
                return "<";
            if (InputString == "Greater than")
                return ">";
            if (InputString == "Equal to")
                return "==";
            if (InputString == "Less than or Equal to")
                return "<=";
            if (InputString == "Greater than or Equal to")
                return ">=";
            if (InputString == "Not Equal to")
                return "!=";
            return "==";
        }

        public static string ComparativeToString(string InputString)
        {
            if (InputString == "<")
                return "Less than";
            if (InputString == ">")
                return "Greater than";
            if (InputString == "==")
                return "Equal to";
            if (InputString == "<=")
                return "Less than or Equal to";
            if (InputString == ">=")
                return "Greater than or Equal to";
            if (InputString == "!=")
                return "Not equal to";
            return "Equal to";
        }

    }
}