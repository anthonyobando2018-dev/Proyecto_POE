using CapaAplicacion.Servicios;
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
    public partial class FrmReportes : Form
    {
        private readonly ReporteServicio _reporteServicio;

        public FrmReportes(ReporteServicio reporteServicio)
        {
            InitializeComponent();
            _reporteServicio = reporteServicio;
        }

        private void btnGenerarReporte_Click(object sender, EventArgs e)
        {
            try
            {
                var fechaInicioI = DateOnly.FromDateTime(fechaInicio.Value);
                var fechaFinI = DateOnly.FromDateTime(fechaFin.Value);
                rtbReserva.Text = _reporteServicio.GenerarReporte(fechaInicioI, fechaFinI);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
