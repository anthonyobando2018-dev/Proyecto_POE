using CapaAplicacion.Servicios;
using CapaNegocio.Entidades;
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
        private readonly ReservaServicio _reservaServicio;

        public FrmReservas(ReservaServicio reservaServicio)
        {
            InitializeComponent();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            _reservaServicio = reservaServicio;
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            /*bool habilitarGuardado = VerificarCampos();
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
            }*/
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            ActivarControles();
            LimpiarControles();
            btnRegistrar.Enabled = true;
            btnNuevo.Enabled = true;
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            /*try
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
            }*/
        }

        private void dgvReserva_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
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
            cmbEstadoReserva.Enabled = false;
        }

        private void ActivarControles()
        {
            cmbDocente.Enabled = true;
            cmbLab.Enabled = true;
            rtbMotivoReserva.Enabled = true;
            nruCantidadEstudiantes.Enabled = true;
            dtpDiaReserva.Enabled = true;
            cmbHoraInicio.Enabled = true;
            cmbHoraFin.Enabled = true;
            btnRegistrar.Enabled = true;
            btnNuevo.Enabled = true;
            cmbEstadoReserva.Enabled = true;
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
            dtpDiaReserva.Value = DateTime.Today;
            nruCantidadEstudiantes.Value = 0;
            btnRegistrar.Text = "Guardar";
        }

        private void RefrescarTablaReservas()
        {
            try
            {
                dgvReserva.DataSource = null;
                //dgvReserva.DataSource = obj_cln_est.GetListadoEstudiantes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
