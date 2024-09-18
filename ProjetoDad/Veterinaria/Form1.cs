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
using Veterinaria.view;

namespace Veterinaria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void raçaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRaca frm_raca = new FrmRaca();
            frm_raca.ShowDialog();
        }

        private void sexoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSexo frm_Sexo = new FrmSexo();
            frm_Sexo.ShowDialog();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Deseja Sair do sistema?", "SAIR!", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes)
            {


                Close();
            }
        }

        private void tIpoAnimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoAnimal frmTipoAnimal = new FrmTipoAnimal();
            frmTipoAnimal.ShowDialog();
        }

        private void bairroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBairro frmBairro = new FrmBairro();
            frmBairro.ShowDialog();
        }

        private void ruaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Rua frm_Rua = new Frm_Rua();
            frm_Rua.ShowDialog();
        }

        private void cEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_Cep frm_Cep = new Frm_Cep();
            frm_Cep.ShowDialog();
        }

        private void paisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPais frm_Pais = new FrmPais();
            frm_Pais.ShowDialog();
        }

        private void telefoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTelefone frmTelefone = new FrmTelefone();
            frmTelefone.ShowDialog();
        }

        private void tipoFuncionarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoFuncionario frmTipoFuncionario = new FrmTipoFuncionario();
            frmTipoFuncionario.ShowDialog();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarca frmMarca = new FrmMarca();
            frmMarca.ShowDialog();
        }

        private void tipoProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoProduto frmTipoProduto = new FrmTipoProduto();
            frmTipoProduto.ShowDialog();

        }

        private void cIDAnimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCidAnimal frmCidAnimal = new FrmCidAnimal();
            frmCidAnimal.ShowDialog();
        }

        private void tipoServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTipoServico frmTipoServico = new FrmTipoServico();
            frmTipoServico.ShowDialog();
        }
    }
}
