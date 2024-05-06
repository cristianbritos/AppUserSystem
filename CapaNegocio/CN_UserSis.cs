using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_UserSis
    {
        private CD_UserSis objetoCD = new CD_UserSis();
        public DataTable MostarUser()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla; 
        }
        public void InsertarUser(string usr, string pass, string tip, string ape, string email, string mat)
        {
            objetoCD.InsertarUser(usr.Trim(), pass.Trim(), Convert.ToInt32(tip), ape.Trim(), email.Trim(), Convert.ToInt32(mat));
        }
        public string Encrypt(string plainText)
        {
            string res = "";
            res=objetoCD.Encrypt(plainText);
            return res;
        }
        public int BuscarUser(string mat)
        {
            int res = objetoCD.BuscarUser(Convert.ToInt32(mat));
            return res;
        }
    }    
}
