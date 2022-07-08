using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SudokuCore;
namespace UI
{
    public partial class MainForm : Form
    {
        int count=0;
        CoreLayer CL;
        GroupBox gbox;
        Control ctn;
        public MainForm()
        {
            
            gbox = new GroupBox();
            ctn = new Control();
            CL = new CoreLayer();

            InitializeComponent();

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            Entities.OutputTypes output = CheckTextBox();
            if (output==Entities.OutputTypes.AllNumber)
            {
                count = 0;
                int oldcount = 0;
                int infloopbreaker = 0;
                while (true)
                {
                    updateLists();
                    for (int i = 0; i < 9; i++)
                    {
                        for (int j = 0; j < 9; j++)
                        {
                            if (CL.IsZero(i, j))
                            {
                                List<int> l1 = new List<int>();
                                l1 = BoxSelect(i, j);
                                CL.ControlHorizontal(i, j, l1);
                                CL.ControlVertical(i, j, l1);
                            }
                            else
                            {
                                count++;
                            }
                        }
                    }
                    if (count == 81 || infloopbreaker==2)
                    {
                        CL.UpdateSudoku();
                        updateTextboxes();
                        break;
                    }                    
                    else if (oldcount == count)
                    {
                        count = 0;
                        infloopbreaker ++;
                        for (int i = 0; i < 9; i++)
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                if (CL.Sudoku[i][j] == 0)
                                {
                                    CL.NewControlHorizontal(i, j);
                                    CL.NewControlVertical(i, j);
                                    CL.NewControlBox(i, j);
                                }

                            }
                        }
                    }
                    else
                    {
                        infloopbreaker =0;
                        oldcount = count;
                        count = 0;
                    }
                    CL.UpdateSudoku();
                    updateTextboxes();
                }
                if (infloopbreaker==2)
                {
                    MessageBox.Show("Couldn't solved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if(count==81)
                {
                    MessageBox.Show("Solved", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else if (output==Entities.OutputTypes.NotBetween1and9)
            {
                MessageBox.Show("Numbers must be between 1 and 9", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (output==Entities.OutputTypes.NotNumber)
            {
                MessageBox.Show("Please enter number only", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (output == Entities.OutputTypes.NullOrEmpty)
            {
                MessageBox.Show("Please make sure you enter the values", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> TakeList(int id)
        {

            string txtbox;
            
            int value;
            List<int> list = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                txtbox = $"textBox_{id.ToString()}{i.ToString()}";
                ctn = Controls.Find(txtbox, true).FirstOrDefault();
                value = Convert.ToInt32(ctn.Text);
                list.Add(value);
                
            }
            return list;
        }

        private List<int> BoxSelect(int x, int y)
        {
            List<int> l1 = new List<int>();

            if (x < 3 && y < 3)
            {
                gbox = groupBox1;
            }
            else if (y >= 3 && y < 6 && x < 3)
            {
                gbox = groupBox2;
            }
            else if (y >= 6 && x < 3)
            {
                gbox = groupBox3;
            }
            else if (y < 3 && x >= 3 && x < 6)
            {
                gbox = groupBox4;
            }
            else if (y >= 3 && y < 6 && x >= 3 && x < 6)
            {
                gbox = groupBox5;
            }
            else if (y >= 6 && x >= 3 && x < 6)
            {
                gbox = groupBox6;
            }
            else if (y < 3 && x >= 6)
            {
                gbox = groupBox7;
            }
            else if (y >= 3 && y < 6 && x >= 6)
            {
                gbox = groupBox8;
            }
            else if (y >= 6 && x >= 6)
            {
                gbox = groupBox9;
            }

            foreach (TextBox item in gbox.Controls)
            {
                l1.Add(Convert.ToInt32(item.Text));
            }

            l1.Reverse();
            return l1;
        }

        private void updateTextboxes()
        {
            string txtbox;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    txtbox = $"textBox_{i.ToString()}{j.ToString()}";
                    ctn = Controls.Find(txtbox, true).FirstOrDefault();
                    if (ctn.Text=="0")
                    {
                        ctn.Text = CL.Sudoku[i][j].ToString();
                        ctn.BackColor = Color.LightGray;
                    }
                }
            }
            
        }
        
        private void updateLists()
        {
            CL.Sudoku = new List<List<int>>();
            for (int i = 0; i < 9; i++)
            {
                CL.Sudoku.Add(TakeList(i));
            }
            CL.AllPossibleAnswers = new Dictionary<int, List<int>>();
        }

        private void button_Clear_Click(object sender, EventArgs e)
        {
            clrText();
        }

        private void clrText()
        {
            string txtbox;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    txtbox = $"textBox_{i.ToString()}{j.ToString()}";
                    ctn = Controls.Find(txtbox, true).FirstOrDefault();
                    ctn.Text = "0";
                    ctn.BackColor = Color.White;
                }
            }
        }

        private Entities.OutputTypes CheckTextBox()
        {
            string txtbox;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    txtbox = $"textBox_{i.ToString()}{j.ToString()}";
                    ctn = Controls.Find(txtbox, true).FirstOrDefault();
                    if (string.IsNullOrEmpty(ctn.Text))
                    {
                        return Entities.OutputTypes.NullOrEmpty;
                    }
                    else
                    {
                        try
                        {
                            int value = Convert.ToInt32(ctn.Text);
                            if (value<0 || value>9)
                            {
                                return Entities.OutputTypes.NotBetween1and9;
                            }
                        }
                        catch (Exception)
                        {
                            return Entities.OutputTypes.NotNumber;
                        }
                    }
                }
            }
            return Entities.OutputTypes.AllNumber;
        }
    }
}
