using System;
using System.Collections.Generic;

#nullable disable
namespace ScintillaNet;

public class KeywordCollection : TopLevelHelper
{
    private Dictionary<string, string[]> _lexerKeywordListMap;
    private Dictionary<string, Dictionary<string, int>> _lexerStyleMap;
    private Dictionary<string, Lexer> _lexerAliasMap;
    private string[] _keywords = new string[9]
    {
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    "",
    ""
    };

    internal KeywordCollection(Scintilla scintilla)
      : base(scintilla)
    {
        this._lexerAliasMap = new Dictionary<string, Lexer>((IEqualityComparer<string>)StringComparer.OrdinalIgnoreCase);
        this._lexerAliasMap.Add("PL/M", Lexer.Plm);
        this._lexerAliasMap.Add("props", Lexer.Properties);
        this._lexerAliasMap.Add("inno", Lexer.InnoSetup);
        this._lexerAliasMap.Add("clarion", Lexer.Clw);
        this._lexerAliasMap.Add("clarionnocase", Lexer.ClwNoCase);
    }

    public string this[int keywordSet]
    {
        get => this._keywords[keywordSet];
        set
        {
            this._keywords[keywordSet] = value;
            this.NativeScintilla.SetKeywords(keywordSet, value);
        }
    }

    public string this[string keywordSetName]
    {
        get => this[this.getKeyowrdSetIndex(keywordSetName)];
        set => this[this.getKeyowrdSetIndex(keywordSetName)] = value;
    }

    private int getKeyowrdSetIndex(string name)
    {
        string lower = Scintilla.Lexing.Lexer.ToString().ToLower();

        if (_lexerKeywordListMap.ContainsKey(lower))
        {
            throw new ApplicationException($"lexer {lower} does not support named keyword lists");
        }


        string[] test = _lexerKeywordListMap[lower];
        int num = test[0].IndexOf(name, StringComparison.OrdinalIgnoreCase);

        //int num = _lexerKeywordListMap[lower].IndexOf((object)name);

        return num >= 0 ? num : throw new ArgumentException("Keyword Set name does not exist for lexer " + lower, "keywordSetName");
    }
}