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
    public partial class FrmDocente : Form
    {
        public FrmDocente()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCodigo.Text != string.Empty)
                    obj_cln_est.IdEstudiante = int.Parse(txtCodigo.Text);
                obj_cln_est.Cedula = txtCedula.Text;
                obj_cln_est.Nombres = txtNombres.Text;
                obj_cln_est.Apellidos = txtApellidos.Text;
                obj_cln_est.Especialidad = txtEspecialidad.Text;
                if (cmbEstado.SelectedIndex == 0)
                    obj_cln_est.Genero = 'M';
                else
                    obj_cln_est.Genero = 'F';


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
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
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
                LimpiarCampos();
                ListarLaboratorios();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                is_nuevo = true;
                LimpiarControles();

                btnRegistrar.Enabled = true;
                btnEliminar.Enabled = true;
                btnNuevo.Enabled = false;
                btnRegistrar.Text = "Grabar";
                txtCedula.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error --> " + ex.Message);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
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
            }
        }

        private void LimpiarControles()
        {   
            txtCedula.Text = string.Empty;
            txtNombres.Text = string.Empty;
            txtApellidos.Text = string.Empty;
            txtEspecialidad.Text = string.Empty;
            cmbEstado.SelectedIndex = 0;

        }

    }
}
