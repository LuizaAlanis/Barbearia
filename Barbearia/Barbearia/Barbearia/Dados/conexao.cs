﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Barbearia.Dados
{
    public class conexao
    {
        MySqlConnection cn = new MySqlConnection("Server = localhost ; DataBase = bdBarbearia ; User = userBarbearia ; pwd = 1234567");
        public static string msg;

        public MySqlConnection MyConectarBD()
        {

            try
            {
                cn.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }

        public MySqlConnection MyDesconectarBD()
        {

            try
            {
                cn.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }
    }
}