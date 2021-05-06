using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailTask
{
    public class Program
    {
        static void Main(string[] args)
        {
            Conexion con = new Conexion();
            List<CorreosAEnviar> _correosPendientes = new List<CorreosAEnviar>();

            _correosPendientes = con.Consulta();

            if (_correosPendientes != null && _correosPendientes.Count != 0)
            {
                foreach (CorreosAEnviar item in _correosPendientes)
                {
                    try
                    {
                        con.DeleteEmail(item.Id, item);

                        Console.WriteLine(item.SubjectEmail);
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.Message + " --- " + ex.InnerException);
                    }

                }
            }
        }
    }
}
