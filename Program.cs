using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timp_lab2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1("C:\\Users\\druzh\\source\\repos\\timp_lab2\\menu.txt"));


/*
            for row in strings:
                if len(row.Split(' ')) == 3:
                    number, name, status = row.Split(' ');
                elif row.Split(' ') == 4:
                    number, name, status, method_name = row.Split(' ');
                else: 
                    raise error;
            
                addMenuTitle(number, name, status, method_name);
*/

        }
    }
}
