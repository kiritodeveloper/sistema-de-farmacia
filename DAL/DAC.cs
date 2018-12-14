using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class DAC
    {

        private String servidor;
        private String basedatos;
        private String usuario;
        private String contrasena;
        private SqlCommand cmdSP;

        public DAC()
        { //constructor
            
            this.servidor = "DESKTOP-8FGF96I\\SQLEXPRESS";
            this.usuario = "kirito";
            this.contrasena = "";
            this.basedatos = "bd_farmacia";
            this.cmdSP = new SqlCommand();
        }

        public SqlConnection conectar()
        {
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = "server=DESKTOP-8FGF96I\\SQLEXPRESS ; user id=kirito;database=bd_farmacia ; integrated security = true";
            connect.Open();
            return connect;
        }
        public void desconectar()
        {
            SqlConnection cnx = this.conectar();
            cnx.Close();
        }

        public void PrepararSP(String sp)
        {
            //procedimiento almacenado
            cmdSP.Connection = conectar();
            cmdSP.CommandType = CommandType.StoredProcedure;
            cmdSP.CommandText = sp;
        }

        public void AddParametro(String param, String valor)
        {
            SqlParameter par = new SqlParameter();
            par.ParameterName = param;
            par.Value = valor;
            cmdSP.Parameters.Add(par);
        }

        public void ejecutarSP()
        {
            SqlDataReader spResult;
            cmdSP.Prepare();
            spResult = cmdSP.ExecuteReader();
        }

        public void ejecutarSQL(String s, String nTable, DataSet ds)
        {
            SqlDataAdapter sqlAdapter;
            sqlAdapter = new SqlDataAdapter(s, conectar());
            sqlAdapter.Fill(ds, nTable);
            desconectar();
        }
    }
}
