using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.conection;
using Veterinaria.model;

namespace Veterinaria.control
{
    internal class C_Telefone
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_telefone;
        SqlDataAdapter da_telefone;
        List<Telefone> lista_telefone = new List<Telefone>();



        public List<Telefone> DadosTelefone()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();
                    lista_telefone.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_telefone;

        }



        public List<Telefone> DadosTelefoneFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnumerotelefone", parametro + "%");


            SqlDataReader dr_telefone;
            conn.Open();

            try
            {
                dr_telefone = cmd.ExecuteReader();
                while (dr_telefone.Read())
                {
                    Telefone aux = new Telefone();
                    aux.codtelefone = Int32.Parse(dr_telefone["codtelefone"].ToString());
                    aux.numerotelefone = dr_telefone["numerotelefone"].ToString();

                    lista_telefone.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_telefone;
        }


        String sqlAtualiza = "update telefone set numerotelefone = @pnome where" +
            " codtelefone= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Telefone dados = new Telefone();
            dados = (Telefone)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtelefone);
            cmd.Parameters.AddWithValue("@pnome", dados.numerotelefone);

            // cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Atualizei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            };
        }


        String sqlApaga = "delete from telefone where codtelefone = @pcod";
        public void Apaga_Dados(int aux)
        {

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlApaga, conn);
            cmd.Parameters.AddWithValue("@pcod", aux);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Apaguei");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            }

        }



        String sqlFiltro = "select * from telefone where numerotelefone like @pnumero";
        public DataTable Buscar_Filtro(String ptelefone)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnumero", ptelefone);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_telefone = new SqlDataAdapter(cmd);

            dt_telefone = new DataTable();
            da_telefone.Fill(dt_telefone);

            //Finaliza a Conexão
            conn.Close();
            return dt_telefone;
        }


        String sqlTodos = "select * from telefone";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_telefone = new SqlDataAdapter(cmd);
            dt_telefone = new DataTable();
            da_telefone.Fill(dt_telefone);


            return dt_telefone;
        }


        String sqlInsere = "insert into telefone(numerotelefone) values (@pnumero)";
        public void Insere_Dados(object aux)
        {
            Telefone telefone = new Telefone();
            telefone = (Telefone)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnumero", telefone.numerotelefone);

            cmd.CommandType = CommandType.Text;
            conn.Open();

            try
            {
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    MessageBox.Show("Inseriu");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro");
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
