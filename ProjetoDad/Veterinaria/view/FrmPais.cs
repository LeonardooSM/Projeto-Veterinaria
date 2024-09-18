using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veterinaria.control;
using Veterinaria.model;

namespace Veterinaria.view
{
    public partial class FrmPais : Form
    {

        DataTable tabela_pais;


        List<Pais> lista_pais = new List<Pais>();

        Boolean novo = true;
        int posicao;

        byte[] aux;
        public FrmPais()
        {
            InitializeComponent();
            //Carregar o Datagrid de Bairro
            CarregaTabela();



            lista_pais = carregaListaPais();



            if (lista_pais.Count > 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        List<Pais> carregaListaPais()
        {
            List<Pais> lista = new List<Pais>();
            C_Pais cr = new C_Pais();
            lista = cr.DadosPais();



            return lista;

        }


        public void CarregaTabela()
        {

            C_Pais cr = new C_Pais();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Todos();
            tabela_pais = dt;
            dataGridView1.DataSource = dt;
        }



        private void desativaCampos()
        {
            txtPais.Enabled = false;
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
            txtPais.Text = "";
        }

        private void ativarCampos()
        {
            txtPais.Enabled = true;
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
            txtcod.Text = lista_pais[posicao].codpais.ToString();
            txtPais.Text = lista_pais[posicao].nomepais.ToString();
            if (lista_pais[posicao].bandeira != null && lista_pais[posicao].bandeira.Length > 0)
            {
                using (MemoryStream ms = new MemoryStream(lista_pais[posicao].bandeira))
                {
                    pictureBox1.Image = Image.FromStream(ms);
                }
            } else
            {
                pictureBox1.Image = null;
            }
            
            

        }

       

        private void btnImg_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Image img = Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = img;

                FileStream Stream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader binary = new BinaryReader(Stream);
                aux = binary.ReadBytes((int)Stream.Length);
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
            Pais pais = new Pais();
            pais.nomepais = txtPais.Text;
            pais.bandeira = aux;
            
            
            

            C_Pais c_Pais = new C_Pais();

            if (novo == true)
            {
                c_Pais.Insere_Dados(pais);

            }
            else
            {
                pais.codpais = Int32.Parse(txtcod.Text);
                c_Pais.Atualizar_Dados(pais);

            }

            CarregaTabela();
            desativaCampos();
            desativaBotoes();

            CarregaTabela();



            lista_pais = carregaListaPais();



            if (lista_pais.Count > 0)
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
            C_Pais c_Pais = new C_Pais();


            if (txtcod.Text != "")
            {
                int valor = Int32.Parse(txtcod.Text);
                c_Pais.Apaga_Dados(valor);
                CarregaTabela();
            }

            CarregaTabela();



            lista_pais = carregaListaPais();



            if (lista_pais.Count > 0)
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
            dataGridView1.Rows[posicao].Selected = false;
            posicao--;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnProx_Click(object sender, EventArgs e)
        {
            int total = lista_pais.Count - 1;
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
            posicao = lista_pais.Count - 1;
            atualizaCampos();
            dataGridView1.Rows[posicao].Selected = true;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            //Foi definido um atributo chamado cr do tipo C_Pais
            C_Pais cr = new C_Pais();
            DataTable dt = new DataTable();
            dt = cr.Buscar_Filtro(txtBuscar.Text.ToString() + "%");
            tabela_pais = dt;

            //Adiciona os dados do DataTable para o DataGridView
            dataGridView1.DataSource = tabela_pais;


            //Carrega a lista_pais com o valor da consulta com parâmetro
            lista_pais = carregaListaPais();

            if (lista_pais.Count - 1 >= 0)
            {
                posicao = 0;
                atualizaCampos();
                dataGridView1.Rows[posicao].Selected = true;
            }
        }

        private void FrmPais_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow dr = dataGridView1.Rows[index];
            txtcod.Text = dr.Cells[0].Value.ToString();
            txtPais.Text = dr.Cells[1].Value.ToString();
            atualizaCampos();
           
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
