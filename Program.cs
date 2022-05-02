using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//using TabsForForm;

namespace timp_lab2
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CreateMenu menu = new CreateMenu("C:\\Users\\druzh\\source\\repos\\timp_lab2\\menu.txt");
            menu.CreateForm();
        }
    }
}
