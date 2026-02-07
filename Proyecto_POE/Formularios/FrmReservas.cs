using CapaAplicacion.Servicios;
using CapaNegocio.Entidades;


namespace CapaPresentacion.Formularios
{
    public partial class FrmReservas : Form
    {
        private readonly ReservaServicio _reservaServicio;
        private readonly LaboratorioServicio _laboratorioServicio;
        private readonly DocenteServicio _docenteServicio;
        private readonly int _jornadaHoraInicio = 8;
        private readonly int _jornadaHoraFin = 16;
        private readonly int _duracionHorario = 30;
        private List<TimeOnly> _horasInicio = new();
        private List<TimeOnly> _horasFin = new();

        public FrmReservas(
            ReservaServicio reservaServicio,
            LaboratorioServicio laboratorioServicio,
            DocenteServicio docenteServicio
            )
        {
            InitializeComponent();
            btnActualizar.Enabled = false;
            btnEliminar.Enabled = false;
            _reservaServicio = reservaServicio;
            _laboratorioServicio = laboratorioServicio;
            _docenteServicio = docenteServicio;
        }


        private void FrmReserva_Load(object sender, EventArgs e)
        {
            LimpiarControles();
            CargarReservasGrid();
            CargarLaboratoriosComboBox();
            CargarDocentesComboBox();
            CargarHorariosAlComboBox();
            
        }

        private void CargarReservasGrid()
        {
            try
            {
                dgvReservas.DataSource = null;
                var reservas = _reservaServicio.ListarTodas();
                dgvReservas.DataSource = reservas;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarLaboratoriosComboBox()
        {
            try
            {
                cmbLab.DataSource = null;
                var laboratorio = _laboratorioServicio.ListarTodos(1);
                cmbLab.DataSource = laboratorio;
                cmbLab.DisplayMember = "nombre";
                cmbLab.ValueMember = "idLaboratorio";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void CargarDocentesComboBox()
        {
            try
            {
                cmbDocente.DataSource = null;
                var docentes = _docenteServicio.ListarTodos(1);
                cmbDocente.DataSource = docentes;
                cmbDocente.DisplayMember = "nombreCompleto";
                cmbDocente.ValueMember = "idDocente";
                
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
                cmbDocente.Focus();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error --> " + ex.Message);
            }
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            RegistrarNuevaReserva();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDatosDeReserva();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            EliminarReserva();
        }

        private void dgvReservas_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CargarReservaSeleccionado(e);
            btnActualizar.Enabled = true;
            btnEliminar.Enabled = true;
            btnRegistrar.Enabled = false;
        }


        private void RegistrarNuevaReserva()
        {
            try
            {
                // obtenemos los valores de los campos del formulario
                int idDocente = Convert.ToInt32(cmbDocente.SelectedValue);
                int idLaboratorio = Convert.ToInt32(cmbLab.SelectedValue);
                string asunto = rtbMotivoReserva.Text.Trim();
                int cantidad_estudiantes = Convert.ToInt32(nruCantidadEstudiantes.Value);
                DateOnly fecha_reserva = DateOnly.FromDateTime(dtpDiaReserva.Value);
                TimeOnly horaInicio = TimeOnly.Parse(cmbHoraInicio.SelectedValue?.ToString() ?? string.Empty);
                TimeOnly horaFin = TimeOnly.Parse(cmbHoraFin.SelectedValue?.ToString() ?? string.Empty);

                // llamamos al metodo del servicio que guarda el registro
                var idNuevoReserva = _reservaServicio.Reservar(idDocente, idLaboratorio, asunto, cantidad_estudiantes, fecha_reserva, horaInicio, horaFin);

                MessageBox.Show($"Nuevo reserva con id: {idNuevoReserva} registrado!");

                CargarReservasGrid();

                // SalirModoEdicion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActualizarDatosDeReserva()
        {
            try
            {
                int idReserva = Convert.ToInt32(txtIdReserva.Text);
                int idDocente = Convert.ToInt32(cmbDocente.SelectedValue);
                int idLaboratorio = Convert.ToInt32(cmbLab.SelectedValue);
                string asunto = rtbMotivoReserva.Text.Trim();
                int cantidad_estudiantes = Convert.ToInt32(nruCantidadEstudiantes.Value);
                DateOnly fecha_reserva = DateOnly.FromDateTime(dtpDiaReserva.Value);
                TimeOnly horaInicio = TimeOnly.Parse(cmbHoraInicio.SelectedValue?.ToString() ?? string.Empty);
                TimeOnly horaFin = TimeOnly.Parse(cmbHoraFin.SelectedValue?.ToString() ?? string.Empty);
                int indiceComboEstado = cmbEstadoReserva.SelectedIndex;
                int estado = indiceComboEstado == 0 ? 1 : (indiceComboEstado == 1 ? 2 : 3);

                _reservaServicio.ActualizarDatos(idReserva, idDocente, idLaboratorio, asunto, cantidad_estudiantes, fecha_reserva, horaInicio, horaFin, estado);

                CargarReservasGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Datos de reservas actualizado correctamente!", "Registro actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EliminarReserva()
        {
            try
            {
                int idLab = Convert.ToInt32(txtIdReserva.Text);
                _reservaServicio.Cancelar(idLab);

                CargarReservasGrid();

                btnActualizar.Enabled = false;
                btnEliminar.Enabled = false;

                MessageBox.Show("Reserva inactivado correctamente", "Registro eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CargarReservaSeleccionado(DataGridViewCellEventArgs e)
        {
            try
            {
                // obtenemos la fila seleccionada y sus datos para cargarlo en el formulario
                if (e.RowIndex >= 0)
                {
                    var reservaSeleccionado = dgvReservas.Rows[e.RowIndex].Cells;

                    // txtCodigo.Text = ObtenerValorDeColumna(estudianteSeleccionado, "idReserva");
                    txtIdReserva.Text = ObtenerValorDeColumna(reservaSeleccionado, "idReserva");

                    int docenteColumna = Convert.ToInt32(ObtenerValorDeColumna(reservaSeleccionado, "idDocente"));

                    Docente docente = _docenteServicio.BuscarPorId(docenteColumna);
                    int docenteIdCombo = cmbDocente.Items.Cast<Docente>().ToList().IndexOf(docente);

                    cmbDocente.SelectedIndex = docenteIdCombo;

                    int laboratorioColumna = Convert.ToInt32(ObtenerValorDeColumna(reservaSeleccionado, "idLaboratorio"));

                    Laboratorio laboratorio = _laboratorioServicio.BuscarPorId(laboratorioColumna);
                    int laboratorioIdCombo = cmbLab.Items.Cast<Laboratorio>().ToList().IndexOf(laboratorio);

                    cmbLab.SelectedIndex = laboratorioIdCombo;
                    nruCantidadEstudiantes.Value = Convert.ToDecimal(ObtenerValorDeColumna(reservaSeleccionado, "cantidadEstudiantes"));
                    rtbMotivoReserva.Text = ObtenerValorDeColumna(reservaSeleccionado, "asunto");
                    dtpDiaReserva.Value = DateTime.Parse(ObtenerValorDeColumna(reservaSeleccionado, "fechaReserva"));
                    var horario = ObtenerValorDeColumna(reservaSeleccionado, "horario").ToString().Split('-'); 
                    cmbHoraInicio.SelectedIndex = ObtenerIndiceDeHorario(cmbHoraInicio, horario.ElementAt(0));
                    cmbHoraFin.SelectedIndex = ObtenerIndiceDeHorario(cmbHoraFin, horario.ElementAt(1));
                    int estadoReserva = Convert.ToInt32(ObtenerValorDeColumna(reservaSeleccionado, "estado"));
                    cmbEstadoReserva.SelectedIndex = estadoReserva == 1 ? 0 : (estadoReserva == 2 ? 1 : 2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /*
        private int ObtenerIndiceDeDocenteEnComboBox(int idDocente)
        {
            int laboratorioComboBoxId = cmbDocentes.Items.Cast<Laboratorio>().ToList().IndexOf(Convert.ToInt32(ObtenerValorDeColumna(reservaSeleccionado, "idLaboratorio")));
            for (int i = 0; cmbDocente.Items.Count > 0; i++)
            {
                var item = Convert.ToInt32(cmbDocente.Items[i]);
                if (item == idDocente) return i;
            }
            return -1;
        }
        */
        private int ObtenerIndiceDeLaboratorioEnComboBox(int idLaboratorio)
        {
            for (int i = 0; cmbDocente.Items.Count > 0; i++)
            {
                var item = Convert.ToInt32(cmbLab.Items[i]);
                if (item == idLaboratorio) return i;
            }
            return -1;
        }

        private int ObtenerIndiceDeHorario(ComboBox combo, string horario)
        {
            for (int i = 0; combo.Items.Count > 0; i++)
            {
                string item = combo.Items[i]?.ToString() ?? string.Empty;
                if (item.Equals(horario)) return i;
            }
            return -1;
        }

        private void CargarHorariosAlComboBox()
        {
            var bloquesHorarios = GeneradorHorariosServicio.GenerarHorarios(_jornadaHoraInicio, _jornadaHoraFin, _duracionHorario);

            bloquesHorarios.ForEach(bloque =>
            {
                _horasInicio.Add(bloque.HoraInicio);
                _horasFin.Add(bloque.HoraFin);
            });
            cmbHoraInicio.DataSource = _horasInicio;
            cmbHoraFin.DataSource = _horasFin;
        }
        private string ObtenerValorDeColumna(DataGridViewCellCollection fila, string campo)
        {
            return fila[campo].Value.ToString() ?? string.Empty;
        }

        private void LimpiarControles()
        {
            txtIdReserva.Text = string.Empty;
            cmbDocente.SelectedIndex = 0;
            cmbLab.SelectedIndex = 0;
            nruCantidadEstudiantes.Value = 0;
            rtbMotivoReserva.Text = string.Empty;
            cmbHoraFin.SelectedIndex = 0;
            cmbHoraFin.SelectedIndex = 0;
            cmbEstadoReserva.SelectedIndex = 0;
        }
    }
}
