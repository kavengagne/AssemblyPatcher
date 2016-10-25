using System;
using System.Globalization;
using System.Windows.Forms;
using AssemblyPatch.Logging.Patches;
using AssemblyPatcher.Core;
using AssemblyPatcher.MethodPatcher;
using CalculatorLib;


namespace Main
{
    public partial class TestForm : Form
    {
        private readonly Calculator _calculator;
        
        public TestForm()
        {
            InitializeComponent();
            _calculator = new Calculator();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addResult = _calculator.Add(Convert.ToDouble(operand1.Text), Convert.ToDouble(operand2.Text));
            result.Text = addResult.ToString(CultureInfo.InvariantCulture);
        }
        
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            var addResult = _calculator.Add(Convert.ToDouble(operand1.Text), Convert.ToDouble(operand2.Text));
            result.Text = addResult.ToString(CultureInfo.InvariantCulture);
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            var addResult = _calculator.Sub(Convert.ToDouble(operand1.Text), Convert.ToDouble(operand2.Text));
            result.Text = addResult.ToString(CultureInfo.InvariantCulture);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
            {
                txtPath.Text = openFileDialog1.FileName;
            }
        }

        private void btnInject_Click(object sender, EventArgs e)
        {
            var modulePath = txtPath.Text;
            var assemblyEditor = new AssemblyEditor(modulePath);
            var methodPatcher = assemblyEditor.GetPatcher<MethodPatcher>();
            if (methodPatcher == null)
            {
                return;
            }

            // TODO: KG - Check that namespaces are looking good from outside AssemblyPatcher.Core
            //AssemblyPatcher.Patchers.


            methodPatcher.AddPatch(new LogMethodNamePatch());
            //methodPatcher.AddPatch(new LogMethodParametersPatch());
            //methodPatcher.AddPatch(new LogMethodResultPatch());


            methodPatcher.AppliesTo("CalculatorLib.Calculator").AppliesTo("Add", "Add2");
            //methodPatcher.AppliesTo("CalculatorLib.Calculator").AppliesTo(typeof(Calculator).GetMethod("Add"));
            //methodPatcher.AppliesTo(typeof(Calculator)).AppliesTo("Add", "Sub");
            //methodPatcher.AppliesTo(typeof(Calculator)).AppliesTo(typeof(Calculator).GetMethod("Add"));
            if (methodPatcher.ApplyPatches())
            {
                // TODO: KG - Change path for real path here
                assemblyEditor.Write(@"E:\Dropbox\Projects\AssemblyPatcher\Main\bin\Debug\CalculatorLib2.dll");
            }
        }
    }
}
