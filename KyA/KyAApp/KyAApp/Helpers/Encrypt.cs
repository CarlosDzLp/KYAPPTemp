using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace KyAApp.Helpers
{
    public static class Encrypt
    {
        // se define el tamaño del key y del vector
        private static readonly int tamanyoClave = 32;
        private static readonly int tamanyoVector = 16;
        // Define la palabra clave para el cifrado y
        private static readonly string Clave = "no desbloquee el password";
        private static readonly string Vector = "servicio de kyapp27092019";
        // se convierte el vector y la key a bytes
        public static byte[] Key = UTF8Encoding.UTF8.GetBytes(Clave);
        public static byte[] IV = UTF8Encoding.UTF8.GetBytes(Vector);

        public static string Crypt(string txtPlano)
        {
            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);
            // se instancia el Rijndael
            Rijndael RijndaelAlg = Rijndael.Create();
            // se establece cifrado
            MemoryStream memoryStream = new MemoryStream();
            // se crea el flujo de datos de cifrado
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateEncryptor(Key, IV),
                CryptoStreamMode.Write);
            // se obtine la información a cifrar
            byte[] txtPlanoBytes = UTF8Encoding.UTF8.GetBytes(txtPlano);
            // se cifran los datos
            cryptoStream.Write(txtPlanoBytes, 0, txtPlanoBytes.Length);
            cryptoStream.FlushFinalBlock();
            // se obtinen los datos cifrados
            byte[] cipherMessageBytes = memoryStream.ToArray();
            // se cierra todo
            memoryStream.Close();
            cryptoStream.Close();
            // Se devuelve la cadena cifrada
            return Convert.ToBase64String(cipherMessageBytes);
        }
        /**
		 * Descifra una cadena texto con el algoritmo de Rijndael
		 * 
		 * @param	txtCifrado	mensaje cifrado
		 * @return	string				texto descifrado (plano)
		 */
        public static string DeCrypt(string txtCifrado)
        {
            Array.Resize(ref Key, tamanyoClave);
            Array.Resize(ref IV, tamanyoVector);
            // se obtienen los bytes para el cifrado
            byte[] cipherTextBytes = Convert.FromBase64String(txtCifrado);
            // se almacenan los datos descifrados
            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            // se crea una instancia del Rijndael			
            Rijndael RijndaelAlg = Rijndael.Create();
            // se crean los datos cifrados
            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
            // se descifran los datos
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                RijndaelAlg.CreateDecryptor(Key, IV),
                CryptoStreamMode.Read);
            // se obtienen datos descifrados
            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            // se cierra todo
            memoryStream.Close();
            cryptoStream.Close();
            // se devuelve los datos descifrados
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }
    }
}
