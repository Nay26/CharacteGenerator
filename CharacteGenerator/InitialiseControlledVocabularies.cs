using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CharacteGenerator
{
    class InitialiseObjects
    {
        public List<ControlledVocabualry> controlledVocabList;
        public List<ImageTuple> imageTupleList;
        public TextBox csvTextBox;
        public RichTextBox richTextBox1;

        public InitialiseObjects()
        {
        }

        public void InitialiseControlledVocabs(TextBox tb, RichTextBox rtb)
        {
            controlledVocabList = new List<ControlledVocabualry>();

            csvTextBox = tb;
            richTextBox1 = rtb;
            if (!(csvTextBox.Text.Equals("")))
            {
                var path = @csvTextBox.Text;
                string[] lines = System.IO.File.ReadAllLines(path);
                string[] collumns = lines[0].Split(',');
                foreach (var collumn in collumns)
                {
                    ControlledVocabualry vocab = new ControlledVocabualry();
                    vocab.vocabName = collumn;
                    controlledVocabList.Add(vocab);
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    collumns = lines[i].Split(',');
                    for (int j = 0; j < collumns.Length; j++)
                    {
                        if (!(collumns[j].Equals("")))
                        {
                            controlledVocabList[j].values.Add(collumns[j]);
                        }

                    }

                }

                //Split ; into multiple entries
                foreach (ControlledVocabualry vocab in controlledVocabList)
                {
                    for (int i = 0; i < vocab.values.Count; i++)
                    {
                        if (vocab.values[i].Contains(";"))
                        {
                            string[] splitvalues = vocab.values[i].Split(';');
                            vocab.values.RemoveAt(i);
                            for (int j = 0; j < splitvalues.Length; j++)
                            {
                                vocab.values.Add(splitvalues[j]);
                            }
                        }

                    }

                }


                // Sort Controlled Vocabs
                List<string> valueInVocab = new List<string>();
                foreach (ControlledVocabualry vocab in controlledVocabList)
                {
                    valueInVocab = new List<string>();
                    foreach (string value in vocab.values)
                    {
                        if (!(valueInVocab.Contains(value)))
                        {
                            valueInVocab.Add(value);
                        }

                    }
                    vocab.values = valueInVocab;
                }

                // Print controlled vocabs
                foreach (ControlledVocabualry vocab in controlledVocabList)
                {
                    richTextBox1.AppendText("\n" + vocab.vocabName);
                    foreach (string value in vocab.values)
                    {
                        richTextBox1.AppendText(" :" + value);
                    }

                }
            }
        }

        public void InitialiseCharacterObjects(TextBox tb, RichTextBox rtb)
        {
            imageTupleList = new List<ImageTuple>();
            csvTextBox = tb;
            richTextBox1 = rtb;
            if (!(csvTextBox.Text.Equals("")))
            {
                var path = @csvTextBox.Text;
                string[] lines = System.IO.File.ReadAllLines(path);
                string[] collumns = lines[0].Split(',');
                List<string> controlledVocabNames = new List<string>();
                for (int i = 0; i < collumns.Length; i++)
                {
                    controlledVocabNames.Add(collumns[i]);
                }
                // for every tuple
                for (int i = 1; i < lines.Length; i++)
                {
                    // split tuple to array (,)
                    collumns = lines[i].Split(',');
                    ImageTuple imageTuple = new ImageTuple();
                    imageTuple.imageID = collumns[0];

                    // for every other collumn of that tuple
                    for (int j = 1; j < collumns.Length; j++)
                    {
                        // if its not empty
                        if (!(collumns[j].Equals("")))
                        {
                            ControlledVocabualry controlledVocab = new ControlledVocabualry();
                            controlledVocab.vocabName = controlledVocabNames[j];
                            string[] splitCollumn = collumns[j].Split(';');
                            for (int k = 0; k < splitCollumn.Length; k++)
                            {
                                controlledVocab.values.Add(splitCollumn[k]);
                            }
                            imageTuple.controlledVocabualries.Add(controlledVocab);
                        }

                    }
                    imageTupleList.Add(imageTuple);
                }
                // Print controlled vocabs
                foreach (ImageTuple tuple in imageTupleList)
                {
                    richTextBox1.AppendText("\n" + tuple.imageID);
                    foreach (ControlledVocabualry vocab in tuple.controlledVocabualries)
                    {
                        foreach (string value in vocab.values)
                        {
                            richTextBox1.AppendText(" :" + value);
                        }
                    }

                }

            }
        }
    }
}
