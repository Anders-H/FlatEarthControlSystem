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
}