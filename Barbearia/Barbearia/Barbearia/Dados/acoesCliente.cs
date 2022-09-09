using Barbearia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Barbearia.Dados
{
    public class acoesCliente
    {
        conexao con = new conexao();

        public void inserirCliente(modelCliente cm)
        {

            MySqlCommand cmd = new MySqlCommand("call spInsCli(@cod,@nome,@telefone,@celular,@email)", con.MyConectarBD());
            cmd.Parameters.Add("@cod", MySqlDbType.VarChar).Value = cm.codCli;
            cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = cm.nomeCli;
            cmd.Parameters.Add("@telefone", MySqlDbType.VarChar).Value = cm.telefoneCli;
            cmd.Parameters.Add("@celular", MySqlDbType.VarChar).Value = cm.celularCli;
            cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = cm.emailCli;

            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public List<modelCliente> BuscarCliente()
        {
            List<modelCliente> Clientlist = new List<modelCliente>();

            MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                Clientlist.Add(
                    new modelCliente
                    {
                        codCli = Convert.ToString(dr["codCli"]),
                        nomeCli = Convert.ToString(dr["nomeCli"]),
                        telefoneCli = Convert.ToString(dr["telefoneCli"]),
                        celularCli = Convert.ToString(dr["celularCli"]),
                        emailCli = Convert.ToString(dr["emailCli"])
                    });
            }
            return Clientlist;
        }

        public bool DeleteCliente(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbCliente where codCli = @id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool atualizaCliente(modelCliente smodel)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCliente set nomeCli=@nome, telefoneCli=@telefone, celularCli=@celular, emailCli=@email where codCli=@cod", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@cod", smodel.codCli);
            cmd.Parameters.AddWithValue("@nome", smodel.nomeCli);
            cmd.Parameters.AddWithValue("@telefone", smodel.telefoneCli);
            cmd.Parameters.AddWithValue("@celular", smodel.celularCli);
            cmd.Parameters.AddWithValue("@email", smodel.emailCli);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}