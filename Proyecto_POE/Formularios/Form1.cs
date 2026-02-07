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
            //btnPrincipal.ForeColor = Color.White;
            //btnPrincipal.FillColor = Color.FromArgb(21, 93, 252);
            ResetearBotones();
            ActivarBoton(btnPrincipal);
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
            ResetearBotones();
            ActivarBoton(btnLaboratorio);
            FrmLaboratorio frm = new FrmLaboratorio();
            AbrirFormEnPanel(frm);
        }

        private void btnDocentes_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            ActivarBoton(btnDocentes);
            FrmDocente frm = new FrmDocente();
            AbrirFormEnPanel(frm);
        }

        private void btnReservas_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            ActivarBoton(btnReservas);
            FrmReservas frm = new FrmReservas();
            AbrirFormEnPanel(frm);
        }

        private void btnConsultas_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            ActivarBoton(btnConsultas);
            FrmConsultas frm = new FrmConsultas();
            AbrirFormEnPanel(frm);
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ResetearBotones();
            ActivarBoton(btnReportes);
            FrmReportes frm = new FrmReportes();
            AbrirFormEnPanel(frm);
        }

        private void ResetearBotones()
        {
            btnPrincipal.ForeColor = Color.Black;
            btnPrincipal.FillColor = Color.White;

            btnLaboratorio.ForeColor = Color.Black;
            btnLaboratorio.FillColor = Color.White;

            btnDocentes.ForeColor = Color.Black;
            btnDocentes.FillColor = Color.White;

            btnReservas.ForeColor = Color.Black;
            btnReservas.FillColor = Color.White;

            btnConsultas.ForeColor = Color.Black;
            btnConsultas.FillColor = Color.White;

            btnReportes.ForeColor = Color.Black;
            btnReportes.FillColor = Color.White;
        }

        private void ActivarBoton(Guna.UI2.WinForms.Guna2Button boton)
        {
            boton.ForeColor = Color.White;
            boton.FillColor = Color.FromArgb(21, 93, 252);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Close();
        }
    }


}
