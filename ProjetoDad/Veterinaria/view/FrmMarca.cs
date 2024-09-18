using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmMarca : Form
    {
        DataTable tabela_marca;


        List<Marca> lista_marca = new List<Marca>();

        Boolean novo = true;
        int posicao;
        public FrmMarca()
        {
            InitializeComponent();
            //Carregar o Datagrid de Marca
            CarregaTabela();



            lista_marca = carregaListaMarca();



            if (lista_marca.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Marca> carregaListaMarca()
        {
            List<Marca> lista = new List<Marca>();
            C_Marca cr = new C_Marca();
            lista = cr.DadosMarca();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Marca cr = new C_Marca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_marca = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtMarca.Enabled = false;
        }


        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            btnEditar.Enabled = true;
            btnApagar.Enabled = true;
        }

        private void limparCampos()
        {
            txtcod.Text = "";
            txtMarca.Text = "";
        }

        private void ativarCampos()
        {
            txtMarca.Enabled = true;
        }

        private void ativaBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void atualizaCampos()
        {
            txtcod.Text = lista_marca[posicao].codmarca.ToString();
            txtMarca.Text = lista_marca[posicao].nomemarca.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            limparCampos();

            ativarCampos();

            ativaBotoes();

            novo = true;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();
            marca.nomemarca = txtMarca.Text;

            C_Marca c_Marca = new C_Marca();

            if (novo == true)
            {
                c_Marca.Insere_Dados(marca);

            }
            else
            {
                marca.codmarca = Int32.Parse(txtcod.Text);
                c_Marca.Atualizar_Dados(marca);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_marca = carregaListaMarca();



            if (lista_marca.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_Marca c_Marca = new C_Marca();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Marca.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_marca = carregaListaMarca();



            if (lista_marca.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            ativarCampos();
            ativaBotoes();
            novo = false;
        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (posicao > 0)
            {

                dataGridView1.Rows[posicao].Selected = false;
                posicao--;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;


            }
        }

        private void btnProx_Click(object sender, EventArgs e)
        {
            int total = lista_marca.Count - 1;
            if (total > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUlt_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_marca.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Marca
            C_Marca cr = new C_Marca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_marca = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_marca;


            //Carrega a lista_marca com o valor da consulta com parâmetro
            lista_marca = carregaListaMarca();

            if (lista_marca.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtMarca.Text = dr.Cells[1].Value.ToString();
        }
    }
}
