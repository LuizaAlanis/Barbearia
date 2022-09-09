using Barbearia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Barbearia.Dados
{
    public class acoesReserva
    {
        conexao con = new conexao();

        public void TestarAgenda(modelReserva agenda) //verificar se a agenda está reservada     
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbReserva where dataReserva = @data and horaReserva = @hora", con.MyConectarBD());

            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = agenda.dataReserva;
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaReserva;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    agenda.confReserva = "0";
                }

            }

            else
            {
                agenda.confReserva = "1";
            }

            con.MyDesconectarBD();
        }








        public void inserirReserva(modelReserva cm) // Cadastrar a reserva no BD
        {

            MySqlCommand cmd = new MySqlCommand("call spInsReserva(@cliente,@barbeiro,@data,@hora)", con.MyConectarBD());
            cmd.Parameters.Add("@data", MySqlDbType.VarChar).Value = DateTime.Parse(cm.dataReserva).ToString("yyyy-MM-dd");
            cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = cm.horaReserva;
            cmd.Parameters.Add("@cliente", MySqlDbType.VarChar).Value = cm.codCli;
            cmd.Parameters.Add("@barbeiro", MySqlDbType.VarChar).Value = cm.codBarbeiro;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }



        public List<modelReserva> BuscarReserva()
        {
            List<modelReserva> Atendlist = new List<modelReserva>();

            MySqlCommand cmd = new MySqlCommand("select * from tbReserva", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Atendlist.Add(
                    new modelReserva
                    {
                        codReserva = Convert.ToString(dr["codReserva"]),
                        dataReserva = Convert.ToString(dr["dataReserva"]),
                        horaReserva = Convert.ToString(dr["horaReserva"]),
                        codCli = Convert.ToString(dr["codCli"]),
                        codBarbeiro = Convert.ToString(dr["CodBarbeiro"])
                    });
            }
            return Atendlist;
        }




        public List<modelReserva> GetReservaCons()
        {
            List<modelReserva> Atendlist = new List<modelReserva>();

            MySqlCommand cmd = new MySqlCommand("select * from vwReserva", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Atendlist.Add(
                    new modelReserva
                    {
                        codReserva = Convert.ToString(dr["Codigo"]),
                        dataReserva = DateTime.Parse(Convert.ToString(dr["Data"])).ToString("yyyy-MM-dd"),
                        dataReservaF = DateTime.Parse(Convert.ToString(dr["Data"])).ToString("dd/MM/yyyy"),
                        horaReserva = Convert.ToString(dr["Hora"]),
                        nomeCli = Convert.ToString(dr["Cliente"]),
                        nomeBarbeiro = Convert.ToString(dr["Barbeiro"])
                    });
            }
            return Atendlist;
        }







        public bool DeleteReserva(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbReserva where codReserva = @id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }



        public bool atualizaReserva(modelReserva smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbReserva set dataReserva = @dataReserva, horaReserva = @horaReserva where codReserva = @cod", con.MyConectarBD());


            cmd.Parameters.AddWithValue("@dataReserva", smodel.dataReserva);
            cmd.Parameters.AddWithValue("@horaReserva", smodel.horaReserva);
            cmd.Parameters.AddWithValue("@cod", smodel.codReserva);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}