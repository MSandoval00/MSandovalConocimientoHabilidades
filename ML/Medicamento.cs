using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Medicamento
    {
        public int IdMedicamento { get; set; }
        public float Precio { get; set; }
        public string NombreMedicamento { get; set; }
        public int Cantidad { get; set; }
        public float Total { get; set; }
        public List<object> Medicamentos { get; set; }
        public int Piezas { get; set; }
    }
}
