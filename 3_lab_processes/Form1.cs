using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3_lab_processes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool checker = false;

        private void buttonFile_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string textFile;
                int numberOfLines;
                string[] textResources = null;
                int[] resources = null;
                int[] resourceCount = null;
                int[] need = null;

                string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "H", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
                string filename = ofd.FileName;

                try
                {
                    using (var streamReader = new StreamReader(filename, Encoding.UTF8))
                    {
                        textFile = streamReader.ReadToEnd();
                        textBoxDefault.Text = textFile + Environment.NewLine;
                        string ResoursesLine = File.ReadLines(filename).Skip(0).First();
                        numberOfLines = File.ReadAllLines(filename).Length - 1;

                        textResources = ResoursesLine.Split(' ');
                        resources = new int[textResources.Length];

                        for (int i = 0; i != resources.Length; i++)
                        {
                            resources[i] = Convert.ToInt32(textResources[i]);
                        }

                        resourceCount = new int[textResources.Length];
                        need = new int[textResources.Length];
                        textBoxResult.Text = "Available resources" + Environment.NewLine;

                        for (int i = 1; i != numberOfLines + 1; i++)
                        {
                            string Line = File.ReadLines(filename).Skip(i).First();
                            string[] Process = Line.Split(' ');

                            for (int k = 1; k != textResources.Length + 1; k++)
                            {
                                resourceCount[k - 1] = Convert.ToInt32(Process[k]);
                                resources[k - 1] -= resourceCount[k - 1];
                            }
                        }

                        for (int i = 0; i != resources.Length; i++)
                        {
                            textBoxResult.Text += Convert.ToString(resources[i]) + " ";
                        }

                        bool[] wasChecked = new bool[numberOfLines];

                        for (int a = 0; a != numberOfLines; a++)
                        {
                            wasChecked[a] = false;
                        }

                        for (int i = 1; i != numberOfLines + 1; i++)
                        {
                            string line = File.ReadLines(filename).Skip(i).First();
                            string[] process = line.Split(' ');

                            for (int k = 1; k != textResources.Length + 1; k++)
                            {
                                resourceCount[k - 1] = Convert.ToInt32(process[k]);
                            }

                            for (int k = textResources.Length + 1; k != process.Length; k++)
                            {
                                need[k - textResources.Length - 1] = Convert.ToInt32(process[k]);
                            }

                            int checkLength = 0;

                            for (int a = 0; a != resources.Length; a++)
                            {
                                if ((resources[a] + resourceCount[a] >= need[a]) && wasChecked[i - 1] == false)
                                {
                                    checkLength++;
                                }

                                if (checkLength == resources.Length)
                                {
                                    textBoxResult.Text += Environment.NewLine + "Started process " + alphabet[i - 1] + ": " + Environment.NewLine;

                                    wasChecked[i - 1] = true;

                                    for (int j = 0; j != resources.Length; j++)
                                    {
                                        resources[j] += resourceCount[j];
                                        textBoxResult.Text += Convert.ToString(resources[j]) + " ";
                                    }

                                    for (int c = 0; c != numberOfLines; c++)
                                    {
                                        if (wasChecked[c] == false)
                                            checker = true;
                                    }

                                    if (checker != false)
                                    {
                                        i = 0;
                                    }

                                    else
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Incorrect data from file");
                }
            }

            else
            {
                MessageBox.Show("Unable to read file");
            }

            if (checker != false)
            {
                textBoxResult.Text += Environment.NewLine + "This system is stable ";
            }

            else
            {
                textBoxResult.Text += Environment.NewLine + "Unstable system. Deadlock ";
            }
        }

    }
}

