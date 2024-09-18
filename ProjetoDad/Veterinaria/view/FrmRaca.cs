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
    public partial class FrmRaca : Form
    {

        DataTable Tabela_raca;


        List<Raca> lista_raca = new List<Raca>();

        Boolean novo = true;
        int posicao;

        public FrmRaca()
        {
            InitializeComponent();
            
            //Carregar o Datagrid de Raças
            CarregaTabela();

            
            lista_raca = carregaListaRaca();

           

            if(lista_raca.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

            
        }

        private void atualizaCampos()
        {
            txtcod.Text = lista_raca[posicao].codraca.ToString();
            txtRaca.Text = lista_raca[posicao].nomeraca.ToString();
        }

        List<Raca>carregaListaRaca()
        {
            List<Raca> lista = new List<Raca>();
            C_Raca cr = new C_Raca();
            lista = cr.DadosRaca();

            return lista;
            
        }

        List<Raca> carregaListaRacaFiltro()
        {
            List<Raca> lista = new List<Raca>();
            C_Raca cr = new C_Raca();
            lista = cr.DadosRacaFiltro(txtBuscar.Text);

            return lista;

        }



        public void CarregaTabela()
        {
            C_Raca cr = new C_Raca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_raca = dt;
            dataGridView1.DataSource = dt;

        }

        private void FrmRaca_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];  
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtRaca.Text = dr.Cells[1].Value.ToString();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            limparCampos();

            ativarCampos();

            ativaBotoes();

            novo = true;

        }

        private void ativaBotoes()
        {
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnApagar.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void ativarCampos()
        {
            txtRaca.Enabled = true;
        }

        private void limparCampos()
        {
            txtcod.Text = "";
            txtRaca.Text = "";
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Raca raca = new Raca();
            raca.nomeraca = txtRaca.Text;
            
            

            C_Raca c_Raca = new C_Raca();

            if (novo == true)
            {


                c_Raca.Insere_Dados(raca);

            } else
            {

                raca.codraca = Int32.Parse(txtcod.Text);
                c_Raca.Atualizar_Dados(raca);
            }
            


            CarregaTabela();

            desativaCampos();

            desativaBotoes();

            CarregaTabela();



            lista_raca = carregaListaRaca();



            if (lista_raca.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

        }

        private void desativaBotoes()
        {
            btnNovo.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;

            btnEditar.Enabled = true;
            btnApagar.Enabled = true;
        }

        private void desativaCampos()
        {
            txtRaca.Enabled =  false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            limparCampos();
            desativaBotoes();
            desativaCampos();
        }

        private void btnApagar_Click(object sender, EventArgs e)
        {
            C_Raca c_Raca = new C_Raca();




            if(txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Raca.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_raca = carregaListaRaca();



            if (lista_raca.Count > 0)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtRaca_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = 0;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnUlt_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_raca.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao ].Selected = true;

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
            int total = lista_raca.Count - 1;
            if (total > posicao)
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            //Foi definido um atributo chamado cr do tipo C_Raca
            C_Raca cr = new C_Raca();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_raca = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_raca;


            //Carrega a Lista_raca com o valor da consulta com parâmetro
            lista_raca = carregaListaRacaFiltro();

            if (lista_raca.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }

           
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {

        }
    }
}
