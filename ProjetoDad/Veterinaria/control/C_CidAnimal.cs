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
    internal class C_CidAnimal
    {

        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_cidanimal;
        SqlDataAdapter da_cidanimal;
        List<Cidanimal> lista_cidanimals = new List<Cidanimal>();



        public List<Cidanimal> DadosCidAnimal()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_cidanimal;
            conn.Open();

            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    Cidanimal aux = new Cidanimal();
                    aux.codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.descricao = dr_cidanimal["descricao"].ToString();
                    lista_cidanimals.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_cidanimals;

        }



        public List<Cidanimal> DadosCidAnimalFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnomecidanimal", parametro + "%");


            SqlDataReader dr_cidanimal;
            conn.Open();

            try
            {
                dr_cidanimal = cmd.ExecuteReader();
                while (dr_cidanimal.Read())
                {
                    Cidanimal aux = new Cidanimal();
                    aux.codcidanimal = Int32.Parse(dr_cidanimal["codcidanimal"].ToString());
                    aux.nomecidanimal = dr_cidanimal["nomecidanimal"].ToString();
                    aux.descricao = dr_cidanimal["descricao"].ToString();

                    lista_cidanimals.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_cidanimals;
        }


        String sqlAtualiza = "update cidanimal set nomecidanimal = @pnome,  descricao = @pdescricao where codcidanimal= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Cidanimal dados = new Cidanimal();
            dados = (Cidanimal)aux;


            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codcidanimal);
            cmd.Parameters.AddWithValue("@pnome", dados.nomecidanimal);
            cmd.Parameters.AddWithValue("@pdescricao", dados.descricao);

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


        String sqlApaga = "delete from cidanimal where codcidanimal = @pcod";
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



        String sqlFiltro = "select * from cidanimal where nomecidanimal like @pnome";
        public DataTable Buscar_Filtro(String pcidanimal)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnome", pcidanimal);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_cidanimal = new SqlDataAdapter(cmd);

            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);

            //Finaliza a Conexão
            conn.Close();
            return dt_cidanimal;
        }


        String sqlTodos = "select * from cidanimal";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_cidanimal = new SqlDataAdapter(cmd);
            dt_cidanimal = new DataTable();
            da_cidanimal.Fill(dt_cidanimal);


            return dt_cidanimal;
        }


        String sqlInsere = "insert into cidanimal(nomecidanimal, descricao) values (@nome, @descricao)";
        public void Insere_Dados(object aux)
        {
            Cidanimal cidanimal = new Cidanimal();
            cidanimal = (Cidanimal)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@nome", cidanimal.nomecidanimal);
            cmd.Parameters.AddWithValue("@descricao", cidanimal.descricao);

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
