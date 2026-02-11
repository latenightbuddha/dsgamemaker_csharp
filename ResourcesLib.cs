using System;
using System.IO;

namespace DS_Game_Maker
{
    internal static partial class ResourcesLib
    {

        private static bool ShowWindowMode = true;

        public static bool RedoSprites { get; private set; }

        public static void ToggleShowWindow()
        {
            ShowWindowMode = !ShowWindowMode;
        }

        // ''''''''''''''''
        // ''' SPRITES ''''
        // ''''''''''''''''

        public static void AddSprite(string DName)
        {
            File.Copy(DSGMlib.AppPath + @"DefaultResources\Sprite.png", SessionsLib.SessionPath + @"Sprites\0_" + DName + ".png");
            DSGMlib.XDSAddLine("SPRITE " + DName + ",32,32");
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
        {
                if (!IsObject(X.Text))
                    continue;
                ((DObject)X).AddSprite(DName);
            }
            byte sprite = (byte)DSGMlib.ResourceIDs.Sprite;
            DSGMlib.AddResourceNode(ref sprite, DName, "SpriteNode", ShowWindowMode);
            RedoSprites = true;
        }

        // ''''''''''''''''
        // ''' OBJECTS ''''
        // ''''''''''''''''

        public static bool IsObject(string DName)
        {
            return DSGMlib.GetXDSFilter("OBJECT " + DName + ",").Length > 0;
        }

        public static void AddObject(string DName)
        {
            ushort ObjectCount = (ushort)DSGMlib.GetXDSFilter("OBJECT ").Length;
            DSGMlib.XDSAddLine("OBJECT " + DName + ",None,0");
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!(X.Name == "Room"))
                    continue;
                ((Room)X).AddObjectToDropper(DName);
            }
            byte dobj = (byte)DSGMlib.ResourceIDs.DObject;
            DSGMlib.AddResourceNode(ref dobj, DName, "ObjectNode", ShowWindowMode);
        }

        // ''''''''''''''''''''
        // ''' BACKGROUNDS ''''
        // ''''''''''''''''''''

        public static void AddBackground(string DName)
        {
            File.Copy(DSGMlib.AppPath + @"DefaultResources\Background.png", SessionsLib.SessionPath + @"Backgrounds\" + DName + ".png");
            DSGMlib.XDSAddLine("BACKGROUND " + DName);
            byte background = (byte)DSGMlib.ResourceIDs.Background;
            DSGMlib.AddResourceNode(ref background, DName, "BackgroundNode", ShowWindowMode);
            foreach (Form X in Program.Forms.main_Form.MdiChildren)
            {
                if (!IsRoom(X.Text))
                    continue;

                foreach (Control Y in X.Controls)
                {
                    if (!(Y.Name == "ObjectsTabControl"))
                        continue;

                    foreach (Control Z in ((TabControl)Y).TabPages[0].Controls)
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
            DSGMlib.BGsToRedo.Add(DName);
        }

        public static bool IsBackground(string DName)
        {
            return DSGMlib.DoesXDSLineExist("BACKGROUND " + DName);
        }

        // '''''''''''''''
        // ''' SOUNDS ''''
        // '''''''''''''''

        public static void AddSound(string DName)
        {
            Program.Forms.soundType_Form.ShowDialog();
            bool SB = Program.Forms.soundType_Form.IsSoundEffect;
            string Extension = SB ? "wav" : "mp3";
            File.Copy(DSGMlib.AppPath + @"DefaultResources\Sound." + Extension,SessionsLib.SessionPath + @"Sounds\" + DName + "." + Extension);
            DSGMlib.XDSAddLine("SOUND " + DName + "," + (SB ? 0 : 1).ToString());
            byte sound = (byte)DSGMlib.ResourceIDs.Sound;
            DSGMlib.AddResourceNode(ref sound, DName, "SoundNode", ShowWindowMode);
            DSGMlib.SoundsToRedo.Add(DName);
        }

        // ''''''''''''''
        // ''' ROOMS ''''
        // ''''''''''''''

        public static void AddRoom(string DName)
        {
            byte RoomCount = (byte)DSGMlib.GetXDSFilter("ROOM ").Length;
            short DW = Convert.ToInt16(OptionsLib.GetOption("DEFAULT_ROOM_WIDTH"));
            short DH = Convert.ToInt16(OptionsLib.GetOption("DEFAULT_ROOM_HEIGHT"));
            if (DW < 256)
                DW = 256;
            if (DW > 4096)
                DW = 4096;
            if (DW < 192)
                DW = 192;
            if (DH > 4096)
                DH = 4096;
            DSGMlib.XDSAddLine("ROOM " + DName + "," + DW.ToString() + "," + DH.ToString() + ",1,," + DW.ToString() + "," + DH.ToString() + ",1,");
            byte room = (byte)DSGMlib.ResourceIDs.Room;
            DSGMlib.AddResourceNode(ref room, DName, "RoomNode", ShowWindowMode);
        }

        public static bool IsRoom(string ThingyName)
        {
            return DSGMlib.GetXDSFilter("ROOM " + ThingyName + ",").Length > 0;
        }

        // ''''''''''''''''
        // ''' SCRIPTS ''''
        // ''''''''''''''''

        public static void AddScript(string DName, bool IsDBAS)
        {
            File.CreateText(SessionsLib.SessionPath + @"Scripts\" + DName + ".dbas").Dispose();
            DSGMlib.XDSAddLine("SCRIPT " + DName + "," + (IsDBAS ? 1 : 0).ToString());
            byte script = (byte)DSGMlib.ResourceIDs.Script;
            DSGMlib.AddResourceNode(ref script, DName, "ScriptNode", ShowWindowMode);
        }

    }
}