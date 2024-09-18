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
    public partial class FrmBairro : Form
    {
        DataTable Tabela_bairro;


        List<Bairro> lista_bairro = new List<Bairro>();

        Boolean novo = true;
        int posicao;
        public FrmBairro()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_bairro = carregaListaBairro();



            if (lista_bairro.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Bairro> carregaListaBairro()
        {
            List<Bairro> lista = new List<Bairro>();
            C_Bairro cr = new C_Bairro();
            lista = cr.DadosBairro();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Bairro cr = new C_Bairro();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_bairro = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtBairro.Enabled = false;
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
            txtBairro.Text = "";
        }

        private void ativarCampos()
        {
            txtBairro.Enabled = true;
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
            txtcod.Text = lista_bairro[posicao].codbairro.ToString();
            txtBairro.Text = lista_bairro[posicao].nomebairro.ToString();
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
            Bairro bairro = new Bairro();
            bairro.nomebairro = txtBairro.Text;

            C_Bairro c_Bairro = new C_Bairro();

            if (novo == true)
            {
                c_Bairro.Insere_Dados(bairro);

            }
            else
            {
                bairro.codbairro = Int32.Parse(txtcod.Text);
                c_Bairro.Atualizar_Dados(bairro);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_bairro = carregaListaBairro();



            if (lista_bairro.Count > 0)
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
            C_Bairro c_Bairro = new C_Bairro();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Bairro.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_bairro = carregaListaBairro();



            if (lista_bairro.Count > 0)
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
            int total = lista_bairro.Count - 1;
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
            posicao = lista_bairro.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Bairro
            C_Bairro cr = new C_Bairro();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_bairro = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_bairro;


            //Carrega a lista_bairro com o valor da consulta com parâmetro
            lista_bairro = carregaListaBairro();

            if (lista_bairro.Count - 1 >= 0)
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
            txtBairro.Text = dr.Cells[1].Value.ToString();
        }

        private void FrmBairro_Load(object sender, EventArgs e)
        {

        }
    }
}
