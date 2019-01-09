using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CharacteGenerator
{
    public partial class Form1 : Form
    {
        InitialiseObjects initialiser = new InitialiseObjects();

        public Form1()
        {
            InitializeComponent();           
        }

        private void selectCSVButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "CSV Files|*.csv";
            openFileDialog1.Title = "Select a CSV";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {  
                csvTextBox.Text = openFileDialog1.FileName;
                
                initialiser.InitialiseControlledVocabs(csvTextBox,richTextBox);
                initialiser.InitialiseCharacterObjects(csvTextBox, richTextBox);
                GenerateFilters();
            }

        }

        private void GenerateFilters()
        {

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            

        }
    }
}
