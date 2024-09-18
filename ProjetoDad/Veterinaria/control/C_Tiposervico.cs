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
    internal class C_Tiposervico
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tiposervico;
        SqlDataAdapter da_tiposervico;
        List<TipoServico> lista_tiposervico = new List<TipoServico>();



        public List<TipoServico> DadosTipoServico()
        {
            List<TipoServico> lista_aux = new List<TipoServico>();
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tiposervico;
            conn.Open();

            try
            {
                dr_tiposervico = cmd.ExecuteReader();
                while (dr_tiposervico.Read())
                {
                    TipoServico aux = new TipoServico();
                    aux.codtiposervico = Int32.Parse(dr_tiposervico["codtiposervico"].ToString());
                    aux.nometiposervico = dr_tiposervico["nometiposervico"].ToString();
                    aux.valortiposervico = Double.Parse((string)dr_tiposervico["valortiposervico"].ToString());
                    lista_aux.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_aux;

        }



        public List<TipoServico> DadostIPOsERVICOFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometiposervico", parametro + "%");


            SqlDataReader dr_tiposervico;
            conn.Open();

            try
            {
                dr_tiposervico = cmd.ExecuteReader();
                while (dr_tiposervico.Read())
                {
                    TipoServico aux = new TipoServico();
                    aux.codtiposervico = Int32.Parse(dr_tiposervico["codtiposervico"].ToString());

                    aux.nometiposervico = dr_tiposervico["nometiposervico"].ToString();
                    aux.valortiposervico = Double.Parse((string)dr_tiposervico["valortiposervico".ToString()]);

                    lista_tiposervico.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_tiposervico;
        }


        String sqlAtualiza = "update tiposervico" +
            " set nometiposervico = @pnome,  valortiposervico = @pvalor where codtiposervico= @pcod";
        public void Atualizar_Dados(object aux)
        {
            TipoServico dados = new TipoServico();
            dados = (TipoServico)aux;


            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtiposervico);
            cmd.Parameters.AddWithValue("@pnome", dados.nometiposervico);
            cmd.Parameters.AddWithValue("@pvalor", dados.valortiposervico);

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


        String sqlApaga = "delete from tiposervico where codtiposervico = @pcod";
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



        String sqlFiltro = "select * from tiposervico where nometiposervico like @pnome";
        public DataTable Buscar_Filtro(String ptiposervico)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnome", ptiposervico);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_tiposervico = new SqlDataAdapter(cmd);

            dt_tiposervico = new DataTable();
            da_tiposervico.Fill(dt_tiposervico);

            //Finaliza a Conexão
            conn.Close();
            return dt_tiposervico;
        }


        String sqlTodos = "select * from tiposervico";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_tiposervico = new SqlDataAdapter(cmd);
            dt_tiposervico = new DataTable();
            da_tiposervico.Fill(dt_tiposervico);


            return dt_tiposervico;
        }


        String sqlInsere = "insert into tiposervico(nometiposervico, valortiposervico) values (@nome, @valor)";
        public void Insere_Dados(object aux)
        {
            TipoServico tipoServico = new TipoServico();
            tipoServico = (TipoServico)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@nome", tipoServico.nometiposervico);
            cmd.Parameters.AddWithValue("@valor", tipoServico.valortiposervico);

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
                MessageBox.Show("Erro" + ex.ToString());
            }
            finally
            {
                conn.Close();
            }
        }     //Criando a Conexao o banco de Dados
        Conexao conexao = new Conexao();
    }
}
