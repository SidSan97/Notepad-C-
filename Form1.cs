using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace Bloco_de_Notas_C_Sharppe
{
    public partial class JanelaPrincipal : Form
    {
        public string LastSaveLocation = "";
        string texto;

        public JanelaPrincipal()
        {
            InitializeComponent();
        }

        private void SalvarClick(object sender, EventArgs e)
        {
        
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Arquivos de texto|*.txt"; 
            sfd.ShowDialog();

           if (sfd.FileName != "") 
            {
             LastSaveLocation = sfd.FileName;
             System.IO.File.WriteAllText(LastSaveLocation, JanelaDeTxt.Text);      
            }
            
        }

        private void salvarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (LastSaveLocation == "")
            {
                SalvarClick(sender , e);
            }
            else
            {
                System.IO.File.WriteAllText(LastSaveLocation, JanelaDeTxt.Text);
            }
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(this.JanelaDeTxt.Text))
            {
                DialogResult result = MessageBox.Show("Deseja salvar arquivo?", "Sair do programa", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {                  
                    SalvarClick(sender, e);
                    JanelaDeTxt.Clear();
                    Application.Exit();
                }
                else if(result == DialogResult.No)
                {
                    JanelaDeTxt.Clear();
                    Application.Exit();                  
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            OpenFileDialog ofd = new OpenFileDialog();
            if(ofd.ShowDialog() == DialogResult.OK && string.IsNullOrEmpty(this.JanelaDeTxt.Text))
            {
                this.JanelaDeTxt.Text = File.ReadAllText(ofd.FileName);
            }
       }   

        private void JanelaPrincipal_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.JanelaDeTxt.Text))
            {
                DialogResult result = MessageBox.Show("Deseja salvar arquivo?", "Sair do programa", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    SalvarClick(sender, e);
                }              
            }
            else
            {
                Application.Exit();
            }
        }
   

        private void NewFile(bool saveFile)
        {
            if (saveFile == true)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                if(sfd.ShowDialog() == DialogResult.OK /*&& JanelaDeTxt.Text != ""*/)
                {
                    File.WriteAllText(sfd.FileName, this.JanelaDeTxt.Text);
                }
            }
            this.JanelaDeTxt.Clear(); //LIMPA AREA DE TEXTO
            this.Text = "Sem título";
        }

        //FONTE DOS CARACTERES DO BLOCO DE TEXTO
        private void fonteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            if (fd.ShowDialog() == DialogResult.OK)
                this.JanelaDeTxt.Font = fd.Font;
        }

        private void novoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Deseja salvar arquivo?", "Sair do programa", MessageBoxButtons.YesNoCancel);
            if(result == DialogResult.Yes && !string.IsNullOrEmpty(this.JanelaDeTxt.Text))
            {
                NewFile(true);
            }
            else if(result == DialogResult.No)
            {
                NewFile(false);
            }
        }

        private void imprimirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringReader leitor;
            printDialog1.Document = printDocument1;
            string texto = this.JanelaDeTxt.Text;
            leitor = new StringReader(texto);

            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                this.printDocument1.Print();
            }
        }

        private void copiarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JanelaDeTxt.SelectedText != "")
            {
                string texto1 = JanelaDeTxt.SelectedText;
                Clipboard.SetText(texto1);
            }
        }

        private void colarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                if (JanelaDeTxt.Text != "")
                {
                    JanelaDeTxt.Multiline = true;
                    JanelaDeTxt.Text =  JanelaDeTxt.Text + Clipboard.GetText();
                }
                else
                    JanelaDeTxt.Text = Clipboard.GetText();
            }
        }

        private void recortarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JanelaDeTxt.SelectedText != "")
            {
                string texto = JanelaDeTxt.SelectedText;
                Clipboard.SetText(texto);
                JanelaDeTxt.SelectedText = "";
            }
        }

        private void deletarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JanelaDeTxt.SelectedText != "")
            {
                JanelaDeTxt.SelectedText = "";
            }
        }

        private void desfazerToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            if (JanelaDeTxt.Text != "")
            {
                texto = JanelaDeTxt.Text;
                JanelaDeTxt.Text = "";
            }
            else if (JanelaDeTxt.Text == "")
            {
               JanelaDeTxt.Text = texto;
            }
        }

        private void selecionarTudoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (JanelaDeTxt.Text != "")
            {
                JanelaDeTxt.SelectAll();
            }
        }

        private void JanelaDeTxt_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void JanelaPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void editarToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
