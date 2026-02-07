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
    public partial class FrmDocente : Form
    {
        private readonly DocenteServicio _docenteServicio;

        public FrmDocente(DocenteServicio docenteServicio)
        {
            InitializeComponent();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            _docenteServicio = docenteServicio;
        }

        private void FrmDocente_Load(object sender, EventArgs e)
        {
            CargarDocentesGrid();
            LimpiarControles();
        }

        private void CargarDocentesGrid()
        {
            try
            {
                dgvDocentes.DataSource = null;
                var docentes = _docenteServicio.ListarTodos();
                dgvDocentes.DataSource = docentes;

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
                txtCedula.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error --> " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarNuevoDocente();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatosDeDocente();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarDocente();
        }
        
        private void dgvDocentes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarDocenteSeleccionado(e);
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnRegistrar.Enabled = false;
        }

        
        private void RegistrarNuevoDocente()
        {
            try
            {
                // obtenemos los valores de los campos del formulario
                string cedula = txtCedula.Text.Trim();
                string nombres = txtEspecialidad.Text.Trim();
                string apellidos = txtApellidos.Text.Trim();
                string especialidad = txtNombres.Text.Trim();

                // llamamos al metodo del servicio que guarda el registro
                var idNuevoEstudiante = _docenteServicio.Registrar(cedula, nombres, apellidos, especialidad);

                MessageBox.Show($"Nuevo estudiante con id: {idNuevoEstudiante} registrado!");

                CargarDocentesGrid();

                // SalirModoEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActualizarDatosDeDocente()
        {
            try
            {
                string cedula = txtCedula.Text.Trim();
                string nombres = txtNombres.Text.Trim();
                string apellidos = txtApellidos.Text.Trim();
                string especialidad = txtEspecialidad.Text.Trim();
                bool estado = cmbEstado.SelectedIndex == 0 ? true : false;

                Docente docente = _docenteServicio.BuscarPorCedula(cedula);

                _docenteServicio.ActualizarDatos(docente.IdDocente, cedula, nombres, apellidos, especialidad, estado);

                CargarDocentesGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Datos de docentes actualizado correctamente!", "Registro actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EliminarDocente()
        {
            try
            {
                Docente eliminado = _docenteServicio.BuscarPorCedula(txtCedula.Text);
                _docenteServicio.Inactivar(eliminado.IdDocente);

                CargarDocentesGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Docente inactivado correctamente", "Registro eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarDocenteSeleccionado(DataGridViewCellEventArgs e)
        {
            try
            {
                // obtenemos la fila seleccionada y sus datos para cargarlo en el formulario
                if (e.RowIndex >= 0)
                {
                    var docenteSeleccionado = dgvDocentes.Rows[e.RowIndex].Cells;

                    // txtCodigo.Text = ObtenerValorDeColumna(estudianteSeleccionado, "idDocente");
                    txtCedula.Text = ObtenerValorDeColumna(docenteSeleccionado, "cedula");
                    txtEspecialidad.Text = ObtenerValorDeColumna(docenteSeleccionado, "nombres");
                    txtApellidos.Text = ObtenerValorDeColumna(docenteSeleccionado, "apellidos");
                    txtNombres.Text = ObtenerValorDeColumna(docenteSeleccionado, "especialidad");
                    cmbEstado.SelectedIndex = bool.Parse(ObtenerValorDeColumna(docenteSeleccionado, "estado")) ? 0 : 1;
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
            txtCedula.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtNombres.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;

        }




    }
}
