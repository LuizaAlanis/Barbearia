using Barbearia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Barbearia.Dados
{
    public class acoesBarbeiro
    {
        conexao con = new conexao();

        public void inserirBarbeiro(modelBarbeiro cm)
        {

            MySqlCommand cmd = new MySqlCommand("call spInsBarbeiro(@cod,@nome)", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = cm.codBarbeiro;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nomeBarbeiro;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<modelBarbeiro> BuscarBarbeiro()
        {
            List<modelBarbeiro> Barbeirolist = new List<modelBarbeiro>();

            MySqlCommand cmd = new MySqlCommand("select * from tbBarbeiro", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Barbeirolist.Add(
                    new modelBarbeiro
                    {
                        codBarbeiro = Convert.ToString(dr["codBarbeiro"]),
                        nomeBarbeiro = Convert.ToString(dr["nomeBarbeiro"]),
                    });
            }
            return Barbeirolist;
        }

        public bool DeleteBarbeiro(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbBarbeiro where codBarbeiro = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool atualizaBarbeiro(modelBarbeiro smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbBarbeiro set nomeBarbeiro=@nome where codBarbeiro=@cod", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@cod", smodel.codBarbeiro);
            cmd.Parameters.AddWithValue("@nome", smodel.nomeBarbeiro);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}