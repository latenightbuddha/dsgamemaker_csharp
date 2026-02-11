
namespace DS_Game_Maker
{
    public class GlobalFormsClass
    {
        public string[] CommandLineArgs;

        public MainForm main_Form;
        public SoundType soundType_Form;
        public GameSettings gameSettings_Form;
        public Options options_Form;
        public Compiled compiled_Form;
        public ActionEditor actionEditor_Form;
        public FontViewer fontViewer_Form;
        public AboutDSGM aboutDSG_Form;
        public GlobalArrays globalArrays_Form;
        public Statistics statistics_Form;
        public GlobalVariables globalVariables_Form;
        public GlobalFormsClass globalFormsClass_Form;
        public GlobalStructures globalStructures_Form;
        public DUpdate dUpdate_Form;
        public HelpViewer helpViewer_Form;
        public Action action_Form;
        public EditCode editCode_Form;
        public DEvent dEvent_Form;
        public StructureItem structureItem_Form;
        public SetCoOrdinates setCoOrdinates_Form;
        public Preview preview_Form;
        public BadSpriteSize badSpriteSize_Form;
        public TransformSprite transformSprite_Form;
        public ImportSprite importSprite_Form;

        public void Initialize(string[] args)
        {
            CommandLineArgs = args;

            main_Form = new MainForm();
            soundType_Form = new SoundType();
            gameSettings_Form = new GameSettings();
            options_Form = new Options();
            compiled_Form = new Compiled();
            actionEditor_Form = new ActionEditor();
            fontViewer_Form = new FontViewer();
            aboutDSG_Form = new AboutDSGM();
            globalArrays_Form = new GlobalArrays();
            statistics_Form = new Statistics();
            globalVariables_Form = new GlobalVariables();
            globalFormsClass_Form = new GlobalFormsClass();
            globalStructures_Form = new GlobalStructures();
            dUpdate_Form = new DUpdate();
            helpViewer_Form = new HelpViewer();
            action_Form = new Action();
            editCode_Form = new EditCode();
            dEvent_Form = new DEvent();
            structureItem_Form = new StructureItem();
            setCoOrdinates_Form = new SetCoOrdinates();
            preview_Form = new Preview();
            badSpriteSize_Form = new BadSpriteSize();
            transformSprite_Form = new TransformSprite();
            importSprite_Form = new ImportSprite();
        }
    }

    public static class Constants
    {
        public const string vbCrLf = "\r\n";
        public const string vbNewLine = "\n";
    }
}
