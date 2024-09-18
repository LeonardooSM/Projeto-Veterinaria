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
    public partial class FrmTipoServico : Form
    {
        DataTable tabela_tiposervico;


        List<TipoServico> lista_tiposervico = new List<TipoServico>();

        Boolean novo = true;
        int posicao;
        public FrmTipoServico()
        {
            InitializeComponent();
            //Carregar o Datagrid de TipoServico
            CarregaTabela();



            lista_tiposervico = carregaListaTipoServico();



            if (lista_tiposervico.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<TipoServico> carregaListaTipoServico()
        {
            List<TipoServico> lista = new List<TipoServico>();
            C_Tiposervico cr = new C_Tiposervico();
            lista = cr.DadosTipoServico();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Tiposervico cr = new C_Tiposervico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_tiposervico = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtTipo.Enabled = false;
            txtValor.Enabled = false;
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
            txtTipo.Text = "";
            txtValor.Text = "";
        }

        private void ativarCampos()
        {
            txtTipo.Enabled = true;
            txtValor.Enabled = true;
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
            if (posicao >= 0)
            {
                txtcod.Text = lista_tiposervico[posicao].codtiposervico.ToString();
                txtTipo.Text = lista_tiposervico[posicao].nometiposervico.ToString();
                txtValor.Text = lista_tiposervico[posicao].valortiposervico.ToString();
            }
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
            TipoServico tipoServico = new TipoServico();
            tipoServico.nometiposervico = txtTipo.Text;
            tipoServico.valortiposervico = Double.Parse((txtValor.Text));

            C_Tiposervico c_Tiposervico = new C_Tiposervico();

            if (novo == true)
            {
                c_Tiposervico.Insere_Dados(tipoServico);

            }
            else
            {
                tipoServico.codtiposervico = Int32.Parse(txtcod.Text);
                c_Tiposervico.Atualizar_Dados(tipoServico);

            }

            lista_tiposervico = carregaListaTipoServico();
            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();




            if (lista_tiposervico.Count > 0)
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
            C_Tiposervico c_Tiposervico = new C_Tiposervico();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Tiposervico.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_tiposervico = carregaListaTipoServico();



            if (lista_tiposervico.Count > 0)
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
            int total = lista_tiposervico.Count;
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
            if (posicao >= 0)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao = lista_tiposervico.Count-1;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Tiposervico
            C_Tiposervico cr = new C_Tiposervico();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_tiposervico = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_tiposervico;


            //Carrega a lista_tiposervico com o valor da consulta com parâmetro
            lista_tiposervico = carregaListaTipoServico();

            if (lista_tiposervico.Count >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void FrmTipoServico_Load(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtTipo.Text = dr.Cells[1].Value.ToString();
            txtValor.Text = dr.Cells[2].Value.ToString();
        }
    }
}
