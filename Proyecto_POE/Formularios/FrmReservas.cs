using SRLCProyectoPOE.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class FrmReservas : Form
    {
        public FrmReservas()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            bool habilitarGuardado = VerificarCampos();
            if (habilitarGuardado)
            {
                try
                {
                    Reserva reservaNueva = ObtenerReservaDelFormulario();
                    if (btnGuardar.Text == "Guardar")
                    {
                        ReservaAdministradora.AgregarReserva(reservaNueva);
                        MessageBox.Show("Reserva registrada con exito!");
                        LimpiarControles();
                        DesactivarControles();
                    }
                    else
                    {
                        reservaNueva.IdReserva = Convert.ToInt32(tbId.Text);
                        ReservaAdministradora.ActualizarDatosReserva(reservaNueva);
                        MessageBox.Show("Reserva actualizada con exito!");
                    }
                    btnNuevo.Enabled = true;
                    RefrescarTablaReservas();
                }
                catch (Exception ex)
                {
                    ErrorPersonalizado(ex);
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivarControles();
            LimpiarControles();
            btnNuevo.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idReserva = Convert.ToInt32(tbId.Text);
                ReservaAdministradora.EliminarReserva(idReserva);
                MessageBox.Show("Reserva eliminada con exito!");
                RefrescarTablaReservas();
                LimpiarControles();
                DesactivarControles();
                btnEliminar.Enabled = false;
            }
            catch (Exception ex)
            {
                ErrorPersonalizado(ex);
            }
        }

        private void DesactivarControles()
        {
            cmbDocente.Enabled = false;
            cmbLab.Enabled = false;
            rtbMotivoReserva.Enabled = false;
            nruCantidadEstudiantes.Enabled = false;
            dtpDiaReserva.Enabled = false;
            cmbHoraInicio.Enabled = false;
            cmbHoraFin.Enabled = false;
            btnRegistrar.Enabled = false;
            btnNuevo.Enabled = false;
            chkbEstadoReserva.Enabled = false;
        }

        private void ActivarControles()
        {
            cbDocente.Enabled = true;
            cbLaboratorio.Enabled = true;
            rtbMotivoReserva.Enabled = true;
            nudCantidadEstudiantes.Enabled = true;
            dtpDiaReserva.Enabled = true;
            cbHoraInicio.Enabled = true;
            cbHoraFin.Enabled = true;
            btnGuardar.Enabled = true;
            btnLimpiar.Enabled = true;
            chkbEstadoReserva.Enabled = true;
        }

        private void ErrorPersonalizado(Exception ex)
        {
            string titulo = string.Empty;
            if (ex is ArgumentNullException)
                titulo = "Valor inexistente";
            else if (ex is ArgumentException)
                titulo = "Valor ingresado no valido";
            else
                titulo = "Algo salio mal";
            MessageBox.Show($"Error: {ex.Message}", titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LimpiarControles()
        {
            cmbDocente.SelectedIndex = -1;
            cmbLab.SelectedIndex = -1;
            cmbHoraInicio.SelectedIndex = -1;
            cmbHoraFin.SelectedIndex = -1;
            rtbMotivoReserva.Text = string.Empty;
            tbId.Text = ReservaAdministradora.GetIdActual().ToString();
            dtpDiaReserva.Value = DateTime.Today;
            nruCantidadEstudiantes.Value = 0;
            btnRegistrar.Text = "Guardar";
        }
    }
}
