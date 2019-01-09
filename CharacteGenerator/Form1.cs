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
        List<RichTextBox> richTextBoxList = new List<RichTextBox>();
        int index = 0; 

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
          
            foreach (ControlledVocabualry controlledVocabualry in initialiser.controlledVocabList)
            {
                RichTextBox rtb = new RichTextBox();
                rtb.Dock = DockStyle.Fill;
                rtb.Text = controlledVocabualry.vocabName;
                richTextBoxList.Add(rtb);
                if (index == 0)
                {
                    tableLayoutPanel1.Controls.Add(rtb, index, 1);
                }
                else
                {
                    tableLayoutPanel1.ColumnCount++;
                    tableLayoutPanel1.Controls.Add(rtb, index, 1);
                }
                index++;
                TableLayoutColumnStyleCollection styles = tableLayoutPanel1.ColumnStyles;
                foreach (ColumnStyle style in styles)
                {
                    style.SizeType = SizeType.Absolute;
                    style.Width = 150;
                }

            }

           
        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            

        }
    }
}
