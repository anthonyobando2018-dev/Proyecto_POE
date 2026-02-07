using CapaAplicacion.Servicios;
using CapaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion.Formularios
{
    public partial class FrmLaboratorio : Form
    {
        private readonly LaboratorioServicio _laboratorioServicio;

        public FrmLaboratorio(LaboratorioServicio laboratorioServicio)
        {
            InitializeComponent();
            btnEliminar.Enabled = false;
            btnActualizar.Enabled = false;
            _laboratorioServicio = laboratorioServicio;
        }


        private void FrmLaboratorio_Load(object sender, EventArgs e)
        {
            CargarLaboratoriosGrid();
            LimpiarControles();
        }

        private void CargarLaboratoriosGrid()
        {
            try
            {
                dgvLaboratorios.DataSource = null;
                var laboratorios = _laboratorioServicio.ListarTodos();
                dgvLaboratorios.DataSource = laboratorios;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                //is_nuevo = true;
                LimpiarControles();
                btnRegistrar.Enabled = true;
                txtNombreLab.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error --> " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarNuevoLaboratorio();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatosDeLaboratorio();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarLaboratorio();
        }

        private void dgvLaboratorios_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarLaboratorioSeleccionado(e);
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnRegistrar.Enabled = false;
        }


        private void RegistrarNuevoLaboratorio()
        {
            try
            {
                // obtenemos los valores de los campos del formulario
                string nombre = txtNombreLab.Text.Trim();
                int capacidad_estudiantes = Convert.ToInt32(numCapacidad.Value);

                // llamamos al metodo del servicio que guarda el registro
                var idNuevoLaboratorio = _laboratorioServicio.Registrar(nombre, capacidad_estudiantes);

                MessageBox.Show($"Nuevo laboratorio con id: {idNuevoLaboratorio} registrado!");

                CargarLaboratoriosGrid();

                // SalirModoEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActualizarDatosDeLaboratorio()
        {
            try
            {
                int idLab = Convert.ToInt32(txtIdLab.Text);
                string nombre = txtNombreLab.Text.Trim();
                int capacidad_estudiantes = Convert.ToInt32(numCapacidad.Value);
                int estado = cmbEstadoLab.SelectedIndex == 0 ? 1 : 0;

                Laboratorio laboratorio = _laboratorioServicio.BuscarPorId(idLab);

                _laboratorioServicio.ActualizarDatos(laboratorio.IdLaboratorio, nombre, capacidad_estudiantes, estado);

                CargarLaboratoriosGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Datos de laboratorios actualizado correctamente!", "Registro actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EliminarLaboratorio()
        {
            try
            {
                int idLab = Convert.ToInt32(txtIdLab.Text);
                _laboratorioServicio.Inactivar(idLab);

                CargarLaboratoriosGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Laboratorio inactivado correctamente", "Registro eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarLaboratorioSeleccionado(DataGridViewCellEventArgs e)
        {
            try
            {
                // obtenemos la fila seleccionada y sus datos para cargarlo en el formulario
                if (e.RowIndex >= 0)
                {
                    var laboratorioSeleccionado = dgvLaboratorios.Rows[e.RowIndex].Cells;

                    // txtCodigo.Text = ObtenerValorDeColumna(estudianteSeleccionado, "idLaboratorio");
                    txtIdLab.Text = ObtenerValorDeColumna(laboratorioSeleccionado, "idLaboratorio");
                    txtNombreLab.Text = ObtenerValorDeColumna(laboratorioSeleccionado, "nombre");
                    numCapacidad.Value = Convert.ToDecimal(ObtenerValorDeColumna(laboratorioSeleccionado, "capacidadMaxima"));
                    cmbEstadoLab.SelectedIndex = Convert.ToInt32(ObtenerValorDeColumna(laboratorioSeleccionado, "estado")) == 1 ? 0 : 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private string ObtenerValorDeColumna(DataGridViewCellCollection fila, string campo)
        {
            return fila[campo].Value.ToString() ?? string.Empty;
        }

        private void LimpiarControles()
        {
            txtIdLab.Text = string.Empty;
            txtNombreLab.Text = string.Empty;
            numCapacidad.Value = 0;
            cmbEstadoLab.SelectedIndex = 0;

        }
    }
}
