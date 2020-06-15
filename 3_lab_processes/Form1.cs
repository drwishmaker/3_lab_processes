using System;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace _3_lab_processes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string text;
        int numOfLines;
        string[] textRes = null;
        int[] res = null;
        int[] count = null;
        int[] need = null;

        bool sysIsStable = false;

        string[] alphabet = new string[] { "A", "B", "C", "D", "E", "F", "H", "J" };
        
        private void buttonFile_Click(object sender, EventArgs e)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                input();
                output();
            }

            else
            {
                MessageBox.Show("Unable to read file");
            }

            
        }

        private void input()
        {
            string filename = ofd.FileName;
            text = File.ReadAllText(filename);
            textBoxDefault.Text = text + Environment.NewLine;
            string ResoursesLine = File.ReadLines(filename).Skip(0).First();
            numOfLines = File.ReadAllLines(filename).Length - 1;

            textRes = ResoursesLine.Split(' ');
            res = new int[textRes.Length];

            for (int i = 0; i != res.Length; i++)
            {
                res[i] = Convert.ToInt32(textRes[i]);
            }

            count = new int[textRes.Length];
            need = new int[textRes.Length];


            for (int i = 1; i != numOfLines + 1; i++)
            {
                string str = File.ReadLines(filename).Skip(i).First();
                string[] process = str.Split(' ');

                for (int k = 1; k != textRes.Length + 1; k++)
                {
                    count[k - 1] = Convert.ToInt32(process[k]);
                    res[k - 1] -= count[k - 1];
                }
            }


        }

        private void output()
        {
            string filename = ofd.FileName;
            textBoxResult.Text = "Available resources" + Environment.NewLine;

            for (int i = 0; i != res.Length; i++)
            {
                textBoxResult.Text += Convert.ToString(res[i]) + " ";
            }

            bool[] wasChecked = new bool[numOfLines];

            for (int a = 0; a != numOfLines; a++)
            {
                wasChecked[a] = false;
            }

            for (int i = 1; i != numOfLines + 1; i++)
            {
                string line = File.ReadLines(filename).Skip(i).First();
                string[] process = line.Split(' ');

                for (int k = 1; k != textRes.Length + 1; k++)
                {
                    count[k - 1] = Convert.ToInt32(process[k]);
                }

                for (int k = textRes.Length + 1; k != process.Length; k++)
                {
                    need[k - textRes.Length - 1] = Convert.ToInt32(process[k]);
                }

                int checkLength = 0;

                for (int a = 0; a != res.Length; a++)
                {
                    if ((res[a] + count[a] >= need[a]) && wasChecked[i - 1] == false)
                    {
                        checkLength++;
                    }

                    if (checkLength == res.Length)
                    {
                        textBoxResult.Text += Environment.NewLine + "Started process " + alphabet[i - 1] + ": " + Environment.NewLine;

                        wasChecked[i - 1] = true;

                        for (int j = 0; j != res.Length; j++)
                        {
                            res[j] += count[j];
                            textBoxResult.Text += Convert.ToString(res[j]) + " ";
                        }

                        for (int c = 0; c != numOfLines; c++)
                        {
                            if (wasChecked[c] == false)
                                sysIsStable = true;
                        }

                        if (sysIsStable != false)
                        {
                            i = 0;
                        }

                        else
                        {
                            return;
                        }

                        if (sysIsStable != false)
                        {
                            textBoxResult.Text += Environment.NewLine + "This system is stable";
                        }

                        else
                        {
                            textBoxResult.Text += Environment.NewLine + "This system is unstable";
                        }
                    }
                }
            }
        }

    }
}

