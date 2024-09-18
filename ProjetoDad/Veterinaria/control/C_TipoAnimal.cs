using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veterinaria.model;
using Veterinaria.conection;
using System.Windows.Forms;

namespace Veterinaria.control
{
    internal class C_TipoAnimal
    {

        //VARIAVEIS GLOBAIS DA CLASSE
        SqlConnection conn;
        SqlCommand cmd;
        DataTable dt_tipoAnimal;
        SqlDataAdapter da_tipoAnimal;
        List<Tipoanimal> lista_TipoAnimal = new List<Tipoanimal>();



        public List<Tipoanimal> DadosTipoAnimal()
        {

            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            SqlDataReader dr_tipoanimal;
            conn.Open();

            try
            {
                dr_tipoanimal = cmd.ExecuteReader();
                while (dr_tipoanimal.Read())
                {
                    Tipoanimal aux = new Tipoanimal();
                    aux.codtipoanimal = Int32.Parse(dr_tipoanimal["codtipoanimal"].ToString());
                    aux.nometipoanimal = dr_tipoanimal["nometipoanimal"].ToString();
                    lista_TipoAnimal.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }

            return lista_TipoAnimal;

        }



        public List<Tipoanimal> DadosTipoAnimalFiltro(String parametro)
        {
            //Cria uma lista do tipo sexo -Array


            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlFiltro, conn);

            //Adiciona o valor a ser pesquisado no parâmetro
            cmd.Parameters.AddWithValue("pnometipoanimal", parametro + "%");


            SqlDataReader dr_tipoanimal;
            conn.Open();

            try
            {
                dr_tipoanimal = cmd.ExecuteReader();
                while (dr_tipoanimal.Read())
                {
                    Tipoanimal aux = new Tipoanimal();
                    aux.codtipoanimal = Int32.Parse(dr_tipoanimal["codtipoanimal"].ToString());
                    aux.nometipoanimal = dr_tipoanimal["nometipoanimal"].ToString();

                    lista_TipoAnimal.Add(aux);
                }
            }
            catch (Exception ex)
            {

            }


            return lista_TipoAnimal;
        }


        String sqlAtualiza = "update tipoanimal set nometipoanimal = @pnome where" +
            " codtipoanimal= @pcod";
        public void Atualizar_Dados(object aux)
        {
            Tipoanimal dados = new Tipoanimal();
            dados = (Tipoanimal)aux;


            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlAtualiza, conn);
            cmd.Parameters.AddWithValue("@pcod", dados.codtipoanimal);
            cmd.Parameters.AddWithValue("@pnome", dados.nometipoanimal);

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


        String sqlApaga = "delete from tipoanimal where codtipoanimal = @pcod";
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



        String sqlFiltro = "select * from tipoanimal where nometipoanimal like @pnometipoanimal";
        public DataTable Buscar_Filtro(String ptipoanimal)
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlFiltro, conn);
            cmd.Parameters.AddWithValue("pnometipoanimal", ptipoanimal);
            //Abrir conexão
            conn.Open();

            //Criar o DataAdapter
            da_tipoAnimal = new SqlDataAdapter(cmd);

            dt_tipoAnimal = new DataTable();
            da_tipoAnimal.Fill(dt_tipoAnimal);

            //Finaliza a Conexão
            conn.Close();
            return dt_tipoAnimal;
        }


        String sqlTodos = "select * from tipoanimal";
        public DataTable Buscar_Todos()
        {
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();
            cmd = new SqlCommand(sqlTodos, conn);

            conn.Open();
            da_tipoAnimal = new SqlDataAdapter(cmd);
            dt_tipoAnimal = new DataTable();
            da_tipoAnimal.Fill(dt_tipoAnimal);


            return dt_tipoAnimal;
        }


        String sqlInsere = "insert into tipoanimal(nometipoanimal) values (@pnome)";
        public void Insere_Dados(object aux)
        {
            Tipoanimal tipoanimal = new Tipoanimal();
            tipoanimal = (Tipoanimal)aux; //casting

            //Criando a Conexao o banco de Dados
            Conexao conexao = new Conexao();
            conn = conexao.ConectarBanco();

            cmd = new SqlCommand(sqlInsere, conn);
            cmd.Parameters.AddWithValue("@pnome", tipoanimal.nometipoanimal);

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
