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
    public partial class FrmSexo : Form
    {

        DataTable Tabela_sexo;


        List<Sexo> lista_sexo = new List<Sexo>();

        Boolean novo = true;
        int posicao;

        public FrmSexo()
        {

            InitializeComponent();
            //Carregar o Datagrid de Sexo
            CarregaTabela();
            


            lista_sexo = carregaListaSexo();



            if (lista_sexo.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void atualizaCampos()
        {
            txtCod.Text = lista_sexo[posicao].codsexo.ToString();
            txtSexo.Text = lista_sexo[posicao].nomesexo.ToString();
        }

        List<Sexo> carregaListaSexo()
        {
            List<Sexo> lista = new List<Sexo>();
            C_Sexo cr = new C_Sexo();
            lista = cr.DadosSexo();

        

            return lista;

        }
        private void FrmSexo_Load(object sender, EventArgs e)
        {

        }

        List<Sexo> carregaListaSexoFiltro()
        {
            List<Sexo> lista = new List<Sexo>();
            C_Sexo cr = new C_Sexo();
            lista = cr.DadosSexoFiltro(txtBuscar.Text);

            return lista;

        }


        private void desativaCampos()
        {
            txtSexo.Enabled = false;
        }


        private void desativaBotoes()
        {
            btnAdd.Enabled = true;
            btnSalvar.Enabled = false;
            btnCancel.Enabled = false;

            btnEditar.Enabled = true;
            btnExcluir.Enabled = true;
        }

        private void limparCampos()
        {
            txtCod.Text = "";
            txtSexo.Text = "";
        }

        private void ativarCampos()
        {
            txtSexo.Enabled = true;
        }

        private void ativaBotoes()
        {
            btnAdd.Enabled = false;
            btnSalvar.Enabled = true;
            btnCancel.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
                limparCampos();

                ativarCampos();

                ativaBotoes();

                novo = true;
            
        }

        public void CarregaTabela()
        {

            C_Sexo cr = new C_Sexo();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            Tabela_sexo = dt;
            dataGridView1.DataSource = dt;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            
            Sexo sexo = new Sexo();
            sexo.nomesexo = txtSexo.Text;

            C_Sexo c_Sexo = new C_Sexo();

            if (novo == true)
            {
                c_Sexo.Insere_Dados(sexo);

            }
            else
            {
                sexo.codsexo = Int32.Parse(txtCod.Text);
                c_Sexo.Atualizar_Dados(sexo);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_sexo = carregaListaSexo();



            if (lista_sexo.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            
                limparCampos();
                desativaBotoes();
                desativaCampos();
           
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            
                C_Sexo c_Sexo = new C_Sexo();


                if (txtCod.Text != "")
                {
                    int valor = Int32.Parse(txtCod.Text);
                    c_Sexo.Apaga_Dados(valor);
                    CarregaTabela();
                }

            CarregaTabela();



            lista_sexo = carregaListaSexo();



            if (lista_sexo.Count > 0)
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

        private void btnProximo_Click(object sender, EventArgs e)
        {

            int total = lista_sexo.Count - 1;
            if (total > posicao )
            {
                dataGridView1.Rows[posicao].Selected = false;
                posicao++;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows[posicao].Selected = false;
            posicao = lista_sexo.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtCod.Text = dr.Cells[0].Value.ToString();
            txtSexo.Text = dr.Cells[1].Value.ToString();
        }

        private void txtBuscar_Click(object sender, EventArgs e)
        {

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_SEXO
            C_Sexo cr = new C_Sexo();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            Tabela_sexo = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = Tabela_sexo;


            //Carrega a Lista_Sexo com o valor da consulta com parâmetro
            lista_sexo = carregaListaSexoFiltro();

            if (lista_sexo.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }
    }
}
