using System;
using System.Data;
using System.Data.SqlClient;


namespace CapaDatos
{
    class CD_Conexion
    {
        protected const string SQL_CONNECTION_STRING = @"Server=NCC-1701-SV\SQLSERVER;" + "DataBase=Capsule;" + "Persist Security Info=True;User ID=dax;Password=Gabriela75;Connect Timeout=15";
        private SqlConnection Conexion = new SqlConnection(SQL_CONNECTION_STRING);

        public SqlConnection AbrirConexion()
        {
            if (Conexion.State == ConnectionState.Closed)
                Conexion.Open();
            return Conexion;
        }

        public SqlConnection CerrarConexion()
        {
            if (Conexion.State == ConnectionState.Open)
                Conexion.Close();
            return Conexion;
        }

    }
}
