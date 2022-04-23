using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timp_lab2
{

    //class Menu()
    //{
    //    string fileName;









    //}

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
            string[] strs = str.Split(' ');

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
                throw new IndexOutOfRangeException("Error: Is not paragraph.");

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

    public partial class Form1 : Form
    {
        public Form1(string file_path)
        {
            string[] lines = System.IO.File.ReadAllLines(file_path);
            Tree[] trees = new Tree[lines.Length];
            int j = -1;

            for (int i = 0; i < lines.Length; i++)
            {
                
                if (lines[i][0] == '0')
                {
                    j++;
                    trees[j] = new Tree(lines[i]);
                }
                else
                {
                    trees[j].AddSubparagraph(lines[i]);
                }
            }
            Array.Resize(ref trees, j + 1);


            //-----------------Create menu strip

            ToolStripPanel tspTop = new ToolStripPanel(); // создание меню
            tspTop.Dock = DockStyle.Top; // прикрепление к верху
            // Создание элемента управления пункта меню
            //ToolStrip tsTop = new ToolStrip();
            //tsTop.Items.Add("Menu"); // добавление имени
            //tspTop.Join(tsTop); // прикрепление к панели
            MenuStrip ms = new MenuStrip();

            for (int i = 0; i < trees.Length; i++)
                BuildItemMenu(ref tspTop,ref ms, trees[i]);
             ms.Dock = DockStyle.Top;

            // The Form.MainMenuStrip property determines the merge target.
            this.MainMenuStrip = ms;

            // Add the ToolStripPanels to the form in reverse order.
            this.Controls.Add(tspTop);

            // Add the MenuStrip last.
            // This is important for correct placement in the z-order.
            this.Controls.Add(ms);
            //-------------------------------------
            /*ToolStripPanel tspTop = new ToolStripPanel(); // создание меню
            tspTop.Dock = DockStyle.Top; // прикрепление к верху

            // блок добавляющий пункт в область меню 
            ToolStrip tsTop = new ToolStrip();
            tsTop.Items.Add("Top");
            tspTop.Join(tsTop);

            // блок создающий новую вкладку по клику
            MenuStrip ms = new MenuStrip();
            ToolStripMenuItem windowMenu = new ToolStripMenuItem("Window");
            ToolStripMenuItem windowNewMenu = new ToolStripMenuItem("New", null, new EventHandler(windowNewMenu_Click)); // тут менять название меню или название метода
            windowMenu.DropDownItems.Add(windowNewMenu);
            ((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowImageMargin = false;
            ((ToolStripDropDownMenu)(windowMenu.DropDown)).ShowCheckMargin = true;

            // Assign the ToolStripMenuItem that displays 
            // the list of child forms.
            ms.MdiWindowListItem = windowMenu;

            // Add the window ToolStripMenuItem to the MenuStrip.
            ms.Items.Add(windowMenu);

            // Dock the MenuStrip to the top of the form.
            ms.Dock = DockStyle.Top;

            // The Form.MainMenuStrip property determines the merge target.
            this.MainMenuStrip = ms;

            // Add the ToolStripPanels to the form in reverse order.
            //this.Controls.Add(tspRight);
            //this.Controls.Add(tspLeft);
            //this.Controls.Add(tspBottom);
            //this.Controls.Add(tspTop);

            // This is important for correct placement in the z-order.
            this.Controls.Add(ms);
            
            //menu.*/
        }

        void windowNewMenu_Click(object sender, EventArgs e)
        {
            //Form f = new Form();
            //f.MdiParent = this;
            //f.Text = "Form - " + this.MdiChildren.Length.ToString();
            //f.Show();
            MessageBox.Show((sender as ToolStripMenuItem).Text);
        }

        private void ItemStatus(ref ToolStripMenuItem item, int status)
        {
            if (status == 2)
            {
                item.Visible = false;
                item.Enabled = false;
            }
            else if (status == 1)
            {
                item.Visible = true;
                item.Enabled = false;
            }
            else if (status == 0)
            {
                item.Visible = true;
                item.Enabled = true;
            }
            // Некорректный status
            else throw new ArgumentException("Wrong status value.");
            
            
        }
        //in tree class

        private void BuildItemMenu (ref ToolStripPanel tspTop, ref MenuStrip ms, Tree tree)
        {
            
            // Настройка статуса пункта
            //ItemStatus(ref tsTop, tree.status);
            ToolStripMenuItem windowMenu;
            if (tree.isRoot) // если есть подпункты
            {
                // Создаём меню подпунктов
   
                windowMenu = new ToolStripMenuItem(tree.name); // добавление имени
                // Добавление подпунктов пункта
                foreach (var tr in tree.trees)
                {
                    // Если элементов больше нет
                    if (tr.name == null)
                        break;
                    // Создаем подпункт
                    ToolStripMenuItem windowNewMenu = new ToolStripMenuItem(
                        tr.name, null, new EventHandler(windowNewMenu_Click)); // тут менять название меню или название метода
                    windowMenu.DropDownItems.Add(windowNewMenu); // добавляем подпункт в еню подпунктов
                    ((ToolStripDropDownMenu)(windowNewMenu.DropDown)).ShowImageMargin = false;
                    ((ToolStripDropDownMenu)(windowNewMenu.DropDown)).ShowCheckMargin = true;

                    ItemStatus(ref windowNewMenu, tr.status); // настраиваем статус подпункта
                }
            }
            else{
                windowMenu = new ToolStripMenuItem(tree.name, null, new EventHandler(windowNewMenu_Click));
                ItemStatus(ref windowMenu, tree.status);
            }

        ms.MdiWindowListItem = windowMenu;

        // Add the window ToolStripMenuItem to the MenuStrip.
        ms.Items.Add(windowMenu);

        // Dock the MenuStrip to the top of the form.
       

        }
        /*
         * InitializeComponent();
 
        ToolStripMenuItem fileItem = new ToolStripMenuItem("Файл");
 
        ToolStripMenuItem newItem = new ToolStripMenuItem("Создать") { Checked = true, CheckOnClick = true };
        fileItem.DropDownItems.Add(newItem);
 
 
        ToolStripMenuItem saveItem = new ToolStripMenuItem("Сохранить") { Checked = true, CheckOnClick = true };
        saveItem.CheckedChanged += menuItem_CheckedChanged;
 
        fileItem.DropDownItems.Add(saveItem);
 
        menuStrip1.Items.Add(fileItem);
        */
    }
}
