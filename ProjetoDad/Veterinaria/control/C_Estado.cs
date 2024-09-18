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
    internal class C_Estado
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_estado;
        SqlDataAdapter da_estado;
        List<Estado> lista_estado = new List<Estado>();



        public List<Estado> DadosEstado()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_estado;
            conn.Open();

            try
            {
                dr_estado = cmd.ExecuteReader();
                while (dr_estado.Read())
                {
                    Estado aux = new Estado();
                    aux.codestado = Int32.Parse(dr_estado["codestado"].ToString());
                    aux.nomeestado = dr_estado["nomeestado"].ToString();
                    aux.sigla = dr_estado["sigla"].ToString();
                    lista_estado.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_estado;

        }



        public List<Estado> DadosEstadoFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomeestado", parametro + "%");


            SqlDataReader dr_estado;
            conn.Open();

            try
            {
                dr_estado = cmd.ExecuteReader();
                while (dr_estado.Read())
                {
                    Estado aux = new Estado();
                    aux.codestado = Int32.Parse(dr_estado["codestado"].ToString());
                    aux.nomeestado = dr_estado["nomeestado"].ToString();
                    aux.sigla = dr_estado["sigla"].ToString();

                    lista_estado.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_estado;
        }


        String sqlAtualiza = "update estado set nomeestado = @pnome, sigla = @psigla where" +
            " codestado= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Estado dados = new Estado();
            dados = (Estado)aux;


       conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codestado);
            cmd.Parameters.AddWithValue("@pnome", dados.nomeestado);
            cmd.Parameters.AddWithValue("@psigla", dados.sigla);

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


        String sqlApaga = "delete from estado where codestado = @pcod";
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



        String sqlFiltro = "select * from estado where nomeestado like @pnome";
        public DataTable Buscar_Filtro(String pestado)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnome", pestado);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_estado = new SqlDataAdapter(cmd);

            dt_estado = new DataTable();
            da_estado.Fill(dt_estado);

            //Finaliza a Conexão
            conn.Close();
            return dt_estado;
        }


        String sqlTodos = "select * from estado";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_estado = new SqlDataAdapter(cmd);
            dt_estado = new DataTable();
            da_estado.Fill(dt_estado);


            return dt_estado;
        }


        String sqlInsere = "insert into estado(nomeestado, sigla) values (@nome, @sigla)";
        public void Insere_Dados(object aux)
        {
            Estado estado = new Estado();
            estado = (Estado)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", estado.nomeestado);
            cmd.Parameters.AddWithValue("@psigla", estado.sigla);

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
        }     //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            
    }
}
