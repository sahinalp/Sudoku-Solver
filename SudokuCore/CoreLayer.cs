using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuCore
{
    public class CoreLayer
    {
        public Dictionary<int, List<int>> AllPossibleAnswers;
        public List<List<int>> Sudoku;
        List<int> Answers;
        
        public void ControlHorizontal(int x, int y, List<int> Box)
        {
            Answers = new List<int>();
            bool IsThereKey = AllPossibleAnswers.ContainsKey(10 * x + y);
            if (!IsThereKey)
            {
                for (int i = 1; i <= 9; i++)
                {
                    Answers.Add(i);
                }
            }
            else
            {
                CreateAnswerList(x, y);
            }

            for (int i = 0; i < 9; i++)
            {
                if (i != y)
                {
                    if (Answers.Contains(Sudoku[x][i]))
                    {
                        Answers.Remove(Sudoku[x][i]);
                    }
                }
            }
            ControlBox(Answers, Box);
            if (IsThereKey)
            {
                AllPossibleAnswers.Remove(10 * x + y);
            }
            AllPossibleAnswers.Add(10 * x + y, Answers);

        }

        public void ControlVertical(int x, int y, List<int> Box)
        {
            Answers = new List<int>();
            bool IsThereKey = AllPossibleAnswers.ContainsKey(10 * x + y);
            if (!IsThereKey)
            {
                for (int i = 1; i <= 9; i++)
                {
                    Answers.Add(i);
                }
            }
            else
            {
                CreateAnswerList(x, y);
            }

            for (int i = 0; i < 9; i++)
            {
                if (i != x)
                {
                    if (Answers.Contains(Sudoku[i][y]))
                    {
                        Answers.Remove(Sudoku[i][y]);
                    }
                }
            }
            ControlBox(Answers, Box);
            if (IsThereKey)
            {
                AllPossibleAnswers.Remove(10 * x + y);
            }
            AllPossibleAnswers.Add(10 * x + y, Answers);

        }

        public List<int> ControlBox(List<int> Answers, List<int> Box)
        {
            foreach (int item in Box)
            {
                if (Answers.Contains(item))
                {
                    Answers.Remove(item);
                }
            }
            return Answers;
        }

        public bool IsZero(int x, int y)
        {
            if (Sudoku[x][y] == 0)
            {
                return true;
            }
            return false;
        }

        public void UpdateSudoku()
        {
            int x;
            int y;
            foreach (var item in AllPossibleAnswers)
            {
                if (item.Value.Count == 1)
                {
                    x = item.Key / 10;
                    y = item.Key % 10;
                    Sudoku[x][y] = item.Value[0];
                }
            }

        }

        public void NewControlHorizontal(int x, int y)
        {
            //  Horizontal
            CreateAnswerList(x, y);
            if (Answers.Count > 1)
            {

                for (int k = 0; k < 9; k++)
                {
                    if (Sudoku[x][k] == 0 && y != k)
                    {
                        foreach (int item in AllPossibleAnswers[10 * x + k])
                        {
                            if (Answers.Contains(item))
                            {
                                Answers.Remove(item);
                            }
                        }
                    }
                }
                if (Answers.Count == 1)
                {
                    AllPossibleAnswers.Remove(10 * x + y);
                    AllPossibleAnswers.Add(10 * x + y, Answers);
                }
            }


        }
        
        public void NewControlVertical(int x, int y)
        {
            CreateAnswerList(x, y);
            if (Answers.Count > 1)
            {

                for (int k = 0; k < 9; k++)
                {
                    if (Sudoku[k][y] == 0 && x != k)
                    {
                        foreach (int item in AllPossibleAnswers[10 * k + y])
                        {
                            if (Answers.Contains(item))
                            {
                                Answers.Remove(item);
                            }
                        }
                    }
                }
                if (Answers.Count == 1)
                {
                    AllPossibleAnswers.Remove(10 * x + y);
                    AllPossibleAnswers.Add(10 * x + y, Answers);
                }
            }
        }

        public void NewControlBox(int x, int y)
        {
            CreateAnswerList(x, y);
            if (Answers.Count > 1)
            {
                int SelectedBoxIndex = BoxIndex(x, y);
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {
                        if (Sudoku[i][j] == 0 && (i != x || j != y) && SelectedBoxIndex == BoxIndex(i, j))
                        {
                            foreach (int item in AllPossibleAnswers[10 * i + j])
                            {
                                if (Answers.Contains(item))
                                {
                                    Answers.Remove(item);
                                }
                            }
                        }
                    }
                }
                if (Answers.Count == 1)
                {
                    AllPossibleAnswers.Remove(10 * x + y);
                    AllPossibleAnswers.Add(10 * x + y, Answers);
                }

            }
        }
        
        public int BoxIndex(int x, int y)
        {
            int index = 0;
            if (x < 3 && y < 3)
            {
                index = 1;
            }
            else if (y >= 3 && y < 6 && x < 3)
            {
                index = 2;
            }
            else if (y >= 6 && x < 3)
            {
                index = 3;
            }
            else if (y < 3 && x >= 3 && x < 6)
            {
                index = 4;
            }
            else if (y >= 3 && y < 6 && x >= 3 && x < 6)
            {
                index = 5;
            }
            else if (y >= 6 && x >= 3 && x < 6)
            {
                index = 6;
            }
            else if (y < 3 && x >= 6)
            {
                index = 7;
            }
            else if (y >= 3 && y < 6 && x >= 6)
            {
                index = 8;
            }
            else if (y >= 6 && x >= 6)
            {
                index = 9;
            }
            return index;
        }

        public void CreateAnswerList(int x, int y)
        {
            Answers = new List<int>();
            foreach (int item in AllPossibleAnswers[10 * x + y])
            {
                Answers.Add(item);
            }
        }

    }
}
