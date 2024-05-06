using System;
using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class CN_Profesional
    {
        private CD_Profesional objetoCD = new CD_Profesional();
        public DataTable MostarProf()
        {
            DataTable tabla = new DataTable();
            tabla = objetoCD.Mostrar();
            return tabla;
        }
    }
}
