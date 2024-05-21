using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConstanciaNoInhabilitado.Shared.Entities.RegistroInhabilitacion
{
    public class Security
    {
        private static bool invalid = false;
        private const String EXP_RFC = @"([A-Z,Ñ,&]{3,4}([0-9]{2})(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1]))([A-Z|\d]{3})?$";
        private const String EXP_CURP = @"[A-Z][A,E,I,O,U,X][A-Z]{2}[0-9]{2}[0-1][0-9][0-3][0-9][M,H][A-Z]{2}[B,C,D,F,G,H,J,K,L,M,N,Ñ,P,Q,R,S,T,V,W,X,Y,Z]{3}[0-9,A-Z][0-9]";
        private const String EXP_NOMBRES = @"[A-ZÁÉÍÓÚÑ]+([\w\s][A-ZÁÉÍÓÚÑ]+)+$";
        private const String EXP_ALFA = @"[a-zA-Z0-9 ñáéíóúÑÁÉÍÓÚ]{2,150}";
        private const String EXP_REFERENCIA = @"[0-9]{20}";


        /// <summary>
        /// Encripta una cadena con md5
        /// </summary>
        /// <param name="md5Hash">Objeto MD5</param>
        /// <param name="input">Cadena a encriptar</param>
        /// <returns>Cadena encriptada por MD5</returns>
        public static string GetMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        /// <summary>
        /// Determina si una referencia tiene el formato correcto
        /// </summary>
        /// <param name="s">Cadena a evaluar</param>
        /// <returns>Verdadero si tiene el formato correcto, de lo contrario sera falso</returns>
        public static bool IsValidReference(String s)
        {
            try
            {
                return Regex.IsMatch(s, EXP_REFERENCIA);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// Determina si un correo electronico es valido
        /// </summary>
        /// <param name="strIn">Correo Electronico</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsValidEmail(string strIn)
        {
            invalid = false;
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (invalid) return false;

            // Return true if strIn is in valid e-mail format.
            try
            {
                return Regex.IsMatch(strIn,
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,24}))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }


        /// <summary>
        /// Verifica si el nombre o apellido contiene caracteres permitidos
        /// </summary>
        /// <param name="s">Cadena</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool IsValidName(String s)
        {
            try
            {
                return Regex.IsMatch(s, EXP_NOMBRES);
            }
            catch
            {
                return false;
            }

        }

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                invalid = true;
            }
            return match.Groups[1].Value + domainName;
        }

        /// <summary>
        /// Determina si una cadena es alfanumerica
        /// </summary>
        /// <param name="s">Cadena a Evaluar</param>
        /// <returns></returns>
        public static bool EsAlfaNumerico(String s)
        {
            try
            {
                return Regex.IsMatch(s, EXP_ALFA);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Valida rfc con o sin homoclave
        /// </summary>
        /// <param name="rfc">RFC</param>
        /// <returns>Verdadero o falso</returns>
        public static bool ValidarRFC(String rfc)
        {
            return Regex.IsMatch(rfc, EXP_RFC);
        }

        /// <summary>
        /// Valida si una CURP tiene el formato correcto
        /// </summary>
        /// <param name="curp">CURP</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool ValidarCURP(String curp)
        {
            return Regex.IsMatch(curp, EXP_CURP);
        }

        /// <summary>
        /// Elimina caracteres 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FormatoCadena(string s)
        {
            try
            {
                string response = s.Replace("Ã ", "Ñ").Replace("Ã±", "Ñ").Replace(".", "");
                return response;
            }
            catch
            {
                return s;
            }
        }


    }
}
