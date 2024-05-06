using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Profesional
    {
        private CD_Conexion conexion = new CD_Conexion();
        SqlDataReader leer;
        DataTable tabla = new DataTable();
        SqlCommand comando = new SqlCommand();

        public DataTable Mostrar()
        {
            tabla.Reset();
            comando.Connection = conexion.AbrirConexion();
            comando.CommandText = "Select medmat, mednom, meddoc, medmail From Medicos where medmat>738";
            leer = comando.ExecuteReader();
            tabla.Load(leer);
            if (tabla.Rows.Count > 0)
                Console.WriteLine("EXISTEN REGISTROS!!!!");
            conexion.CerrarConexion();
            return tabla;
        }
    }
}
