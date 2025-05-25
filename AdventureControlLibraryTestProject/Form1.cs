namespace AdventureControlLibraryTestProject;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        textInputControl1.TellCursorActivity(keyData);
        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void Form1_Load(object sender, EventArgs e)
    {
        var parser = textInputControl1.Parser;

        parser.AddVerbs("GO", "OPEN", "CLOSE", "GIVE", "SHOW", "LOOK", "INVENTORY", "GET", "TAKE", "DROP", "USE");
        parser.AddImportantFillers("TO", "ON", "IN");
        parser.AddUnimportantFillers("THE", "A", "AN", "AT");
        parser.AddNouns(
            "NORTH",
            "EAST",
            "WEST",
            "SOUTH",
            "GREEN DOOR",
            "BLUE DOOR",
            "SKELETON KEY",
            "GOLD KEY"
        );
        parser.Aliases.Add("GO NORTH", "N", "NORTH");
        parser.Aliases.Add("GO EAST", "E", "EAST");
        parser.Aliases.Add("GO SOUTH", "S", "SOUTH");
        parser.Aliases.Add("GO WEST", "W", "WEST");
        parser.Aliases.Add("INVENTORY", "I", "INV");
    }

    private void textInputControl1_CommandEntered(object sender, TextAdventureGameInputParser.Sentence command)
    {
        switch (command.CleanInput)
        {
            case "N":
                graphicsOutputControl1.SetGraphics(@"


");
                break;
            default:
                textInputControl1.Write("THANK YOU! THIS WAS NICE! NOW I HAVE TO WRITE A LONG TEXT TO TEST THE WORD WRAPPING SHIT THAT I HAVE IMPLEMENTED IN THIS TEST PROGRAM.");
                return;
        }
    }
}