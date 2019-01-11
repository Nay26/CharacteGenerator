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
        List<ListBox> listBoxList = new List<ListBox>();
        List<ListBox> listBoxList2 = new List<ListBox>();
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
                initialiser = new InitialiseObjects();
                csvTextBox.Text = openFileDialog1.FileName;               
                initialiser.InitialiseControlledVocabs(csvTextBox,richTextBox);
                initialiser.InitialiseCharacterObjects(csvTextBox, richTextBox);
                GenerateFilters();
            }

        }

        private void GenerateFilters()
        {
            tableLayoutPanel1.Controls.Clear();
            index = 0;
            listBoxList2.Clear();
            listBoxList.Clear();
            foreach (ControlledVocabualry controlledVocabualry in initialiser.controlledVocabList)
            {
               

                ListBox lb = new ListBox();
                //lb.Dock = DockStyle.Fill;
                lb.DataSource = controlledVocabualry.values;                
                listBoxList.Add(lb);
                lb.Name = ("" + index);
                lb.SelectedIndexChanged += lb_SelectedIndexChanged;

                List<string> dummyList = new List<string>();
                ListBox lb2 = new ListBox();
                //lb.Dock = DockStyle.Fill;
                lb2.DataSource = dummyList;
                listBoxList2.Add(lb2);
                lb2.Name = ("" + index);
                lb2.SelectedIndexChanged += lb2_SelectedIndexChanged;


                if (index == 0)
                { 
                    tableLayoutPanel1.Controls.Add(listBoxList2[index], index, 1);
                    tableLayoutPanel1.Controls.Add(listBoxList[index], index, 0);
                }
                else
                {
                    tableLayoutPanel1.ColumnCount++;
                    tableLayoutPanel1.Controls.Add(listBoxList2[index], index, 1);
                    tableLayoutPanel1.Controls.Add(listBoxList[index], index, 0);
                }
                index++;               
            }
            TableLayoutColumnStyleCollection styles = tableLayoutPanel1.ColumnStyles;
            foreach (ColumnStyle style in styles)
            {
                style.SizeType = SizeType.Absolute;
                style.Width = 150;
            }


        }

        private void lb2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = sender as ListBox;
            int lbIndex = Int32.Parse(lb.Name);
            List<string> lb2ValueList = new List<string>();
            lb2ValueList = listBoxList2[lbIndex].DataSource as List<string>;
            if (!(lb.SelectedItem == null))
            {
                string selectedItem = lb.SelectedItem.ToString();
                listBoxList2[lbIndex].DataSource = null;
                lb2ValueList.Remove(selectedItem);
            }
            listBoxList2[lbIndex].DataSource = null;
            lb.DataSource = lb2ValueList;
        }

        private void lb_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ListBox lb = sender as ListBox;
            int lbIndex = Int32.Parse(lb.Name);
            List<string> lb2ValueList = new List<string>();
            lb2ValueList = listBoxList2[lbIndex].DataSource as List<string>;
            listBoxList2[lbIndex].DataSource = null;
            string selectedItem = lb.SelectedItem.ToString();
            Boolean allreadyAdded = false;

            foreach (string value in lb2ValueList)
            {
                if (value.Equals(selectedItem))
                {
                    allreadyAdded = true;
                }
            }
            
            
            if (allreadyAdded == false)
            {
                lb2ValueList.Add(selectedItem);
            }

            listBoxList2[lbIndex].DataSource = lb2ValueList;
            

        }

        private void generateButton_Click(object sender, EventArgs e)
        {
            // Get filter options:
            List<ControlledVocabualry> selectedFiltersList = new List<ControlledVocabualry>();
            for (int i = 0; i < listBoxList2.Count(); i++)
            {
                ControlledVocabualry cv = new ControlledVocabualry();
                cv.vocabName = initialiser.controlledVocabList[i].vocabName;
                List<string> lb2ValueList = new List<string>();
                lb2ValueList = listBoxList2[i].DataSource as List<string>;
                cv.values = lb2ValueList;
                selectedFiltersList.Add(cv);
            }

            List<string> filteredImageIdList = new List<string>();
            Boolean match = false;
            // Compare with Image Tuples: Output a list of valid image ID's
            foreach (ImageTuple tuple in initialiser.imageTupleList)
            {
                Console.WriteLine(tuple.imageID);
                match = true;
                for (int i = 1; i < selectedFiltersList.Count; i++)
                {
                    Boolean oneMatch = true;
                    if (selectedFiltersList[i].values.Count > 0)
                    {
                        oneMatch = false;
                    }                   
                    foreach (string vocabValue in selectedFiltersList[i].values)
                    {            
                        
                        Console.WriteLine(tuple.controlledVocabualries[i - 1].vocabName);
                        if ((tuple.controlledVocabualries[i - 1].values.Contains(vocabValue)))
                        {
                            oneMatch = true;
                        }
                       
                    }
                    if (oneMatch == false)
                    {
                        match = false;
                    }
                }
                if (match == true)
                {
                    filteredImageIdList.Add(tuple.imageID);
                }
            }
            foreach (string imageid in filteredImageIdList)
            {
                richTextBox.AppendText("\n" + imageid);
            }
        }
    }
}
