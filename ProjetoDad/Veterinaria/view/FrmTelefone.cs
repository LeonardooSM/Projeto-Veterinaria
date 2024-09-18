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
    public partial class FrmTelefone : Form
    {
        DataTable tabela_telefone;


        List<Telefone> lista_telefone = new List<Telefone>();

        Boolean novo = true;
        int posicao;
        public FrmTelefone()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



             lista_telefone= carregaListaTelefone();



            if (lista_telefone.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Telefone> carregaListaTelefone()
        {
            List<Telefone> lista = new List<Telefone>();
            C_Telefone cr = new C_Telefone();
            lista = cr.DadosTelefone();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Telefone cr = new C_Telefone();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_telefone = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            mskTel.Enabled = false;
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
            mskTel.Text = "";
        }

        private void ativarCampos()
        {
            mskTel.Enabled = true;
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
            txtcod.Text = lista_telefone[posicao].codtelefone.ToString();
            mskTel.Text = lista_telefone[posicao].numerotelefone.ToString();
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
            Telefone telefone = new Telefone();
            telefone.numerotelefone = mskTel.Text;

            C_Telefone c_telefone = new C_Telefone();

            if (novo == true)
            {
                c_telefone.Insere_Dados(telefone);

            }
            else
            {
                telefone.codtelefone = Int32.Parse(txtcod.Text);
                c_telefone.Atualizar_Dados(telefone);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_telefone = carregaListaTelefone();



            if (lista_telefone.Count > 0)
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
            C_Telefone c_Telefone = new C_Telefone();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Telefone.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_telefone = carregaListaTelefone();



            if (lista_telefone.Count > 0)
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
            int total = lista_telefone.Count - 1;
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
            posicao = lista_telefone.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_telefone
            C_Telefone cr = new C_Telefone();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_telefone = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_telefone;


            //Carrega a lista_cep com o valor da consulta com parâmetro
            lista_telefone = carregaListaTelefone();

            if (lista_telefone.Count - 1 >= 0)
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
            mskTel.Text = dr.Cells[1].Value.ToString();
        }
    }
}
