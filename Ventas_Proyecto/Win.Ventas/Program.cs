using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Win.Ventas
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMenú());
        }

        public static byte[] imageToByteArray (Image  imageIn)//para capturar la imagen
        {
            var ms = new MemoryStream();

            imageIn.Save(ms, imageIn.RawFormat);

            return ms.ToArray();
        }
    }
}
