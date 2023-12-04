using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Medicamento
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities context=new DL.MSandovalConicimientoHabilidadesEntities())
                {
                    var query = context.MedicamentoGetAll().ToList();
                    result.Objects=new List<object>();
                    if (query!=null)
                    {
                        foreach (var row in query)
                        {
                            ML.Medicamento medicamento = new ML.Medicamento();
                            medicamento.IdMedicamento = row.IdMedicamento;
                            medicamento.Precio = float.Parse(row.Precio.ToString());
                            medicamento.NombreMedicamento=row.NombreMedicamento;
                            medicamento.Cantidad = int.Parse(row.Cantidad.ToString());
                            medicamento.Total = float.Parse(row.Total.ToString());
                            result.Objects.Add(medicamento);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No hay registros";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
                
            }
            return result;
        }
        public static ML.Result GetById(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities context=new DL.MSandovalConicimientoHabilidadesEntities())
                {
                    var query=context.MedicamentoGetById(IdMedicamento).FirstOrDefault();
                    if (query!=null)
                    {
                        ML.Medicamento medicamento = new ML.Medicamento();
                        medicamento.IdMedicamento = query.IdMedicamento;
                        medicamento.Precio = float.Parse(query.Precio.ToString());
                        medicamento.NombreMedicamento = query.NombreMedicamento;
                        medicamento.Cantidad = int.Parse(query.Cantidad.ToString());
                        medicamento.Total=float.Parse(query.Total.ToString());
                        result.Object = medicamento;
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities context=new DL.MSandovalConicimientoHabilidadesEntities())
                {
                    var query = context.MedicamentoAdd(medicamento.Precio, medicamento.NombreMedicamento, medicamento.Cantidad, medicamento.Total);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo registrar el medicamento";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Medicamento medicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities context=new DL.MSandovalConicimientoHabilidadesEntities())
                {
                    var query = context.MedicamentoUpdate(medicamento.IdMedicamento, medicamento.Precio, medicamento.NombreMedicamento, medicamento.Cantidad, medicamento.Total);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se pudo actualizar el medicamento";
                    }                    
                }
            }
            catch (Exception ex)
            {
                result.Correct=false ;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdMedicamento)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities context=new DL.MSandovalConicimientoHabilidadesEntities())
                {
                    var query = context.MedicamentoDelete(IdMedicamento);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;
                        result.ErrorMessage = "No se pudo eliminar el medicamento";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct= false ;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
