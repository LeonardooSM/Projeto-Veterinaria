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
    public partial class FrmTipoAnimal : Form
    {


        DataTable Tabela_tipoanimal;


        List<Tipoanimal> lista_tipoanimal = new List<Tipoanimal>();

        Boolean novo = true;
        int posicao;
        public FrmTipoAnimal()
        {
            InitializeComponent();
            //Carregar o Datagrid de TipoAnimal
            CarregaTabela();



            lista_tipoanimal = carregaListaTipoAnimal();



            if (lista_tipoanimal.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Tipoanimal> carregaListaTipoAnimal()
        {
            List<Tipoanimal> lista = new List<Tipoanimal>();
            C_TipoAnimal cr = new C_TipoAnimal();
            lista = cr.DadosTipoAnimal();



            return lista;

        }


        public void CarregaTabela()
        {

            C_TipoAnimal cr = new C_TipoAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_tipoanimal = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtTipoAnimal.Enabled = false;
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
            txtTipoAnimal.Text = "";
        }

        private void ativarCampos()
        {
            txtTipoAnimal.Enabled = true;
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
            txtcod.Text = lista_tipoanimal[posicao].codtipoanimal.ToString();
            txtTipoAnimal.Text = lista_tipoanimal[posicao].nometipoanimal.ToString();
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

            Tipoanimal tipoanimal = new Tipoanimal();
            tipoanimal.nometipoanimal = txtTipoAnimal.Text;

            C_TipoAnimal c_TipoAnimal = new C_TipoAnimal();

            if (novo == true)
            {
                c_TipoAnimal.Insere_Dados(tipoanimal);

            }
            else
            {
                tipoanimal.codtipoanimal = Int32.Parse(txtcod.Text);
                c_TipoAnimal.Atualizar_Dados(tipoanimal);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_tipoanimal = carregaListaTipoAnimal();



            if (lista_tipoanimal.Count > 0)
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

            C_TipoAnimal c_TipoAnimal = new C_TipoAnimal();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_TipoAnimal.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_tipoanimal = carregaListaTipoAnimal();



            if (lista_tipoanimal.Count > 0)
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

            int total = lista_tipoanimal.Count - 1;
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
            posicao = lista_tipoanimal.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtTipoAnimal.Text = dr.Cells[1].Value.ToString();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_TipoAnimal
            C_TipoAnimal cr = new C_TipoAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_tipoanimal = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_tipoanimal;


            //Carrega a lista_tipoanimal com o valor da consulta com parâmetro
            lista_tipoanimal = carregaListaTipoAnimal();

            if (lista_tipoanimal.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
    }
}
