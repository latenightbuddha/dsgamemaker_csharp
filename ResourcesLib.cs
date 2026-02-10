using System;
using System.IO;

internal static partial class ResourcesLib
{

    private static bool ShowWindowMode = true;

    public static void ToggleShowWindow()
    {
        ShowWindowMode = !ShowWindowMode;
    }

    // ''''''''''''''''
    // ''' SPRITES ''''
    // ''''''''''''''''

    public static void AddSprite(string DName)
    {
        File.Copy(AppPath + @"DefaultResources\Sprite.png", SessionPath + @"Sprites\0_" + DName + ".png");
        XDSAddLine("SPRITE " + DName + ",32,32");
        foreach (Form X in MainForm.MdiChildren)
        {
            if (!IsObject(X.Text))
                continue;
            ((DObject)X).AddSprite(DName);
        }
        AddResourceNode(ResourceIDs.Sprite, DName, "SpriteNode", ShowWindowMode);
        RedoSprites = true;
    }

    // ''''''''''''''''
    // ''' OBJECTS ''''
    // ''''''''''''''''

    public static bool IsObject(string DName)
    {
        return GetXDSFilter("OBJECT " + DName + ",").Length > 0;
    }

    public static void AddObject(string DName)
    {
        ushort ObjectCount = GetXDSFilter("OBJECT ").Length;
        XDSAddLine("OBJECT " + DName + ",None,0");
        foreach (Form X in MainForm.MdiChildren)
        {
            if (!(X.Name == "Room"))
                continue;
            ((Room)X).AddObjectToDropper(DName);
        }
        AddResourceNode(ResourceIDs.DObject, DName, "ObjectNode", ShowWindowMode);
    }

    // ''''''''''''''''''''
    // ''' BACKGROUNDS ''''
    // ''''''''''''''''''''

    public static void AddBackground(string DName)
    {
        File.Copy(AppPath + @"DefaultResources\Background.png", SessionPath + @"Backgrounds\" + DName + ".png");
        XDSAddLine("BACKGROUND " + DName);
        AddResourceNode(ResourceIDs.Background, DName, "BackgroundNode", ShowWindowMode);
        foreach (Form X in MainForm.MdiChildren)
        {
            if (!IsRoom(X.Text))
                continue;
            foreach (Control Y in X.Controls)
            {
                if (!(Y.Name == "ObjectsTabControl"))
                    continue;
                foreach (Control Z in ((TabControl)Y).TabPages(0).Controls)
                {
                    if (Z.Name == "TopScreenGroupBox" | Z.Name == "BottomScreenGroupBox")
                    {
                        foreach (Control I in Z.Controls)
                        {
                            if (I.Name.EndsWith("BGDropper"))
                            {
                                ((ComboBox)I).Items.Add(DName);
                            }
                        }
                    }
                }
            }
        }
        BGsToRedo.Add(DName);
    }

    public static bool IsBackground(string DName)
    {
        return DoesXDSLineExist("BACKGROUND " + DName);
    }

    // '''''''''''''''
    // ''' SOUNDS ''''
    // '''''''''''''''

    public static void AddSound(string DName)
    {
        SoundType.ShowDialog();
        bool SB = SoundType.IsSoundEffect;
        string Extension = SB ? "wav" : "mp3";
        File.Copy(AppPath + @"DefaultResources\Sound." + Extension, SessionPath + @"Sounds\" + DName + "." + Extension);
        XDSAddLine("SOUND " + DName + "," + (SB ? 0 : 1).ToString());
        AddResourceNode(ResourceIDs.Sound, DName, "SoundNode", ShowWindowMode);
        SoundsToRedo.Add(DName);
    }

    // ''''''''''''''
    // ''' ROOMS ''''
    // ''''''''''''''

    public static void AddRoom(string DName)
    {
        byte RoomCount = GetXDSFilter("ROOM ").Length;
        short DW = Convert.ToInt16(GetOption("DEFAULT_ROOM_WIDTH"));
        short DH = Convert.ToInt16(GetOption("DEFAULT_ROOM_HEIGHT"));
        if (DW < 256)
            DW = 256;
        if (DW > 4096)
            DW = 4096;
        if (DW < 192)
            DW = 192;
        if (DH > 4096)
            DH = 4096;
        XDSAddLine("ROOM " + DName + "," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1,");
        AddResourceNode(ResourceIDs.Room, DName, "RoomNode", ShowWindowMode);
    }

    public static bool IsRoom(string ThingyName)
    {
        return GetXDSFilter("ROOM " + ThingyName + ",").Length > 0;
    }

    // ''''''''''''''''
    // ''' SCRIPTS ''''
    // ''''''''''''''''

    public static void AddScript(string DName, bool IsDBAS)
    {
        File.CreateText(SessionPath + @"Scripts\" + DName + ".dbas").Dispose();
        XDSAddLine("SCRIPT " + DName + "," + (IsDBAS ? 1 : 0).ToString());
        AddResourceNode(ResourceIDs.Script, DName, "ScriptNode", ShowWindowMode);
    }

}