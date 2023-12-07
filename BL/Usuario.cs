using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioGetAll().ToList();
                    result.Objects = new List<object>();
                    if (query!=null)
                    {
                        foreach (var row in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = row.IdUsuario;
                            usuario.Nombre = row.NombreUsuario;
                            usuario.ApellidoPaterno = row.ApellidoPaterno;
                            usuario.ApellidoMaterno = row.ApellidoMaterno;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No hay registros";
                    }
                    result.Correct=true;
                    
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
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioGetById(IdUsuario).First();
                    ML.Usuario usuario=new ML.Usuario();
                    if (query != null)
                    {
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        result.Object = usuario;
                        result.Correct = true;
                    }
                   
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage=ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct=false;
                        result.ErrorMessage = "No se pudo insertar";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioUpdate(usuario.IdUsuario,usuario.Nombre,usuario.ApellidoPaterno,usuario.ApellidoMaterno);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "no se puede actualizar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result=new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioDelete(IdUsuario);
                    if (query>0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct= false;  
                        result.ErrorMessage = "No se pudo eliminar el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result UsuarioGetByEmail(string email)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.MSandovalConicimientoHabilidadesEntities1 context=new DL.MSandovalConicimientoHabilidadesEntities1())
                {
                    var query = context.UsuarioGetByEmail(email).First();
                    if (query!=null)
                    {
                        ML.Usuario usuario=new ML.Usuario();
                        usuario.Rol=new ML.Rol();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.NombreUsuario;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.Rol.IdRol= query.IdRol;
                        usuario.Rol.Nombre = query.NombreRol;
                        result.Object=usuario;
                        result.Correct=true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.ErrorMessage = ex.Message;
                result.Ex=ex;
            }
            return result;
        }
    }
}
