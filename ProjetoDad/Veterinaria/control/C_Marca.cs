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
    internal class C_Marca
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_marca;
        SqlDataAdapter da_marca;
        List<Marca> lista_marca = new List<Marca>();



        public List<Marca> DadosMarca()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_marca;
            conn.Open();

            try
            {
                dr_marca = cmd.ExecuteReader();
                while (dr_marca.Read())
                {
                    Marca aux = new Marca();
                    aux.codmarca = Int32.Parse(dr_marca["codmarca"].ToString());
                    aux.nomemarca = dr_marca["nomemarca"].ToString();
                    lista_marca.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_marca;

        }



        public List<Marca> DadosMarcaFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomemarca", parametro + "%");


            SqlDataReader dr_marca;
            conn.Open();

            try
            {
                dr_marca = cmd.ExecuteReader();
                while (dr_marca.Read())
                {
                    Marca aux = new Marca();
                    aux.codmarca = Int32.Parse(dr_marca["codmarca"].ToString());
                    aux.nomemarca = dr_marca["nomemarca"].ToString();

                    lista_marca.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_marca;
        }


        String sqlAtualiza = "update marca set nomemarca = @pnome where" +
            " codmarca= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Marca dados = new Marca();
            dados = (Marca)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codmarca);
            cmd.Parameters.AddWithValue("@pnome", dados.nomemarca);

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


        String sqlApaga = "delete from marca where codmarca = @pcod";
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



        String sqlFiltro = "select * from marca where nomemarca like @pnomemarca";
        public DataTable Buscar_Filtro(String pmarca)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnomemarca", pmarca);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_marca = new SqlDataAdapter(cmd);

            dt_marca = new DataTable();
            da_marca.Fill(dt_marca);

            //Finaliza a Conexão
            conn.Close();
            return dt_marca;
        }


        String sqlTodos = "select * from marca";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_marca = new SqlDataAdapter(cmd);
            dt_marca = new DataTable();
            da_marca.Fill(dt_marca);


            return dt_marca;
        }


        String sqlInsere = "insert into marca(nomemarca) values (@pnome)";
        public void Insere_Dados(object aux)
        {
            Marca marca = new Marca();
            marca = (Marca)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", marca.nomemarca);

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
