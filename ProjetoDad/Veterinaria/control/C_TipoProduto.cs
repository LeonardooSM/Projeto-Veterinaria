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
    internal class C_TipoProduto
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_TipoProduto;
        SqlDataAdapter da_TipoProduto;
        List<Tipoproduto> lista_tipoproduto = new List<Tipoproduto>();



        public List<Tipoproduto> DadosTipoProduto()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoproduto;
            conn.Open();

            try
            {
                dr_tipoproduto = cmd.ExecuteReader();
                while (dr_tipoproduto.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_tipoproduto["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_tipoproduto["nometipoproduto"].ToString();
                    lista_tipoproduto.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_tipoproduto;

        }



        public List<Tipoproduto> DadosTipoProdutoFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipoproduto", parametro + "%");


            SqlDataReader dr_bairro;
            conn.Open();

            try
            {
                dr_bairro = cmd.ExecuteReader();
                while (dr_bairro.Read())
                {
                    Tipoproduto aux = new Tipoproduto();
                    aux.codtipoproduto = Int32.Parse(dr_bairro["codtipoproduto"].ToString());
                    aux.nometipoproduto = dr_bairro["nometipoproduto"].ToString();

                    lista_tipoproduto.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_tipoproduto;
        }


        String sqlAtualiza = "update tipoproduto set nometipoproduto = @pnome where" +
            " codtipoproduto= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipoproduto dados = new Tipoproduto();
            dados = (Tipoproduto)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipoproduto);
            cmd.Parameters.AddWithValue("@pnome", dados.nometipoproduto);

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


        String sqlApaga = "delete from tipoproduto where codtipoproduto = @pcod";
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



        String sqlFiltro = "select * from tipoproduto where nometipoproduto like @pnometipoproduto";
        public DataTable Buscar_Filtro(String ptipoproduto)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipoproduto", ptipoproduto);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_TipoProduto = new SqlDataAdapter(cmd);

            dt_TipoProduto = new DataTable();
            da_TipoProduto.Fill(dt_TipoProduto);

            //Finaliza a Conexão
            conn.Close();
            return dt_TipoProduto;
        }


        String sqlTodos = "select * from tipoproduto";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_TipoProduto = new SqlDataAdapter(cmd);
            dt_TipoProduto = new DataTable();
            da_TipoProduto.Fill(dt_TipoProduto);


            return dt_TipoProduto;
        }


        String sqlInsere = "insert into TipoProduto(nometipoproduto) values (@pnome)";
        public void Insere_Dados(object aux)
        {
            Tipoproduto tipoproduto = new Tipoproduto();
            tipoproduto = (Tipoproduto)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", tipoproduto.nometipoproduto);

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
