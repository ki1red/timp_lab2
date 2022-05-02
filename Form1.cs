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
    public partial class Form1 : Form
    {
        public Form1(Tree[] trees)
        {
            //-----------------Create menu strip

            ToolStripPanel tspTop = new ToolStripPanel(); // создание меню
            tspTop.Dock = DockStyle.Top; // прикрепление к верху
            // Создание элемента управления пункта меню
            MenuStrip ms = new MenuStrip();

            for (int i = 0; i < trees.Length; i++)
                BuildItemMenu(ref tspTop, ref ms, trees[i]);
            ms.Dock = DockStyle.Top;

            // The Form.MainMenuStrip property determines the merge target.
            this.MainMenuStrip = ms;

            // Add the ToolStripPanels to the form in reverse order.
            this.Controls.Add(tspTop);

            // Add the MenuStrip last.
            // This is important for correct placement in the z-order.
            this.Controls.Add(ms);
            //-------------------------------------

        }

        void windowNewMenu_Click(object sender, EventArgs e)
        {
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

        private void BuildItemMenu(ref ToolStripPanel tspTop, ref MenuStrip ms, Tree tree)
        {

            // Настройка статуса пункта
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
            else
            {
                windowMenu = new ToolStripMenuItem(tree.name, null, new EventHandler(windowNewMenu_Click));
                ItemStatus(ref windowMenu, tree.status);
            }

            ms.MdiWindowListItem = windowMenu;

            // Add the window ToolStripMenuItem to the MenuStrip.
            ms.Items.Add(windowMenu);


        }
    }
    
}
