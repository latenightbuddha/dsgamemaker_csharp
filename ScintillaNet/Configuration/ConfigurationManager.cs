using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace ScintillaNet.Configuration;

[TypeConverter(typeof (ExpandableObjectConverter))]
public class ConfigurationManager : TopLevelHelper
{
  private bool _useXmlReader = true;
  private string _language;
  private bool _isBuiltInEnabled = true;
  private bool _isUserEnabled = true;
  private string _customLocation;
  private ConfigurationLoadOrder _loadOrder;
  private bool _clearKeyBindings;
  private bool _clearStyles;
  private bool _clearIndicators;
  private bool _clearSnippets;
  private bool _clearMargins;
  private bool _clearMarkers;
  private string _appDataFolder;
  private string _userFolder;

  [DefaultValue(true)]
  public bool UseXmlReader
  {
    get => this._useXmlReader;
    set => this._useXmlReader = value;
  }

  internal ConfigurationManager(Scintilla scintilla)
    : base(scintilla)
  {
  }

  internal bool ShouldSerialize()
  {
    return this.ShouldSerializeClearKeyBindings() || this.ShouldSerializeClearMargins() || this.ShouldSerializeClearSnippets() || this.ShouldSerializeClearStyles() || this.ShouldSerializeCustomLocation() || this.ShouldSerializeIsBuiltInEnabled() || this.ShouldSerializeIsUserEnabled() || this.ShouldSerializeLanguage() || this.ShouldSerializeLoadOrder();
  }

  protected internal override void Initialize()
  {
    if (this._language == null)
      return;
    this.Configure();
  }

  public string Language
  {
    get => this._language;
    set
    {
      this._language = value;
      if (this.Scintilla.IsDesignMode)
        return;
      this.Configure();
    }
  }

  private bool ShouldSerializeLanguage() => !string.IsNullOrEmpty(this._language);

  private void ResetLanguage() => this._language = (string) null;

  public bool IsBuiltInEnabled
  {
    get => this._isBuiltInEnabled;
    set => this._isBuiltInEnabled = value;
  }

  private bool ShouldSerializeIsBuiltInEnabled() => !this._isBuiltInEnabled;

  private void ResetIsBuiltInEnabled() => this._isBuiltInEnabled = true;

  public bool IsUserEnabled
  {
    get => this._isUserEnabled;
    set => this._isUserEnabled = value;
  }

  private bool ShouldSerializeIsUserEnabled() => !this._isUserEnabled;

  private void ResetIsUserEnabled() => this._isUserEnabled = true;

  public string CustomLocation
  {
    get => this._customLocation;
    set => this._customLocation = value;
  }

  private bool ShouldSerializeCustomLocation() => !string.IsNullOrEmpty(this._customLocation);

  private void ResetCustomLocation() => this._customLocation = string.Empty;

  public ConfigurationLoadOrder LoadOrder
  {
    get => this._loadOrder;
    set => this._loadOrder = value;
  }

  private bool ShouldSerializeLoadOrder()
  {
    return this._loadOrder != ConfigurationLoadOrder.BuiltInCustomUser;
  }

  private void ResetLoadOrder() => this._loadOrder = ConfigurationLoadOrder.BuiltInCustomUser;

  public bool ClearKeyBindings
  {
    get => this._clearKeyBindings;
    set => this._clearKeyBindings = value;
  }

  private bool ShouldSerializeClearKeyBindings() => this._clearKeyBindings;

  private void ResetClearKeyBindings() => this._clearKeyBindings = false;

  public bool ClearStyles
  {
    get => this._clearStyles;
    set => this._clearStyles = value;
  }

  private bool ShouldSerializeClearStyles() => this._clearStyles;

  private void ResetClearStyles() => this._clearStyles = false;

  public bool ClearIndicators
  {
    get => this._clearIndicators;
    set => this._clearIndicators = value;
  }

  private bool ShouldSerializeClearIndicators() => this._clearIndicators;

  private void ResetClearIndicators() => this._clearIndicators = false;

  public bool ClearSnippets
  {
    get => this._clearSnippets;
    set => this._clearSnippets = value;
  }

  private bool ShouldSerializeClearSnippets() => this._clearSnippets;

  private void ResetClearSnippets() => this._clearSnippets = false;

  public bool ClearMargins
  {
    get => this._clearMargins;
    set => this._clearMargins = value;
  }

  private bool ShouldSerializeClearMargins() => this._clearMargins;

  private void ResetClearMargins() => this._clearMargins = true;

  public bool ClearMarkers
  {
    get => this._clearMarkers;
    set => this._clearMarkers = value;
  }

  private bool ShouldSerializeClearMarkers() => this._clearMarkers;

  private void ResetClearMarkers() => this._clearMarkers = false;

  private string userFolder
  {
    get
    {
      if (this._appDataFolder == null)
        this._appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
      if (this._userFolder == null)
      {
        Version version = this.GetType().Assembly.GetName().Version;
        this._userFolder = Path.Combine(Path.Combine(this._appDataFolder, "ScintillaNET"), $"{version.Major.ToString()}.{version.Minor.ToString()}");
      }
      return this._userFolder;
    }
  }

  public void Configure()
  {
    if (this.Scintilla.IsDesignMode || this.Scintilla.IsInitializing)
      return;
    ScintillaNet.Configuration.Configuration configuration1 = (ScintillaNet.Configuration.Configuration) null;
    ScintillaNet.Configuration.Configuration configuration2 = (ScintillaNet.Configuration.Configuration) null;
    ScintillaNet.Configuration.Configuration configuration3 = (ScintillaNet.Configuration.Configuration) null;
    ScintillaNet.Configuration.Configuration configuration4 = (ScintillaNet.Configuration.Configuration) null;
    ScintillaNet.Configuration.Configuration configuration5 = (ScintillaNet.Configuration.Configuration) null;
    ScintillaNet.Configuration.Configuration configuration6 = (ScintillaNet.Configuration.Configuration) null;
    if (this._isBuiltInEnabled)
    {
      using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream("ScintillaNet.Configuration.Builtin.default.xml"))
        configuration1 = new ScintillaNet.Configuration.Configuration(manifestResourceStream, "default", this._useXmlReader);
      if (!string.IsNullOrEmpty(this._language))
      {
        using (Stream manifestResourceStream = this.GetType().Assembly.GetManifestResourceStream($"ScintillaNet.Configuration.Builtin.{this._language}.xml"))
        {
          if (manifestResourceStream != null)
            configuration2 = new ScintillaNet.Configuration.Configuration(manifestResourceStream, this._language, this._useXmlReader);
        }
      }
    }
    if (this._isUserEnabled)
    {
      string str1 = Path.Combine(this.userFolder, "default.xml");
      if (File.Exists(str1))
        configuration5 = new ScintillaNet.Configuration.Configuration(str1, "default", this._useXmlReader);
      if (!string.IsNullOrEmpty(this._language))
      {
        string str2 = Path.Combine(this.userFolder, this._language + ".xml");
        if (File.Exists(str2))
          configuration6 = new ScintillaNet.Configuration.Configuration(str2, this._language, this._useXmlReader);
      }
    }
    if (!string.IsNullOrEmpty(this._customLocation))
    {
      string customConfigPath1 = this.getCustomConfigPath("default");
      string customConfigPath2 = this.getCustomConfigPath(this._language);
      if (!string.IsNullOrEmpty(customConfigPath1))
        configuration3 = new ScintillaNet.Configuration.Configuration(customConfigPath1, "default", this._useXmlReader);
      if (string.IsNullOrEmpty(customConfigPath2))
        throw new FileNotFoundException("Could not find the custom configuration file.", this._customLocation);
      configuration4 = new ScintillaNet.Configuration.Configuration(customConfigPath2, this._language, this._useXmlReader);
    }
    List<ScintillaNet.Configuration.Configuration> configList = new List<ScintillaNet.Configuration.Configuration>();
    if (this._loadOrder == ConfigurationLoadOrder.BuiltInCustomUser)
    {
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
    }
    else if (this._loadOrder == ConfigurationLoadOrder.BuiltInUserCustom)
    {
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
    }
    else if (this._loadOrder == ConfigurationLoadOrder.CustomBuiltInUser)
    {
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
    }
    else if (this._loadOrder == ConfigurationLoadOrder.CustomUserBuiltIn)
    {
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
    }
    else if (this._loadOrder == ConfigurationLoadOrder.UserBuiltInCustom)
    {
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
    }
    else if (this._loadOrder == ConfigurationLoadOrder.UserCustomBuiltIn)
    {
      if (configuration5 != null && configuration5.HasData)
        configList.Add(configuration5);
      if (configuration6 != null && configuration6.HasData)
        configList.Add(configuration6);
      if (configuration3 != null && configuration3.HasData)
        configList.Add(configuration3);
      if (configuration4 != null && configuration4.HasData)
        configList.Add(configuration4);
      if (configuration1 != null && configuration1.HasData)
        configList.Add(configuration1);
      if (configuration2 != null && configuration2.HasData)
        configList.Add(configuration2);
    }
    this.Configure(configList);
  }

  private string getCustomConfigPath(string language)
  {
    string customConfigPath = language;
    if (!string.IsNullOrEmpty(language) && !File.Exists(customConfigPath))
    {
      customConfigPath = Path.Combine(this._customLocation, language + ".xml");
      if (!File.Exists(customConfigPath))
      {
        customConfigPath = Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName, this._customLocation);
        if (!File.Exists(customConfigPath))
        {
          customConfigPath = Path.Combine(customConfigPath, language + ".xml");
          if (!File.Exists(customConfigPath))
            return (string) null;
        }
      }
    }
    return customConfigPath;
  }

  public void Configure(ScintillaNet.Configuration.Configuration config)
  {
    this.Configure(new List<ScintillaNet.Configuration.Configuration>((IEnumerable<ScintillaNet.Configuration.Configuration>) new ScintillaNet.Configuration.Configuration[1]
    {
      config
    }));
  }

  internal void Configure(List<ScintillaNet.Configuration.Configuration> configList)
  {
    bool? nullable1 = new bool?();
    int? nullable2 = new int?();
    Color empty = Color.Empty;
    char? nullable3 = new char?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_AutoHide.HasValue)
        nullable1 = config.AutoComplete_AutoHide;
    }
    if (nullable1.HasValue)
      this.Scintilla.AutoComplete.AutoHide = nullable1.Value;
    nullable1 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_AutomaticLengthEntered.HasValue)
        nullable1 = config.AutoComplete_AutomaticLengthEntered;
    }
    if (nullable1.HasValue)
      this.Scintilla.AutoComplete.AutomaticLengthEntered = nullable1.Value;
    nullable1 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_CancelAtStart.HasValue)
        nullable1 = config.AutoComplete_CancelAtStart;
    }
    if (nullable1.HasValue)
      this.Scintilla.AutoComplete.CancelAtStart = nullable1.Value;
    nullable1 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_DropRestOfWord.HasValue)
        nullable1 = config.AutoComplete_DropRestOfWord;
    }
    if (nullable1.HasValue)
      this.Scintilla.AutoComplete.DropRestOfWord = nullable1.Value;
    string str1 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_FillUpCharacters != null)
        str1 = config.AutoComplete_FillUpCharacters;
    }
    if (str1 != null)
      this.Scintilla.AutoComplete.FillUpCharacters = str1;
    char? nullable4 = new char?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      char? completeImageSeperator = config.AutoComplete_ImageSeperator;
      int? nullable5;
      int? nullable6;
      if (!completeImageSeperator.HasValue)
      {
        nullable5 = new int?();
        nullable6 = nullable5;
      }
      else
        nullable6 = new int?((int) completeImageSeperator.GetValueOrDefault());
      nullable5 = nullable6;
      if (nullable5.HasValue)
        nullable4 = config.AutoComplete_ImageSeperator;
    }
    char? nullable7 = nullable4;
    if ((nullable7.HasValue ? new int?((int) nullable7.GetValueOrDefault()) : new int?()).HasValue)
      this.Scintilla.AutoComplete.ImageSeparator = nullable4.Value;
    bool? nullable8 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_IsCaseSensitive.HasValue)
        nullable8 = config.AutoComplete_IsCaseSensitive;
    }
    if (nullable8.HasValue)
      this.Scintilla.AutoComplete.IsCaseSensitive = nullable8.Value;
    char? nullable9 = new char?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_ListSeperator.HasValue)
        nullable9 = config.AutoComplete_ListSeperator;
    }
    if (nullable9.HasValue)
      this.Scintilla.AutoComplete.ListSeparator = nullable9.Value;
    string str2 = this.Scintilla.AutoComplete.ListSeparator.ToString();
    string str3 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_List != null)
      {
        bool? completeListInherits = config.AutoComplete_ListInherits;
        if (completeListInherits.HasValue)
        {
          completeListInherits = config.AutoComplete_ListInherits;
          if (!completeListInherits.Value && !string.IsNullOrEmpty(str3))
          {
            str3 = str3 + str2 + config.AutoComplete_List;
            continue;
          }
        }
        str3 = config.AutoComplete_List;
      }
    }
    if (str3 != null)
      this.Scintilla.AutoComplete.ListString = str3;
    int? nullable10 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_MaxHeight.HasValue)
        nullable10 = config.AutoComplete_MaxHeight;
    }
    if (nullable10.HasValue)
      this.Scintilla.AutoComplete.MaxHeight = nullable10.Value;
    nullable10 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_MaxWidth.HasValue)
        nullable10 = config.AutoComplete_MaxWidth;
    }
    if (nullable10.HasValue)
      this.Scintilla.AutoComplete.MaxWidth = nullable10.Value;
    bool? nullable11 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_SingleLineAccept.HasValue)
        nullable11 = config.AutoComplete_SingleLineAccept;
    }
    if (nullable11.HasValue)
      this.Scintilla.AutoComplete.SingleLineAccept = nullable11.Value;
    string str4 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.AutoComplete_StopCharacters != null)
        str4 = config.AutoComplete_StopCharacters;
    }
    if (str4 != null)
      this.Scintilla.AutoComplete.StopCharacters = str4;
    Color color1 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.CallTip_BackColor != Color.Empty)
        color1 = config.CallTip_BackColor;
    }
    if (color1 != Color.Empty)
      this.Scintilla.CallTip.BackColor = color1;
    Color color2 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.CallTip_ForeColor != Color.Empty)
        color2 = config.CallTip_ForeColor;
    }
    if (color2 != Color.Empty)
      this.Scintilla.CallTip.ForeColor = color2;
    Color color3 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.CallTip_HighlightTextColor != Color.Empty)
        color3 = config.CallTip_HighlightTextColor;
    }
    if (color3 != Color.Empty)
      this.Scintilla.CallTip.HighlightTextColor = color3;
    int? nullable12 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_BlinkRate.HasValue)
        nullable12 = config.Caret_BlinkRate;
    }
    if (nullable12.HasValue)
      this.Scintilla.Caret.BlinkRate = nullable12.Value;
    Color color4 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_Color != Color.Empty)
        color4 = config.Caret_Color;
    }
    if (color4 != Color.Empty)
      this.Scintilla.Caret.Color = color4;
    int? nullable13 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_CurrentLineBackgroundAlpha.HasValue)
        nullable13 = config.Caret_CurrentLineBackgroundAlpha;
    }
    if (nullable13.HasValue)
      this.Scintilla.Caret.CurrentLineBackgroundAlpha = nullable13.Value;
    Color color5 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_CurrentLineBackgroundColor != Color.Empty)
        color5 = config.Caret_CurrentLineBackgroundColor;
    }
    if (color5 != Color.Empty)
      this.Scintilla.Caret.CurrentLineBackgroundColor = color5;
    bool? nullable14 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_HighlightCurrentLine.HasValue)
        nullable14 = config.Caret_HighlightCurrentLine;
    }
    if (nullable14.HasValue)
      this.Scintilla.Caret.HighlightCurrentLine = nullable14.Value;
    nullable14 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_IsSticky.HasValue)
        nullable14 = config.Caret_IsSticky;
    }
    if (nullable14.HasValue)
      this.Scintilla.Caret.IsSticky = nullable14.Value;
    CaretStyle? nullable15 = new CaretStyle?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_Style.HasValue)
        nullable15 = config.Caret_Style;
    }
    if (nullable15.HasValue)
      this.Scintilla.Caret.Style = nullable15.Value;
    int? nullable16 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Caret_Width.HasValue)
        nullable16 = config.Caret_Width;
    }
    if (nullable16.HasValue)
      this.Scintilla.Caret.Width = nullable16.Value;
    bool? nullable17 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Clipboard_ConvertEndOfLineOnPaste.HasValue)
        nullable17 = config.Clipboard_ConvertEndOfLineOnPaste;
    }
    if (nullable17.HasValue)
      this.Scintilla.Clipboard.ConvertEndOfLineOnPaste = nullable17.Value;
    nullable17 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Commands_KeyBindingList.AllowDuplicateBindings.HasValue)
        nullable17 = config.Commands_KeyBindingList.AllowDuplicateBindings;
    }
    if (nullable17.HasValue)
      this.Scintilla.Commands.AllowDuplicateBindings = nullable17.Value;
    if (this._clearKeyBindings)
      this.Scintilla.Commands.RemoveAllBindings();
    CommandBindingConfigList bindingConfigList = new CommandBindingConfigList();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? inherit = config.Commands_KeyBindingList.Inherit;
      if (inherit.HasValue)
      {
        inherit = config.Commands_KeyBindingList.Inherit;
        if (!inherit.Value)
          bindingConfigList.Clear();
      }
      foreach (CommandBindingConfig commandsKeyBinding in (List<CommandBindingConfig>) config.Commands_KeyBindingList)
        bindingConfigList.Add(commandsKeyBinding);
    }
    foreach (CommandBindingConfig commandBindingConfig in (List<CommandBindingConfig>) bindingConfigList)
    {
      bool? replaceCurrent = commandBindingConfig.ReplaceCurrent;
      if (replaceCurrent.HasValue)
      {
        replaceCurrent = commandBindingConfig.ReplaceCurrent;
        if (replaceCurrent.Value)
          this.Scintilla.Commands.RemoveBinding(commandBindingConfig.KeyBinding.KeyCode, commandBindingConfig.KeyBinding.Modifiers);
      }
      this.Scintilla.Commands.AddBinding(commandBindingConfig.KeyBinding.KeyCode, commandBindingConfig.KeyBinding.Modifiers, commandBindingConfig.BindableCommand);
    }
    string str5 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.DropMarkers_SharedStackName != null)
        str5 = config.DropMarkers_SharedStackName;
    }
    if (str5 != null)
      this.Scintilla.DropMarkers.SharedStackName = str5;
    bool? nullable18 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.EndOfLine_ConvertOnPaste.HasValue)
        nullable18 = config.EndOfLine_ConvertOnPaste;
    }
    if (nullable18.HasValue)
      this.Scintilla.EndOfLine.ConvertOnPaste = nullable18.Value;
    nullable18 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.EndOfLine_IsVisisble.HasValue)
        nullable18 = config.EndOfLine_IsVisisble;
    }
    if (nullable18.HasValue)
      this.Scintilla.EndOfLine.IsVisible = nullable18.Value;
    EndOfLineMode? nullable19 = new EndOfLineMode?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.EndOfLine_Mode.HasValue)
        nullable19 = config.EndOfLine_Mode;
    }
    if (nullable19.HasValue)
      this.Scintilla.EndOfLine.Mode = nullable19.Value;
    FoldFlag? nullable20 = new FoldFlag?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Folding_Flags.HasValue)
        nullable20 = config.Folding_Flags;
    }
    if (nullable20.HasValue)
      this.Scintilla.Folding.Flags = nullable20.Value;
    bool? nullable21 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Folding_IsEnabled.HasValue)
        nullable21 = config.Folding_IsEnabled;
    }
    if (nullable21.HasValue)
      this.Scintilla.Folding.IsEnabled = nullable21.Value;
    nullable21 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Folding_UseCompactFolding.HasValue)
        nullable21 = config.Folding_UseCompactFolding;
    }
    if (nullable21.HasValue)
      this.Scintilla.Folding.UseCompactFolding = nullable21.Value;
    Color color6 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_ActiveBackColor != Color.Empty)
        color6 = config.Hotspot_ActiveBackColor;
    }
    if (color6 != Color.Empty)
      this.Scintilla.HotspotStyle.ActiveBackColor = color6;
    Color color7 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_ActiveForeColor != Color.Empty)
        color7 = config.Hotspot_ActiveForeColor;
    }
    if (color7 != Color.Empty)
      this.Scintilla.HotspotStyle.ActiveForeColor = color7;
    bool? nullable22 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_ActiveUnderline.HasValue)
        nullable22 = config.Hotspot_ActiveUnderline;
    }
    if (nullable22.HasValue)
      this.Scintilla.HotspotStyle.ActiveUnderline = nullable22.Value;
    nullable22 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_SingleLine.HasValue)
        nullable22 = config.Hotspot_SingleLine;
    }
    if (nullable22.HasValue)
      this.Scintilla.HotspotStyle.SingleLine = nullable22.Value;
    nullable22 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_UseActiveBackColor.HasValue)
        nullable22 = config.Hotspot_UseActiveBackColor;
    }
    if (nullable22.HasValue)
      this.Scintilla.HotspotStyle.UseActiveBackColor = nullable22.Value;
    nullable22 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Hotspot_UseActiveForeColor.HasValue)
        nullable22 = config.Hotspot_UseActiveForeColor;
    }
    if (nullable22.HasValue)
      this.Scintilla.HotspotStyle.UseActiveForeColor = nullable22.Value;
    nullable22 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_BackspaceUnindents.HasValue)
        nullable22 = config.Indentation_BackspaceUnindents;
    }
    if (nullable22.HasValue)
      this.Scintilla.Indentation.BackspaceUnindents = nullable22.Value;
    int? nullable23 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_IndentWidth.HasValue)
        nullable23 = config.Indentation_IndentWidth;
    }
    if (nullable23.HasValue)
      this.Scintilla.Indentation.IndentWidth = nullable23.Value;
    bool? nullable24 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_ShowGuides.HasValue)
        nullable24 = config.Indentation_ShowGuides;
    }
    if (nullable24.HasValue)
      this.Scintilla.Indentation.ShowGuides = nullable24.Value;
    nullable24 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_TabIndents.HasValue)
        nullable24 = config.Indentation_TabIndents;
    }
    if (nullable24.HasValue)
      this.Scintilla.Indentation.TabIndents = nullable24.Value;
    int? nullable25 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_TabWidth.HasValue)
        nullable25 = config.Indentation_TabWidth;
    }
    if (nullable25.HasValue)
      this.Scintilla.Indentation.TabWidth = nullable25.Value;
    bool? nullable26 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_UseTabs.HasValue)
        nullable26 = config.Indentation_UseTabs;
    }
    if (nullable26.HasValue)
      this.Scintilla.Indentation.UseTabs = nullable26.Value;
    SmartIndent? nullable27 = new SmartIndent?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Indentation_SmartIndentType.HasValue)
        nullable27 = config.Indentation_SmartIndentType;
    }
    if (nullable27.HasValue)
      this.Scintilla.Indentation.SmartIndentType = nullable27.Value;
    if (this._clearIndicators)
      this.Scintilla.Indicators.Reset();
    IndicatorConfigList indicatorConfigList = new IndicatorConfigList();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? nullable28 = config.Indicator_List.Inherit;
      if (nullable28.HasValue)
      {
        nullable28 = config.Indicator_List.Inherit;
        if (!nullable28.Value)
          indicatorConfigList.Clear();
      }
      foreach (IndicatorConfig indicator in (Collection<IndicatorConfig>) config.Indicator_List)
      {
        if (indicatorConfigList.Contains(indicator.Number))
        {
          nullable28 = indicator.Inherit;
          if (nullable28.HasValue)
          {
            nullable28 = indicator.Inherit;
            if (nullable28.Value)
            {
              IndicatorConfig indicatorConfig = indicatorConfigList[indicator.Number];
              if (indicator.Color != Color.Empty)
                indicatorConfig.Color = indicator.Color;
              if (indicator.Style.HasValue)
                indicatorConfig.Style = indicator.Style;
              nullable28 = indicator.IsDrawnUnder;
              if (nullable28.HasValue)
              {
                indicatorConfig.IsDrawnUnder = indicator.IsDrawnUnder;
                continue;
              }
              continue;
            }
          }
        }
        indicatorConfigList.Remove(indicator.Number);
        indicatorConfigList.Add(indicator);
      }
    }
    foreach (IndicatorConfig indicatorConfig in (Collection<IndicatorConfig>) indicatorConfigList)
    {
      Indicator indicator1 = this.Scintilla.Indicators[indicatorConfig.Number];
      if (indicatorConfig.Color != Color.Empty)
        indicator1.Color = indicatorConfig.Color;
      bool? isDrawnUnder = indicatorConfig.IsDrawnUnder;
      if (isDrawnUnder.HasValue)
      {
        Indicator indicator2 = indicator1;
        isDrawnUnder = indicatorConfig.IsDrawnUnder;
        int num = isDrawnUnder.Value ? 1 : 0;
        indicator2.IsDrawnUnder = num != 0;
      }
      IndicatorStyle? style = indicatorConfig.Style;
      if (style.HasValue)
      {
        Indicator indicator3 = indicator1;
        style = indicatorConfig.Style;
        int num = (int) style.Value;
        indicator3.Style = (IndicatorStyle) num;
      }
    }
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      foreach (KeyWordConfig lexingKeyword in (Collection<KeyWordConfig>) config.Lexing_Keywords)
      {
        if (lexingKeyword.Inherit.HasValue && lexingKeyword.Inherit.Value)
        {
          KeywordCollection keywords;
          int list;
          (keywords = this.Scintilla.Lexing.Keywords)[list = lexingKeyword.List] = keywords[list] + lexingKeyword.Value;
        }
        else
          this.Scintilla.Lexing.Keywords[lexingKeyword.List] = lexingKeyword.Value;
      }
    }
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      foreach (KeyValuePair<string, string> lexingProperty in (Dictionary<string, string>) config.Lexing_Properties)
        this.Scintilla.Lexing.SetProperty(lexingProperty.Key, lexingProperty.Value);
    }
    string str6 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_WhitespaceChars != null)
        str6 = config.Lexing_WhitespaceChars;
    }
    if (str6 != null)
      this.Scintilla.Lexing.WhitespaceChars = str6;
    string str7 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_WordChars != null)
        str7 = config.Lexing_WordChars;
    }
    if (str7 != null)
      this.Scintilla.Lexing.WordChars = str7;
    string str8 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_LineCommentPrefix != null)
        str8 = config.Lexing_LineCommentPrefix;
    }
    if (str8 != null)
      this.Scintilla.Lexing.LineCommentPrefix = str8;
    string str9 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_StreamCommentPrefix != null)
        str9 = config.Lexing_StreamCommentPrefix;
    }
    if (str9 != null)
      this.Scintilla.Lexing.StreamCommentPrefix = str9;
    string str10 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_StreamCommentSuffix != null)
        str10 = config.Lexing_StreamCommentSuffix;
    }
    if (str10 != null)
      this.Scintilla.Lexing.StreamCommentSufix = str10;
    string str11 = (string) null;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Lexing_Language != null)
        str11 = config.Lexing_Language;
    }
    if (str11 == null)
    {
      if (this.Scintilla.Lexing.LexerLanguageMap.ContainsKey(this._language))
      {
        str11 = this.Scintilla.Lexing.LexerLanguageMap[this._language];
      }
      else
      {
        try
        {
          Enum.Parse(typeof (Lexer), this._language, true);
          str11 = this._language;
        }
        catch (ArgumentException ex)
        {
        }
      }
    }
    this.Scintilla.Lexing.LexerName = str11;
    LineCache? nullable29 = new LineCache?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_LayoutCache.HasValue)
        nullable29 = config.LineWrap_LayoutCache;
    }
    if (nullable29.HasValue)
      this.Scintilla.LineWrap.LayoutCache = nullable29.Value;
    WrapMode? nullable30 = new WrapMode?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_Mode.HasValue)
        nullable30 = config.LineWrap_Mode;
    }
    if (nullable30.HasValue)
      this.Scintilla.LineWrap.Mode = nullable30.Value;
    int? nullable31 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_PositionCacheSize.HasValue)
        nullable31 = config.LineWrap_PositionCacheSize;
    }
    if (nullable31.HasValue)
      this.Scintilla.LineWrap.PositionCacheSize = nullable31.Value;
    nullable31 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_StartIndent.HasValue)
        nullable31 = config.LineWrap_StartIndent;
    }
    if (nullable31.HasValue)
      this.Scintilla.LineWrap.StartIndent = nullable31.Value;
    WrapVisualFlag? nullable32 = new WrapVisualFlag?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_VisualFlags.HasValue)
        nullable32 = config.LineWrap_VisualFlags;
    }
    if (nullable32.HasValue)
      this.Scintilla.LineWrap.VisualFlags = nullable32.Value;
    WrapVisualLocation? nullable33 = new WrapVisualLocation?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LineWrap_VisualFlagsLocation.HasValue)
        nullable33 = config.LineWrap_VisualFlagsLocation;
    }
    if (nullable33.HasValue)
      this.Scintilla.LineWrap.VisualFlagsLocation = nullable33.Value;
    Color color8 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LongLines_EdgeColor != Color.Empty)
        color8 = config.LongLines_EdgeColor;
    }
    if (color8 != Color.Empty)
      this.Scintilla.LongLines.EdgeColor = color8;
    int? nullable34 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LongLines_EdgeColumn.HasValue)
        nullable34 = config.LongLines_EdgeColumn;
    }
    if (nullable34.HasValue)
      this.Scintilla.LongLines.EdgeColumn = nullable34.Value;
    EdgeMode? nullable35 = new EdgeMode?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.LongLines_EdgeMode.HasValue)
        nullable35 = config.LongLines_EdgeMode;
    }
    if (nullable35.HasValue)
      this.Scintilla.LongLines.EdgeMode = nullable35.Value;
    if (this._clearMargins)
      this.Scintilla.Margins.Reset();
    Dictionary<int, MarginConfig> dictionary1 = new Dictionary<int, MarginConfig>();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? nullable36 = config.Margin_List.Inherit;
      if (nullable36.HasValue)
      {
        nullable36 = config.Margin_List.Inherit;
        if (!nullable36.Value)
          dictionary1.Clear();
      }
      foreach (MarginConfig margin in (List<MarginConfig>) config.Margin_List)
      {
        if (dictionary1.ContainsKey(margin.Number))
        {
          nullable36 = margin.Inherit;
          if (nullable36.HasValue)
          {
            nullable36 = margin.Inherit;
            if (!nullable36.Value)
              goto label_590;
          }
          MarginConfig marginConfig1 = dictionary1[margin.Number];
          if (margin.AutoToggleMarkerNumber.HasValue)
            marginConfig1.AutoToggleMarkerNumber = new int?(margin.AutoToggleMarkerNumber.Value);
          nullable36 = margin.IsClickable;
          if (nullable36.HasValue)
          {
            MarginConfig marginConfig2 = marginConfig1;
            nullable36 = margin.IsClickable;
            bool? nullable37 = new bool?(nullable36.Value);
            marginConfig2.IsClickable = nullable37;
          }
          nullable36 = margin.IsFoldMargin;
          if (nullable36.HasValue)
          {
            MarginConfig marginConfig3 = marginConfig1;
            nullable36 = margin.IsFoldMargin;
            bool? nullable38 = new bool?(nullable36.Value);
            marginConfig3.IsFoldMargin = nullable38;
          }
          nullable36 = margin.IsMarkerMargin;
          if (nullable36.HasValue)
          {
            MarginConfig marginConfig4 = marginConfig1;
            nullable36 = margin.IsMarkerMargin;
            bool? nullable39 = new bool?(nullable36.Value);
            marginConfig4.IsMarkerMargin = nullable39;
          }
          MarginType? type = margin.Type;
          if (type.HasValue)
          {
            MarginConfig marginConfig5 = marginConfig1;
            type = margin.Type;
            MarginType? nullable40 = new MarginType?(type.Value);
            marginConfig5.Type = nullable40;
          }
          int? width = margin.Width;
          if (width.HasValue)
          {
            MarginConfig marginConfig6 = marginConfig1;
            width = margin.Width;
            int? nullable41 = new int?(width.Value);
            marginConfig6.Width = nullable41;
            continue;
          }
          continue;
        }
label_590:
        dictionary1.Remove(margin.Number);
        dictionary1.Add(margin.Number, margin);
      }
    }
    foreach (MarginConfig marginConfig in dictionary1.Values)
    {
      Margin margin1 = this.Scintilla.Margins[marginConfig.Number];
      int? nullable42 = marginConfig.AutoToggleMarkerNumber;
      if (nullable42.HasValue)
      {
        Margin margin2 = margin1;
        nullable42 = marginConfig.AutoToggleMarkerNumber;
        int num = nullable42.Value;
        margin2.AutoToggleMarkerNumber = num;
      }
      bool? nullable43 = marginConfig.IsClickable;
      if (nullable43.HasValue)
      {
        Margin margin3 = margin1;
        nullable43 = marginConfig.IsClickable;
        int num = nullable43.Value ? 1 : 0;
        margin3.IsClickable = num != 0;
      }
      nullable43 = marginConfig.IsFoldMargin;
      if (nullable43.HasValue)
      {
        Margin margin4 = margin1;
        nullable43 = marginConfig.IsFoldMargin;
        int num = nullable43.Value ? 1 : 0;
        margin4.IsFoldMargin = num != 0;
      }
      nullable43 = marginConfig.IsMarkerMargin;
      if (nullable43.HasValue)
      {
        Margin margin5 = margin1;
        nullable43 = marginConfig.IsMarkerMargin;
        int num = nullable43.Value ? 1 : 0;
        margin5.IsMarkerMargin = num != 0;
      }
      MarginType? type = marginConfig.Type;
      if (type.HasValue)
      {
        Margin margin6 = margin1;
        type = marginConfig.Type;
        int num = (int) type.Value;
        margin6.Type = (MarginType) num;
      }
      nullable42 = marginConfig.Width;
      if (nullable42.HasValue)
      {
        Margin margin7 = margin1;
        nullable42 = marginConfig.Width;
        int num = nullable42.Value;
        margin7.Width = num;
      }
    }
    if (this._clearMarkers)
      this.Scintilla.Markers.Reset();
    MarkersConfigList markersConfigList1 = new MarkersConfigList();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? inherit = config.Markers_List.Inherit;
      if (inherit.HasValue)
      {
        inherit = config.Markers_List.Inherit;
        if (!inherit.Value)
          markersConfigList1.Clear();
      }
      foreach (MarkersConfig markers in (Collection<MarkersConfig>) config.Markers_List)
      {
        MarkersConfigList markersConfigList2 = markersConfigList1;
        int? nullable44 = markers.Number;
        int key1 = nullable44.Value;
        if (markersConfigList2.Contains(key1))
        {
          inherit = markers.Inherit;
          if (inherit.HasValue)
          {
            inherit = markers.Inherit;
            if (!inherit.Value)
              goto label_635;
          }
          nullable44 = markers.Number;
          if (!nullable44.HasValue)
            markers.Number = new int?((int) Enum.Parse(typeof (MarkerOutline), markers.Name, true));
          MarkersConfigList markersConfigList3 = markersConfigList1;
          nullable44 = markers.Number;
          int key2 = nullable44.Value;
          MarkersConfig markersConfig = markersConfigList3[key2];
          nullable44 = markers.Alpha;
          if (nullable44.HasValue)
            markersConfig.Alpha = markers.Alpha;
          if (markers.BackColor != Color.Empty)
            markersConfig.BackColor = markers.BackColor;
          if (markers.ForeColor != Color.Empty)
            markersConfig.ForeColor = markers.ForeColor;
          if (markers.Symbol.HasValue)
          {
            markersConfig.Symbol = markers.Symbol;
            continue;
          }
          continue;
        }
label_635:
        MarkersConfigList markersConfigList4 = markersConfigList1;
        nullable44 = markers.Number;
        int key3 = nullable44.Value;
        markersConfigList4.Remove(key3);
        markersConfigList1.Add(markers);
      }
    }
    foreach (MarkersConfig markersConfig in (Collection<MarkersConfig>) markersConfigList1)
    {
      MarkerCollection markers = this.Scintilla.Markers;
      int? nullable45 = markersConfig.Number;
      int markerNumber = nullable45.Value;
      Marker marker1 = markers[markerNumber];
      nullable45 = markersConfig.Alpha;
      if (nullable45.HasValue)
      {
        Marker marker2 = marker1;
        nullable45 = markersConfig.Alpha;
        int num = nullable45.Value;
        marker2.Alpha = num;
      }
      if (markersConfig.BackColor != Color.Empty)
        marker1.BackColor = markersConfig.BackColor;
      if (markersConfig.ForeColor != Color.Empty)
        marker1.ForeColor = markersConfig.ForeColor;
      MarkerSymbol? symbol = markersConfig.Symbol;
      if (symbol.HasValue)
      {
        Marker marker3 = marker1;
        symbol = markersConfig.Symbol;
        int num = (int) symbol.Value;
        marker3.Symbol = (MarkerSymbol) num;
      }
    }
    FoldMarkerScheme? nullable46 = new FoldMarkerScheme?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Folding_MarkerScheme.HasValue)
        nullable46 = config.Folding_MarkerScheme;
    }
    if (nullable46.HasValue)
      this.Scintilla.Folding.MarkerScheme = nullable46.Value;
    bool? nullable47 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Scrolling_EndAtLastLine.HasValue)
        nullable47 = config.Scrolling_EndAtLastLine;
    }
    if (nullable47.HasValue)
      this.Scintilla.Scrolling.EndAtLastLine = nullable47.Value;
    int? nullable48 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Scrolling_HorizontalWidth.HasValue)
        nullable48 = config.Scrolling_HorizontalWidth;
    }
    if (nullable48.HasValue)
      this.Scintilla.Scrolling.HorizontalWidth = nullable48.Value;
    ScrollBars? nullable49 = new ScrollBars?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Scrolling_ScrollBars.HasValue)
        nullable49 = config.Scrolling_ScrollBars;
    }
    if (nullable49.HasValue)
      this.Scintilla.Scrolling.ScrollBars = nullable49.Value;
    int? nullable50 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Scrolling_XOffset.HasValue)
        nullable50 = config.Scrolling_XOffset;
    }
    if (nullable50.HasValue)
      this.Scintilla.Scrolling.XOffset = nullable50.Value;
    Color color9 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_BackColor != Color.Empty)
        color9 = config.Selection_BackColor;
    }
    if (color9 != Color.Empty)
      this.Scintilla.Selection.BackColor = color9;
    Color color10 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_BackColorUnfocused != Color.Empty)
        color10 = config.Selection_BackColorUnfocused;
    }
    if (color10 != Color.Empty)
      this.Scintilla.Selection.BackColorUnfocused = color10;
    Color color11 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_ForeColor != Color.Empty)
        color11 = config.Selection_ForeColor;
    }
    if (color11 != Color.Empty)
      this.Scintilla.Selection.ForeColor = color11;
    Color color12 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_ForeColorUnfocused != Color.Empty)
        color12 = config.Selection_ForeColorUnfocused;
    }
    if (color12 != Color.Empty)
      this.Scintilla.Selection.ForeColorUnfocused = color12;
    bool? nullable51 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_Hidden.HasValue)
        nullable51 = config.Selection_Hidden;
    }
    if (nullable51.HasValue)
      this.Scintilla.Selection.Hidden = nullable51.Value;
    nullable51 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_HideSelection.HasValue)
        nullable51 = config.Selection_HideSelection;
    }
    if (nullable51.HasValue)
      this.Scintilla.Selection.HideSelection = nullable51.Value;
    ScintillaNet.SelectionMode? nullable52 = new ScintillaNet.SelectionMode?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Selection_Mode.HasValue)
        nullable52 = config.Selection_Mode;
    }
    if (nullable52.HasValue)
      this.Scintilla.Selection.Mode = nullable52.Value;
    if (this._clearSnippets)
      this.Scintilla.Snippets.List.Clear();
    bool? nullable53 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.IsEnabled.HasValue)
        nullable53 = config.SnippetsConfigList.IsEnabled;
    }
    if (nullable53.HasValue)
      this.Scintilla.Snippets.IsEnabled = nullable53.Value;
    nullable53 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.IsOneKeySelectionEmbedEnabled.HasValue)
        nullable53 = config.SnippetsConfigList.IsOneKeySelectionEmbedEnabled;
    }
    if (nullable53.HasValue)
      this.Scintilla.Snippets.IsOneKeySelectionEmbedEnabled = nullable53.Value;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.DefaultDelimeter.HasValue)
      {
        char? defaultDelimeter = config.SnippetsConfigList.DefaultDelimeter;
      }
    }
    if (!nullable9.HasValue)
      this.Scintilla.Snippets.DefaultDelimeter = '$';
    SnippetList snippetList1 = new SnippetList((SnippetManager) null);
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? nullable54 = config.SnippetsConfigList.Inherit;
      if (nullable54.HasValue)
      {
        nullable54 = config.SnippetsConfigList.Inherit;
        if (!nullable54.Value)
          snippetList1.Clear();
      }
      foreach (SnippetsConfig snippetsConfig in (List<SnippetsConfig>) config.SnippetsConfigList)
      {
        if (snippetList1.Contains(snippetsConfig.Shortcut))
          snippetList1.Remove(snippetsConfig.Shortcut);
        char? delimeter1 = snippetsConfig.Delimeter;
        Snippet snippet1;
        if (delimeter1.HasValue)
        {
          SnippetList snippetList2 = snippetList1;
          string shortcut = snippetsConfig.Shortcut;
          string code = snippetsConfig.Code;
          delimeter1 = snippetsConfig.Delimeter;
          int delimeter2 = (int) delimeter1.Value;
          snippet1 = snippetList2.Add(shortcut, code, (char) delimeter2);
        }
        else
          snippet1 = snippetList1.Add(snippetsConfig.Shortcut, snippetsConfig.Code, this.Scintilla.Snippets.DefaultDelimeter);
        nullable54 = snippetsConfig.IsSurroundsWith;
        if (nullable54.HasValue)
        {
          Snippet snippet2 = snippet1;
          nullable54 = snippetsConfig.IsSurroundsWith;
          int num = nullable54.Value ? 1 : 0;
          snippet2.IsSurroundsWith = num != 0;
        }
      }
    }
    SnippetList list1 = this.Scintilla.Snippets.List;
    foreach (Snippet snippet in (Collection<Snippet>) snippetList1)
    {
      if (list1.Contains(snippet.Shortcut))
        list1.Remove(snippet.Shortcut);
      list1.Add(snippet.Shortcut, snippet.Code, this.Scintilla.Snippets.DefaultDelimeter, snippet.IsSurroundsWith);
    }
    list1.Sort();
    Color color13 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.ActiveSnippetColor != Color.Empty)
        color13 = config.SnippetsConfigList.ActiveSnippetColor;
    }
    if (color13 != Color.Empty)
      this.Scintilla.Snippets.ActiveSnippetColor = color13;
    IndicatorStyle? nullable55 = new IndicatorStyle?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.ActiveSnippetIndicatorStyle.HasValue)
        nullable55 = config.SnippetsConfigList.ActiveSnippetIndicatorStyle;
    }
    if (nullable55.HasValue)
      this.Scintilla.Snippets.ActiveSnippetIndicatorStyle = nullable55.Value;
    int? nullable56 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.ActiveSnippetIndicator.HasValue)
        nullable56 = config.SnippetsConfigList.ActiveSnippetIndicator;
    }
    if (nullable56.HasValue)
      this.Scintilla.Snippets.ActiveSnippetIndicator = nullable56.Value;
    Color color14 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.InactiveSnippetColor != Color.Empty)
        color14 = config.SnippetsConfigList.InactiveSnippetColor;
    }
    if (color14 != Color.Empty)
      this.Scintilla.Snippets.InactiveSnippetColor = color14;
    int? nullable57 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.InactiveSnippetIndicator.HasValue)
        nullable57 = config.SnippetsConfigList.InactiveSnippetIndicator;
    }
    if (nullable57.HasValue)
      this.Scintilla.Snippets.InactiveSnippetIndicator = nullable57.Value;
    IndicatorStyle? nullable58 = new IndicatorStyle?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.ActiveSnippetIndicatorStyle.HasValue)
        nullable58 = config.SnippetsConfigList.ActiveSnippetIndicatorStyle;
    }
    if (nullable58.HasValue)
      this.Scintilla.Snippets.ActiveSnippetIndicatorStyle = nullable58.Value;
    IndicatorStyle? nullable59 = new IndicatorStyle?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.SnippetsConfigList.InactiveSnippetIndicatorStyle.HasValue)
        nullable59 = config.SnippetsConfigList.InactiveSnippetIndicatorStyle;
    }
    if (nullable59.HasValue)
      this.Scintilla.Snippets.InactiveSnippetIndicatorStyle = nullable59.Value;
    bool? nullable60 = new bool?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.UndoRedoIsUndoEnabled.HasValue)
        nullable60 = config.UndoRedoIsUndoEnabled;
    }
    if (nullable60.HasValue)
      this.Scintilla.UndoRedo.IsUndoEnabled = nullable60.Value;
    Color color15 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Whitespace_BackColor != Color.Empty)
        color15 = config.Whitespace_BackColor;
    }
    if (color15 != Color.Empty)
      this.Scintilla.Whitespace.BackColor = color15;
    Color color16 = Color.Empty;
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Whitespace_ForeColor != Color.Empty)
        color16 = config.Whitespace_ForeColor;
    }
    if (color16 != Color.Empty)
      this.Scintilla.Whitespace.ForeColor = color16;
    WhitespaceMode? nullable61 = new WhitespaceMode?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Whitespace_Mode.HasValue)
        nullable61 = config.Whitespace_Mode;
    }
    if (nullable61.HasValue)
      this.Scintilla.Whitespace.Mode = nullable61.Value;
    if (this._clearStyles)
      this.Scintilla.Styles.Reset();
    int? nullable62 = new int?();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      if (config.Styles.Bits.HasValue)
        nullable62 = config.Styles.Bits;
    }
    if (nullable62.HasValue)
      this.Scintilla.Styles.Bits = nullable62.Value;
    Dictionary<string, int> styleNameMap = this.Scintilla.Lexing.StyleNameMap;
    ResolvedStyleList resolvedStyleList = new ResolvedStyleList();
    int num1 = -1;
    Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
    foreach (ScintillaNet.Configuration.Configuration config in configList)
    {
      bool? nullable63 = config.Styles.Inherit;
      if (nullable63.HasValue)
      {
        nullable63 = config.Styles.Inherit;
        if (!nullable63.Value)
          resolvedStyleList.Clear();
      }
      foreach (StyleConfig style in (List<StyleConfig>) config.Styles)
      {
        nullable62 = style.Number;
        if (!nullable62.HasValue)
        {
          if (!styleNameMap.ContainsKey(style.Name))
          {
            if (dictionary2.ContainsKey(style.Name))
            {
              nullable62 = new int?(dictionary2[style.Name]);
              style.Number = nullable62;
            }
            else
            {
              nullable62 = new int?(num1--);
              style.Number = nullable62;
              dictionary2[style.Name] = style.Number.Value;
            }
          }
          else
          {
            nullable62 = new int?(styleNameMap[style.Name]);
            style.Number = nullable62;
          }
        }
        StyleConfig styleConfig1 = (StyleConfig) null;
        if (!string.IsNullOrEmpty(style.Name) && style.Name.Contains("."))
          styleConfig1 = resolvedStyleList.FindByName(style.Name.Substring(style.Name.IndexOf(".") + 1));
        if (resolvedStyleList.ContainsKey(nullable62.Value))
        {
          nullable63 = style.Inherit;
          if (nullable63.HasValue)
          {
            nullable63 = style.Inherit;
            if (nullable63.Value)
              goto label_932;
          }
          else
            goto label_932;
        }
        resolvedStyleList.Remove(nullable62.Value);
        resolvedStyleList.Add(nullable62.Value, style);
label_932:
        StyleConfig styleConfig2 = resolvedStyleList[nullable62.Value];
        if (style.BackColor != Color.Empty)
          styleConfig2.BackColor = style.BackColor;
        else if (styleConfig1 != null && styleConfig1.BackColor != Color.Empty)
          styleConfig2.BackColor = styleConfig1.BackColor;
        nullable63 = style.Bold;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig3 = styleConfig2;
          nullable63 = style.Bold;
          bool? nullable64 = new bool?(nullable63.Value);
          styleConfig3.Bold = nullable64;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.Bold;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig4 = styleConfig2;
            nullable63 = styleConfig1.Bold;
            bool? nullable65 = new bool?(nullable63.Value);
            styleConfig4.Bold = nullable65;
          }
        }
        StyleCase? nullable66 = style.Case;
        if (nullable66.HasValue)
        {
          StyleConfig styleConfig5 = styleConfig2;
          nullable66 = style.Case;
          StyleCase? nullable67 = new StyleCase?(nullable66.Value);
          styleConfig5.Case = nullable67;
        }
        else if (styleConfig1 != null)
        {
          nullable66 = styleConfig1.Case;
          if (nullable66.HasValue)
          {
            StyleConfig styleConfig6 = styleConfig2;
            nullable66 = styleConfig1.Case;
            StyleCase? nullable68 = new StyleCase?(nullable66.Value);
            styleConfig6.Case = nullable68;
          }
        }
        CharacterSet? characterSet = style.CharacterSet;
        if (characterSet.HasValue)
        {
          StyleConfig styleConfig7 = styleConfig2;
          characterSet = style.CharacterSet;
          CharacterSet? nullable69 = new CharacterSet?(characterSet.Value);
          styleConfig7.CharacterSet = nullable69;
        }
        else if (styleConfig1 != null)
        {
          characterSet = styleConfig1.CharacterSet;
          if (characterSet.HasValue)
          {
            StyleConfig styleConfig8 = styleConfig2;
            characterSet = styleConfig1.CharacterSet;
            CharacterSet? nullable70 = new CharacterSet?(characterSet.Value);
            styleConfig8.CharacterSet = nullable70;
          }
        }
        if (style.FontName != null)
          styleConfig2.FontName = style.FontName;
        else if (styleConfig1 != null && styleConfig1.FontName != null)
          styleConfig2.FontName = styleConfig1.FontName;
        if (style.ForeColor != Color.Empty)
          styleConfig2.ForeColor = style.ForeColor;
        else if (styleConfig1 != null && styleConfig1.ForeColor != Color.Empty)
          styleConfig2.ForeColor = styleConfig1.ForeColor;
        nullable63 = style.IsChangeable;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig9 = styleConfig2;
          nullable63 = style.IsChangeable;
          bool? nullable71 = new bool?(nullable63.Value);
          styleConfig9.IsChangeable = nullable71;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.IsChangeable;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig10 = styleConfig2;
            nullable63 = styleConfig1.IsChangeable;
            bool? nullable72 = new bool?(nullable63.Value);
            styleConfig10.IsChangeable = nullable72;
          }
        }
        nullable63 = style.IsHotspot;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig11 = styleConfig2;
          nullable63 = style.IsHotspot;
          bool? nullable73 = new bool?(nullable63.Value);
          styleConfig11.IsHotspot = nullable73;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.IsHotspot;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig12 = styleConfig2;
            nullable63 = styleConfig1.IsHotspot;
            bool? nullable74 = new bool?(nullable63.Value);
            styleConfig12.IsHotspot = nullable74;
          }
        }
        nullable63 = style.IsSelectionEolFilled;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig13 = styleConfig2;
          nullable63 = style.IsSelectionEolFilled;
          bool? nullable75 = new bool?(nullable63.Value);
          styleConfig13.IsSelectionEolFilled = nullable75;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.IsSelectionEolFilled;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig14 = styleConfig2;
            nullable63 = styleConfig1.IsSelectionEolFilled;
            bool? nullable76 = new bool?(nullable63.Value);
            styleConfig14.IsSelectionEolFilled = nullable76;
          }
        }
        nullable63 = style.IsVisible;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig15 = styleConfig2;
          nullable63 = style.IsVisible;
          bool? nullable77 = new bool?(nullable63.Value);
          styleConfig15.IsVisible = nullable77;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.IsVisible;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig16 = styleConfig2;
            nullable63 = styleConfig1.IsVisible;
            bool? nullable78 = new bool?(nullable63.Value);
            styleConfig16.IsVisible = nullable78;
          }
        }
        nullable63 = style.Italic;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig17 = styleConfig2;
          nullable63 = style.Italic;
          bool? nullable79 = new bool?(nullable63.Value);
          styleConfig17.Italic = nullable79;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.Italic;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig18 = styleConfig2;
            nullable63 = styleConfig1.Italic;
            bool? nullable80 = new bool?(nullable63.Value);
            styleConfig18.Italic = nullable80;
          }
        }
        int? size = style.Size;
        if (size.HasValue)
        {
          StyleConfig styleConfig19 = styleConfig2;
          size = style.Size;
          int? nullable81 = new int?(size.Value);
          styleConfig19.Size = nullable81;
        }
        else if (styleConfig1 != null)
        {
          size = styleConfig1.Size;
          if (size.HasValue)
          {
            StyleConfig styleConfig20 = styleConfig2;
            size = styleConfig1.Size;
            int? nullable82 = new int?(size.Value);
            styleConfig20.Size = nullable82;
          }
        }
        nullable63 = style.Underline;
        if (nullable63.HasValue)
        {
          StyleConfig styleConfig21 = styleConfig2;
          nullable63 = style.Underline;
          bool? nullable83 = new bool?(nullable63.Value);
          styleConfig21.Underline = nullable83;
        }
        else if (styleConfig1 != null)
        {
          nullable63 = styleConfig1.Underline;
          if (nullable63.HasValue)
          {
            StyleConfig styleConfig22 = styleConfig2;
            nullable63 = styleConfig1.Underline;
            bool? nullable84 = new bool?(nullable63.Value);
            styleConfig22.Underline = nullable84;
          }
        }
      }
    }
    StyleConfig[] array = new StyleConfig[resolvedStyleList.Count];
    resolvedStyleList.Values.CopyTo(array, 0);
    Array.Sort<StyleConfig>(array, (Comparison<StyleConfig>) ((sc1, sc2) =>
    {
      int num2 = sc1.Number.Value == 32 /*0x20*/ ? -1 : sc1.Number.Value;
      int num3 = sc2.Number.Value == 32 /*0x20*/ ? -1 : sc2.Number.Value;
      if (num2 < num3)
        return -1;
      return num3 < num2 ? 1 : 0;
    }));
    foreach (StyleConfig styleConfig in array)
    {
      int? nullable85 = styleConfig.Number;
      if ((nullable85.GetValueOrDefault() >= 0 ? 0 : (nullable85.HasValue ? 1 : 0)) == 0)
      {
        StyleCollection styles = this.Scintilla.Styles;
        nullable85 = styleConfig.Number;
        int index = nullable85.Value;
        Style style1 = styles[index];
        if (styleConfig.BackColor != Color.Empty)
          style1.BackColor = styleConfig.BackColor;
        bool? nullable86 = styleConfig.Bold;
        if (nullable86.HasValue)
        {
          Style style2 = style1;
          nullable86 = styleConfig.Bold;
          int num4 = nullable86.Value ? 1 : 0;
          style2.Bold = num4 != 0;
        }
        StyleCase? nullable87 = styleConfig.Case;
        if (nullable87.HasValue)
        {
          Style style3 = style1;
          nullable87 = styleConfig.Case;
          int num5 = (int) nullable87.Value;
          style3.Case = (StyleCase) num5;
        }
        CharacterSet? characterSet = styleConfig.CharacterSet;
        if (characterSet.HasValue)
        {
          Style style4 = style1;
          characterSet = styleConfig.CharacterSet;
          int num6 = (int) characterSet.Value;
          style4.CharacterSet = (CharacterSet) num6;
        }
        if (styleConfig.FontName != null)
          style1.FontName = styleConfig.FontName;
        if (styleConfig.ForeColor != Color.Empty)
          style1.ForeColor = styleConfig.ForeColor;
        nullable86 = styleConfig.IsChangeable;
        if (nullable86.HasValue)
        {
          Style style5 = style1;
          nullable86 = styleConfig.IsChangeable;
          int num7 = nullable86.Value ? 1 : 0;
          style5.IsChangeable = num7 != 0;
        }
        nullable86 = styleConfig.IsHotspot;
        if (nullable86.HasValue)
        {
          Style style6 = style1;
          nullable86 = styleConfig.IsHotspot;
          int num8 = nullable86.Value ? 1 : 0;
          style6.IsHotspot = num8 != 0;
        }
        nullable86 = styleConfig.IsSelectionEolFilled;
        if (nullable86.HasValue)
        {
          Style style7 = style1;
          nullable86 = styleConfig.IsSelectionEolFilled;
          int num9 = nullable86.Value ? 1 : 0;
          style7.IsSelectionEolFilled = num9 != 0;
        }
        nullable86 = styleConfig.IsVisible;
        if (nullable86.HasValue)
        {
          Style style8 = style1;
          nullable86 = styleConfig.IsVisible;
          int num10 = nullable86.Value ? 1 : 0;
          style8.IsVisible = num10 != 0;
        }
        nullable86 = styleConfig.Italic;
        if (nullable86.HasValue)
        {
          Style style9 = style1;
          nullable86 = styleConfig.Italic;
          int num11 = nullable86.Value ? 1 : 0;
          style9.Italic = num11 != 0;
        }
        nullable85 = styleConfig.Size;
        if (nullable85.HasValue)
        {
          Style style10 = style1;
          nullable85 = styleConfig.Size;
          double num12 = (double) nullable85.Value;
          style10.Size = (float) num12;
        }
        nullable86 = styleConfig.Underline;
        if (nullable86.HasValue)
        {
          Style style11 = style1;
          nullable86 = styleConfig.Underline;
          int num13 = nullable86.Value ? 1 : 0;
          style11.Underline = num13 != 0;
        }
        nullable85 = styleConfig.Number;
        if ((nullable85.GetValueOrDefault() != 32 /*0x20*/ ? 0 : (nullable85.HasValue ? 1 : 0)) != 0)
          this.Scintilla.Styles.ClearAll();
      }
    }
    if (this.Scintilla.UseFont)
      this.Scintilla.Font = this.Scintilla.Font;
    if (this.Scintilla.UseForeColor)
      this.Scintilla.UseForeColor = this.Scintilla.UseForeColor;
    if (!this.Scintilla.UseBackColor)
      return;
    this.Scintilla.UseBackColor = this.Scintilla.UseBackColor;
  }
}
