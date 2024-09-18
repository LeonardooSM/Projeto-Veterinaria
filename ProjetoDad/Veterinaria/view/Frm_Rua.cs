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
    public partial class Frm_Rua : Form
    {
        DataTable tabela_rua;


        List<Rua> lista_rua = new List<Rua>();

        Boolean novo = true;
        int posicao;
        public Frm_Rua()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_rua = carregaListaRua();



            if (lista_rua.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Rua> carregaListaRua()
        {
            List<Rua> lista = new List<Rua>();
            C_Rua cr = new C_Rua();
            lista = cr.DadosRua();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Rua cr = new C_Rua();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_rua = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtRua.Enabled = false;
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
            txtRua.Text = "";
        }

        private void ativarCampos()
        {
            txtRua.Enabled = true;
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
            txtcod.Text = lista_rua[posicao].codrua.ToString();
            txtRua.Text = lista_rua[posicao].nomerua.ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtRua.Text = dr.Cells[1].Value.ToString();
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
            Rua rua = new Rua();
            rua.nomerua = txtRua.Text;

            C_Rua c_Rua = new C_Rua();

            if (novo == true)
            {
                c_Rua.Insere_Dados(rua);

            }
            else
            {
                rua.codrua = Int32.Parse(txtcod.Text);
                c_Rua.Atualizar_Dados(rua);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_rua = carregaListaRua();



            if (lista_rua.Count > 0)
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
            C_Rua c_Rua = new C_Rua();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Rua.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_rua = carregaListaRua();



            if (lista_rua.Count > 0)
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
            int total = lista_rua.Count - 1;
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
            posicao = lista_rua.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Rua
            C_Rua cr = new C_Rua();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_rua = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_rua;


            //Carrega a lista_rua com o valor da consulta com parâmetro
            lista_rua = carregaListaRua();

            if (lista_rua.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
    }
}
