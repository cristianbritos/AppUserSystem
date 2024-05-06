using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;


namespace CapaDatos
{
    public class CD_UserSis
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();
        //ENCRIPTAR
        private string passPhrase = "Job@Group";        // can be any string
        private string saltValue = "s@1tVaNFG";        // can be any string
        private string hashAlgorithm = "SHA1";             // can be "MD5"
        private int passwordIterations = 2;                  // can be any number
        private string initVector = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        private int keySize = 256;                      // can be 192 or 128

        public DataTable Mostrar()
        {
            tabla.Reset();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "Select id, usuario, password, tipo, apeYnom, email, mat From CliPlaUser";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            if (tabla.Rows.Count > 0)
                Console.WriteLine("EXISTEN REGISTROS!!!!");
            conexion.CerrarConexion();
            return tabla;
        }
        public string Encrypt(string plainText)
        {
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
            byte[] keyBytes = password.GetBytes(keySize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();

            symmetricKey.Mode = CipherMode.CBC;

            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);


            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            byte[] cipherTextBytes = memoryStream.ToArray();

            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherTextBytes);

            return cipherText;
        }
        public void InsertarUser(string usr, string pass, int tip, string ape, string email, int mat)
        {
            comando.Connection = conexion.AbrirConexion();
            comando.Parameters.Clear();

            comando.CommandText = "INSERT INTO CliPlaUser (usuario, password, tipo, apeYnom, email, mat) VALUES (@usr, @pass, @tip, @ape, @email, @mat)";
            comando.CommandType = CommandType.Text;

            comando.Parameters.AddWithValue("@usr", usr);
            comando.Parameters.AddWithValue("@pass", pass);
            comando.Parameters.AddWithValue("@tip", tip);
            comando.Parameters.AddWithValue("@ape", ape);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@mat", mat);

            comando.ExecuteNonQuery();
        }
        public int BuscarUser(int mat)
        {
            int res = 0;
            tabla.Reset();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "Select id, mat From CliPlaUser Where mat='" + mat + "'";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            if (tabla.Rows.Count > 0)
                res = Convert.ToInt32(tabla.Rows[0]["id"]);
            conexion.CerrarConexion();
            return res;
        }

    }
}
