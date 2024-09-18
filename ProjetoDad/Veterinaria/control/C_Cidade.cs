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
    internal class C_Cidade
    {
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cidade;
        SqlDataAdapter da_cidade;
        List<Cidade> lista_cidade = new List<Cidade>();



        public List<Cidade> DadosCidade()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cep;
            conn.Open();

            try
            {
                dr_cep = cmd.ExecuteReader();
                while (dr_cep.Read())
                {
                    Cidade aux = new Cidade();
                    aux.codcidade = Int32.Parse(dr_cep["codcidade"].ToString());
                    aux.nomecidade = dr_cep["nomecidade"].ToString();
                    lista_cidade.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cidade;

        }



        public List<Cidade> DadosCidadeFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomecidade", parametro + "%");


            SqlDataReader dr_cidade;
            conn.Open();

            try
            {
                dr_cidade = cmd.ExecuteReader();
                while (dr_cidade.Read())
                {
                    Cidade aux = new Cidade();
                    aux.codcidade = Int32.Parse(dr_cidade["codcidade"].ToString());
                    aux.nomecidade = dr_cidade["nomecidade"].ToString();

                    lista_cidade.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_cidade;
        }


        String sqlAtualiza = "update cidade set nomecidade = @pnome where" +
            " codcidade= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Cidade dados = new Cidade();
            dados = (Cidade)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcidade);
            cmd.Parameters.AddWithValue("@pnome", dados.nomecidade);

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


        String sqlApaga = "delete from cidade where codcidade = @pcod";
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



        String sqlFiltro = "select * from cidade where nomecidade like @pnome";
        public DataTable Buscar_Filtro(String pcidade)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnome", pcidade);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_cidade = new SqlDataAdapter(cmd);

            dt_cidade = new DataTable();
            da_cidade.Fill(dt_cidade);

            //Finaliza a Conexão
            conn.Close();
            return dt_cidade;
        }


        String sqlTodos = "select * from cidade";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_cidade = new SqlDataAdapter(cmd);
            dt_cidade = new DataTable();
            da_cidade.Fill(dt_cidade);


            return dt_cidade;
        }


        String sqlInsere = "insert into cidade(nomecidade) values (@nome)";
        public void Insere_Dados(object aux)
        {
            Cidade cidade = new Cidade();
            cidade = (Cidade)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", cidade.nomecidade);

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

