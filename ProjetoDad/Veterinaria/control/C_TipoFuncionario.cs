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
    internal class C_TipoFuncionario
    {


        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipofuncionario;
        SqlDataAdapter da_tipofuncionario;
        List<Tipofuncionario> lista_tipofuncionario = new List<Tipofuncionario>();



        public List<Tipofuncionario> DadosTipoFuncionario()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipofuncionario;
            conn.Open();

            try
            {
                dr_tipofuncionario = cmd.ExecuteReader();
                while (dr_tipofuncionario.Read())
                {
                    Tipofuncionario aux = new Tipofuncionario();
                    aux.codtipofuncionario = Int32.Parse(dr_tipofuncionario["codtipofuncionario"].ToString());
                    aux.nometipofuncionario = dr_tipofuncionario["nometipofuncionario"].ToString();
                    lista_tipofuncionario.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_tipofuncionario;

        }



        public List<Tipofuncionario> DadosTipoFuncionarioFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipofuncionario", parametro + "%");


            SqlDataReader dr_bairro;
            conn.Open();

            try
            {
                dr_bairro = cmd.ExecuteReader();
                while (dr_bairro.Read())
                {
                    Tipofuncionario aux = new Tipofuncionario();
                    aux.codtipofuncionario = Int32.Parse(dr_bairro["codtipofuncionario"].ToString());
                    aux.nometipofuncionario = dr_bairro["nometipofuncionario"].ToString();

                    lista_tipofuncionario.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_tipofuncionario;
        }


        String sqlAtualiza = "update tipofuncionario set nometipofuncionario = @pnome where" +
            " codtipofuncionario= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipofuncionario dados = new Tipofuncionario();
            dados = (Tipofuncionario)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipofuncionario);
            cmd.Parameters.AddWithValue("@pnome", dados.nometipofuncionario);

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


        String sqlApaga = "delete from tipofuncionario where codtipofuncionario = @pcod";
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



        String sqlFiltro = "select * from tipofuncionario where nometipofuncionario like @pnometipofuncionario";
        public DataTable Buscar_Filtro(String ptipofuncionario)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipofuncionario", ptipofuncionario);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipofuncionario = new SqlDataAdapter(cmd);

            dt_tipofuncionario = new DataTable();
            da_tipofuncionario.Fill(dt_tipofuncionario);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipofuncionario;
        }


        String sqlTodos = "select * from TipoFuncionario";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_tipofuncionario = new SqlDataAdapter(cmd);
            dt_tipofuncionario = new DataTable();
            da_tipofuncionario.Fill(dt_tipofuncionario);


            return dt_tipofuncionario;
        }


        String sqlInsere = "insert into TipoFuncionario(NomeTipoFuncionario) values (@pnome)";
        public void Insere_Dados(object aux)
        {
            Tipofuncionario tipofuncionario = new Tipofuncionario();
            tipofuncionario = (Tipofuncionario)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", tipofuncionario.nometipofuncionario);

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
