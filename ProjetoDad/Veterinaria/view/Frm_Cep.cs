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
    public partial class Frm_Cep : Form
    {
        DataTable tabela_cep;


        List<Cep> lista_cep = new List<Cep>();

        Boolean novo = true;
        int posicao;
        public Frm_Cep()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_cep = carregaListaCep();



            if (lista_cep.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Cep> carregaListaCep()
        {
            List<Cep> lista = new List<Cep>();
            C_Cep cr = new C_Cep();
            lista = cr.DadosCep();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_cep = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            mskCep.Enabled = false;
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
            mskCep.Text = "";
        }

        private void ativarCampos()
        {
            mskCep.Enabled = true;
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
            txtcod.Text = lista_cep[posicao].codcep.ToString();
            mskCep.Text = lista_cep[posicao].numerocep.ToString();
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
            Cep cep = new Cep();
            cep.numerocep = mskCep.Text;

            C_Cep c_Cep = new C_Cep();

            if (novo == true)
            {
                c_Cep.Insere_Dados(cep);

            }
            else
            {
                cep.codcep = Int32.Parse(txtcod.Text);
                c_Cep.Atualizar_Dados(cep);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_cep = carregaListaCep();



            if (lista_cep.Count > 0)
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
            C_Cep c_Cep = new C_Cep();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Cep.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_cep = carregaListaCep();



            if (lista_cep.Count > 0)
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
            int total = lista_cep.Count - 1;
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
            posicao = lista_cep.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Cep
            C_Cep cr = new C_Cep();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_cep = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_cep;


            //Carrega a lista_cep com o valor da consulta com parâmetro
            lista_cep = carregaListaCep();

            if (lista_cep.Count - 1 >= 0)
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
            mskCep.Text = dr.Cells[1].Value.ToString();
        }

        private void Frm_Cep_Load(object sender, EventArgs e)
        {

        }
    }
}
