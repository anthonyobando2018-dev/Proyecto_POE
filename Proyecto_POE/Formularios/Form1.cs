using CapaPresentacion.Formularios;
using System.Security.Cryptography;

namespace Proyecto_POE
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            btnPrincipal.ForeColor = Color.White;
            btnPrincipal.FillColor = Color.FromArgb(21, 93, 252);
            FrmPrincipal frm = new FrmPrincipal();
            AbrirFormEnPanel(frm);
        }

        

        private void AbrirFormEnPanel(Form formulario)
        {
            panelPrincipal.Controls.Clear();   // Limpia el panel
            formulario.TopLevel = false;     // Indica que no es ventana independiente
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            panelPrincipal.Controls.Add(formulario);
            formulario.Show();
        }

        private void btnLaboratorio_Click(object sender, EventArgs e)
        {

            FrmLaboratorio frm = new FrmLaboratorio();
            AbrirFormEnPanel(frm);
        }

        private void btnDocentes_Click(object sender, EventArgs e)
        {
            FrmDocente frm = new FrmDocente();
            AbrirFormEnPanel(frm);
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            FrmReservas frm = new FrmReservas();
            AbrirFormEnPanel(frm);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            FrmConsultas frm = new FrmConsultas();
            AbrirFormEnPanel(frm);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            FrmReportes frm = new FrmReportes();
            AbrirFormEnPanel(frm);
        }
    }

}
