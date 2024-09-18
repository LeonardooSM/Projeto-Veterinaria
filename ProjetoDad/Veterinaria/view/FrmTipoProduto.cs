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
    public partial class FrmTipoProduto : Form
    {
        DataTable Tabela_TipoProduto;


        List<Tipoproduto> lista_tipoproduto = new List<Tipoproduto>();

        Boolean novo = true;
        int posicao;
        public FrmTipoProduto()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_tipoproduto = carregaListaTipoProduto();



            if (lista_tipoproduto.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Tipoproduto> carregaListaTipoProduto()
        {
            List<Tipoproduto> lista = new List<Tipoproduto>();
            C_TipoProduto cr = new C_TipoProduto();
            lista = cr.DadosTipoProduto();



            return lista;

        }


        public void CarregaTabela()
        {

            C_TipoProduto cr = new C_TipoProduto();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_TipoProduto = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtTipoProduto.Enabled = false;
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
            txtTipoProduto.Text = "";
        }

        private void ativarCampos()
        {
            txtTipoProduto.Enabled = true;
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
            txtcod.Text = lista_tipoproduto[posicao].codtipoproduto.ToString();
            txtTipoProduto.Text = lista_tipoproduto[posicao].nometipoproduto.ToString();
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
            Tipoproduto tipoproduto = new Tipoproduto();
            tipoproduto.nometipoproduto = txtTipoProduto.Text;

            C_TipoProduto c_TipoProduto = new C_TipoProduto();

            if (novo == true)
            {
                c_TipoProduto.Insere_Dados(tipoproduto);

            }
            else
            {
                tipoproduto.codtipoproduto = Int32.Parse(txtcod.Text);
                c_TipoProduto.Atualizar_Dados(tipoproduto);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_tipoproduto = carregaListaTipoProduto();



            if (lista_tipoproduto.Count > 0)
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
            C_TipoProduto c_TipoProduto = new C_TipoProduto();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_TipoProduto.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_tipoproduto = carregaListaTipoProduto();



            if (lista_tipoproduto.Count > 0)
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
            int total = lista_tipoproduto.Count - 1;
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
            posicao = lista_tipoproduto.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
            //Foi definido um atributo chamado cr do tipo C_TipoProduto
            C_TipoProduto cr = new C_TipoProduto();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_TipoProduto = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_TipoProduto;


            //Carrega a lista_bairro com o valor da consulta com parâmetro
            lista_tipoproduto = carregaListaTipoProduto();

            if (lista_tipoproduto.Count - 1 >= 0)
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
            txtTipoProduto.Text = dr.Cells[1].Value.ToString();
        }
    }
}
