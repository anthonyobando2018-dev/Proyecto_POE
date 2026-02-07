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

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (txtCodigo.Text != string.Empty)
                    obj_cln_est.IdEstudiante = int.Parse(txtCodigo.Text);
                obj_cln_est.Nombre = txtNombreLab.Text;
                obj_cln_CapacidadMax = (int)numCapacidad.Value;
                if (cmbEstado.SelectedIndex == 0)
                    obj_cln_est.Genero = '';
                else
                    obj_cln_est.Genero = '';
                else
                    obj_cln_est.Genero = '';


                //Si es nuevo
                if (is_nuevo)
                {
                    ValidarEstudiante();
                    if (obj_cln_est.InsertarEstudiante())
                    {
                        MessageBox.Show("Estudiabte creado");
                        is_nuevo = false;
                        LimpiarControles();
                    }
                    else MessageBox.Show("No se pudo crear estudiante");

                }
                else
                {
                    //actualizar estudiante 
                    ValidarEstudiante();
                    obj_cln_est.ActualizarEstudiante();
                    //Actualizar estado del Boton
                    btnGrabar.Text = "Guardar";
                    //Mandar un mensaje 
                    MessageBox.Show("Estudiante Actualizado");
                    LimpiarControles();
                }
                btnNuevo.Enabled = true;
                btnGrabar.Enabled = false;
                btnEliminar.Enabled = false;
                //Cargar datos al grid
                CargarEstudiantesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al grabar los datos del estudiante  --> " + ex.Message);
            }*/
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            /*try
            {
                if (!int.TryParse(txtCodigo.Text, out int codigo))
                {
                    MessageBox.Show("Ingrese un codigo valido para actualizar.");
                    return;
                }

                Laboratorio lab = new Laboratorio
                {
                    IdLaboratorio = codigo,
                    Nombre = txtNombre.Text.Trim(),
                    CapacidadMaxima = (int)numCapacidad.Value,
                    Estado = chkActivo.Checked
                };

                LaboratorioAdministradora.ActualizarLaboratorio(lab);
                MessageBox.Show("Laboratorio actualizado correctamente.");
                LimpiarControles();
                CargarEstudiantesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }*/
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                //is_nuevo = true;
                LimpiarControles();

                btnGuardar.Enabled = true;
                btnEliminar.Enabled = false;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = false;
                txtNombreLab.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error --> " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            /*try
            {
                obj_cln_est.IdEstudiante = int.Parse(txtCodigo.Text);

                if (obj_cln_est.EliminarEstudiante())
                {
                    MessageBox.Show("Docente Eliminado");
                    CargarEstudiantesGrid();
                    LimpiarControles();
                }
                else MessageBox.Show("No se pudo eliminar");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error -> " + ex.Message);
            }*/
        }

        private void dgvLab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*try
            {
                if (e.RowIndex < 0 || e.RowIndex >= dgvLab.Rows.Count)
                    return;
                DataGridViewRow fila = dgvLab.Rows[e.RowIndex];

                //txtCodigo.Text = fila.Cells["id_estudiante"].Value?.ToString() ?? "";
                txtNombreLab.Text = fila.Cells["nombre"].Value?.ToString() ?? "";
                numCapacidad.Value = decimal.Parse(fila.Cells["capacidad"].Value.ToString());
                string estado = fila.Cells["estado"].Value?.ToString();
                if (estado == "Activo")
                    cmbEstadoLab.SelectedIndex = 0; // Activo
                else if (estado == "Inactivo")
                    cmbEstadoLab.SelectedIndex = 1; //Inactivo
                else
                    cmbEstadoLab.SelectedIndex = -1;

                is_nuevo = false;

                btnGuardar.Enabled = false;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = true;
                btnActualizar.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error -> " + ex.Message);
            }*/
        }

        private void LimpiarControles()
        {
            txtNombreLab.Text = string.Empty;
            numCapacidad.Value = 0;
            cmbEstadoLab.SelectedIndex = 0;

        }

        private void CargarEstudiantesGrid()
        {
            try
            {
                dgvLab.DataSource = null;
                //dgvLab.DataSource = obj_cln_est.GetListadoEstudiantes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
