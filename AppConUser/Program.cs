using System;
using System.Data;
using CapaNegocio;

namespace AppConUser
{
    class User
    {
        protected CN_UserSis objetoCN = new CN_UserSis();
        public DataTable tabla = new DataTable();
        public User()
        {
            tabla = objetoCN.MostarUser();
            Console.WriteLine("Cargando Tabla, registros Cargado: " + tabla.Rows.Count);
        }
        public void InsertUser(string usr, string pass, string tipo, string ape, string email, string mat)
        {
            objetoCN.InsertarUser(usr, pass, tipo, ape, email, mat);
        }
        public string Encrypt(string plainText)
        {
            string res = "";
            res = objetoCN.Encrypt(plainText);
            return res;
        }
        public int BuscarUser(string mat)
        {
            int res = objetoCN.BuscarUser(mat.Trim());
            return res;
        }
    }
    class Profesional
    {
        protected CN_Profesional objetoCN = new CN_Profesional();
        public DataTable tabla = new DataTable();
        public Profesional()
        {
            tabla = objetoCN.MostarProf();
            Console.WriteLine("Cargando Tabla, registros Cargado: " + tabla.Rows.Count);
        }
    }
    class Program
    {
        
        static void Main(string[] args)
        {
            User usuario = new User();            
            Profesional profesional = new Profesional();
            foreach(DataRow fila in usuario.tabla.Rows)
            {
                Console.WriteLine(fila["id"]);
            }
            foreach (DataRow fila in profesional.tabla.Rows)
            {
                Console.WriteLine("Matricula: "+fila["medmat"]+", Profesional: "+ fila["mednom"]);
                if (usuario.BuscarUser(fila["medmat"].ToString().Trim()) ==0)
                {
                    usuario.InsertUser(fila["medmat"].ToString().Trim(), usuario.Encrypt(fila["mednom"].ToString().Trim().Substring(0, 4)), "2", fila["mednom"].ToString().Trim(), fila["medmail"].ToString().Trim(), fila["medmat"].ToString().Trim());
                    //Clave, primeras 4 letras en mayusculas del apellido
                    //usuario, matricula
                    Console.WriteLine("Usuario Agregado!!");
                }
                else
                {
                    Console.WriteLine("Usuario Existente!!");
                }
            }
            Console.ReadLine();
        }        
    }
}
