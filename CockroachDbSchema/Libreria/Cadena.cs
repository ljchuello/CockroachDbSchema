using System;
using System.Text.RegularExpressions;
using System.Web;

namespace CockroachDbSchema.Libreria
{
    public static class Cadena
    {
        public static string EliminarCaracteresEspeciales(string a)
        {
            try
            {
                a = Regex.Replace(a, "[^a-zA-Z0-9_.-]+", string.Empty, RegexOptions.Compiled);
                return a;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool EsInt(string pSeudoNumero)
        {
            try
            {
                Convert.ToInt32(pSeudoNumero);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static string Aleatoria(int largo)
        {
            Random random = new Random();
            string combinaciones = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            int longitud = combinaciones.Length;
            string nuevacadena = null;
            for (int i = 0; i < largo; i++)
            {
                char letra = combinaciones[random.Next(longitud)];
                nuevacadena += letra.ToString();
            }
            return nuevacadena;
        }

        public static string CodificarUrl(string cadena)
        {
            cadena = HttpUtility.UrlEncode(cadena);
            return cadena;
        }

        public static string DecodificarUrl(string cadena)
        {
            cadena = HttpUtility.UrlDecode(cadena);
            return cadena;
        }

        public static string Depurar(string cadena)
        {
            cadena = HttpUtility.HtmlDecode(cadena);
            cadena = cadena.Replace("&#160;", string.Empty);
            cadena = cadena.Replace("&nbsp;", string.Empty);
            cadena = cadena.Trim();
            return cadena;
        }

        public static string GenerarId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool Vacia(string a)
        {
            try
            {
                // Es nulo??
                if (a == null)
                {
                    return true;
                }

                // Depuramos
                a = Depurar(a);

                // IsNullOrEmpty
                if (string.IsNullOrEmpty(a))
                {
                    return true;
                }

                // IsNullOrWhiteSpace
                if (string.IsNullOrWhiteSpace(a))
                {
                    return true;
                }

                // Vacio
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}