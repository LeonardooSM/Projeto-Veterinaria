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
    public partial class FrmCidAnimal : Form
    {
        DataTable tabela_cidanimal;


        List<Cidanimal> lista_cidanimal = new List<Cidanimal>();

        Boolean novo = true;
        int posicao;
        public FrmCidAnimal()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_cidanimal = carregaListaCidAnimal();



            if (lista_cidanimal.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Cidanimal> carregaListaCidAnimal()
        {
            List<Cidanimal> lista = new List<Cidanimal>();
            C_CidAnimal cr = new C_CidAnimal();
            lista = cr.DadosCidAnimal();



            return lista;

        }


        public void CarregaTabela()
        {

            C_CidAnimal cr = new C_CidAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_cidanimal = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtCid.Enabled = false;
            txtDesc.Enabled = false;
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
            txtCid.Text = "";
            txtDesc.Text = "";
        }

        private void ativarCampos()
        {
            txtCid.Enabled = true;
            txtDesc.Enabled = true;
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
            txtcod.Text = lista_cidanimal[posicao].codcidanimal.ToString();
            txtCid.Text = lista_cidanimal[posicao].nomecidanimal.ToString();
            txtDesc.Text = lista_cidanimal[posicao].descricao.ToString();
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
            Cidanimal cidanimal = new Cidanimal();
            cidanimal.nomecidanimal = txtCid.Text;
            cidanimal.descricao = txtDesc.Text;

            C_CidAnimal c_CidAnimal = new C_CidAnimal();

            if (novo == true)
            {
                c_CidAnimal.Insere_Dados(cidanimal);

            }
            else
            {
                cidanimal.codcidanimal = Int32.Parse(txtcod.Text);
                c_CidAnimal.Atualizar_Dados(cidanimal);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_cidanimal = carregaListaCidAnimal();



            if (lista_cidanimal.Count > 0)
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
            C_CidAnimal c_CidAnimal = new C_CidAnimal();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_CidAnimal.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_cidanimal = carregaListaCidAnimal();



            if (lista_cidanimal.Count > 0)
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
            int total = lista_cidanimal.Count - 1;
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
            posicao = lista_cidanimal.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_CidAnimal
            C_CidAnimal cr = new C_CidAnimal();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_cidanimal = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_cidanimal;


            //Carrega a Lista_cidanimal com o valor da consulta com parâmetro
            lista_cidanimal = carregaListaCidAnimal();

            if (lista_cidanimal.Count - 1 >= 0)
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
            txtCid.Text = dr.Cells[1].Value.ToString();
            txtDesc.Text=dr.Cells[2].Value.ToString();
        }

        private void FrmCidAnimal_Load(object sender, EventArgs e)
        {

        }
    }
}
