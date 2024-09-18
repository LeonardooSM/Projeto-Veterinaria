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
    public partial class FrmTipoFuncionario : Form
    {
        DataTable tabela_tipofuncionario;


        List<Tipofuncionario> lista_tipofuncionario = new List<Tipofuncionario>();

        Boolean novo = true;
        int posicao;
        public FrmTipoFuncionario()
        {
            InitializeComponent();
            //Carregar o Datagrid de Tipo Funcionario
            CarregaTabela();



            lista_tipofuncionario = carregaListaTipoFuncionario();



            if (lista_tipofuncionario.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }


        List<Tipofuncionario> carregaListaTipoFuncionario()
        {
            List<Tipofuncionario> lista = new List<Tipofuncionario>();
            C_TipoFuncionario cr = new C_TipoFuncionario();
            lista = cr.DadosTipoFuncionario();



            return lista;

        }


        public void CarregaTabela()
        {

            C_TipoFuncionario cr = new C_TipoFuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_tipofuncionario = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtTipoFuncionario.Enabled = false;
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
            txtTipoFuncionario.Text = "";
        }

        private void ativarCampos()
        {
            txtTipoFuncionario.Enabled = true;
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
            txtcod.Text = lista_tipofuncionario[posicao].codtipofuncionario.ToString();
            txtTipoFuncionario.Text = lista_tipofuncionario[posicao].nometipofuncionario.ToString();
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
            Tipofuncionario tipofuncionario = new Tipofuncionario();
            tipofuncionario.nometipofuncionario = txtTipoFuncionario.Text;

            C_TipoFuncionario c_TipoFuncionario = new C_TipoFuncionario();

            if (novo == true)
            {
                c_TipoFuncionario.Insere_Dados(tipofuncionario);

            }
            else
            {
                tipofuncionario.codtipofuncionario = Int32.Parse(txtcod.Text);
                c_TipoFuncionario.Atualizar_Dados(tipofuncionario);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_tipofuncionario = carregaListaTipoFuncionario();



            if (lista_tipofuncionario.Count > 0)
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
            C_TipoFuncionario c_TipoFuncionario = new C_TipoFuncionario();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_TipoFuncionario.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_tipofuncionario = carregaListaTipoFuncionario();



            if (lista_tipofuncionario.Count > 0)
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
            int total = lista_tipofuncionario.Count - 1;
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
            posicao = lista_tipofuncionario.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_TipoFuncionario
            C_TipoFuncionario cr = new C_TipoFuncionario();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_tipofuncionario = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_tipofuncionario;


            //Carrega a lista_funcionario com o valor da consulta com parâmetro
            lista_tipofuncionario = carregaListaTipoFuncionario();

            if (lista_tipofuncionario.Count - 1 >= 0)
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
            txtTipoFuncionario.Text = dr.Cells[1].Value.ToString();
        }

        private void FrmTipoFuncionario_Load(object sender, EventArgs e)
        {

        }
    }
}
