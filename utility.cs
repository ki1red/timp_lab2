using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timp_lab2
{
    class FileDamaged : Exception
    {
        public FileDamaged(string message) : base(message)
        { }
    }
    public struct Tree
    {
        public string name { get; private set; }
        public int status { get; private set; }
        public string nameMethod { get; private set; }
        public Tree[] trees { get; private set; }
        public int index { get; private set; }
        public const int size = 10;
        public bool isRoot { get; private set; }

        public Tree(string str)
        {
            string[] strs = str.Split(';');

            if (strs.Length == 3)
            {
                name = strs[1];
                status = Convert.ToInt32(strs[2]);
                isRoot = true;
                nameMethod = "\0";
            }
            else if (strs.Length == 4)
            {
                name = strs[1];
                status = Convert.ToInt32(strs[2]);
                nameMethod = strs[3];
                isRoot = false;
            }
            else
                throw new FileDamaged("Превышено количество параметров в одной или нескольких строках.");

            index = 0;
            trees = new Tree[size];
        }

        public bool AddSubparagraph(string tree)
        {
            if (isRoot)
            {
                Tree tr = new Tree(tree);
                if (index < 10)
                    trees[index] = tr;
                index++;
                return true;
            }
            return false;
        }
    }
    public class CreateMenu
    {
        string[] lines;
        private Form Form1;
        public Tree[] trees;
        int j;
        public CreateMenu(string file_path)
        {
            lines = System.IO.File.ReadAllLines(file_path);
            if (lines.Length <= 0) throw new FileDamaged("Файл \'"+ file_path +"\' пуст.");
            trees = new Tree[lines.Length];
            j = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i][0] == '0')
                {
                    j++;
                    trees[j] = new Tree(lines[i]);
                }
                else
                {
                    if (lines[j][lines[j].Length - 1] != '0' &&
                        lines[j][lines[j].Length - 1] != '1' &&
                        lines[j][lines[j].Length - 1] != '2')
                        throw new FileDamaged("В файле \'" + file_path + "\' строка №" + (i+1) + " не может принадлежать строке №" + (j+1) + ".");
                    trees[j].AddSubparagraph(lines[i]);
                }
            }
            Array.Resize(ref trees, j + 1);
        }

        public bool CreateForm()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(trees));
            }
            catch
            {
                MessageBox.Show("При создании окна возникла ошибка. Попробуйте снова.");
                return false;
            }
            return true;
        }

        public Form GetForm()
        {
            return Form1;
        }
    }

}
