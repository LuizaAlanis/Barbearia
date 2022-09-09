using Barbearia.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbearia.Dados
{
    public class acoesLogin
    {
        conexao con = new conexao();
        public void TestarUsuario(modelUsuario user)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbLogin where usuario = @usuario and senha = @Senha ", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                }
            }

            else
            {
                user.usuario = null;
                user.senha = null;
                user.tipo = null;
            }

            con.MyDesconectarBD();
        }
    }
}