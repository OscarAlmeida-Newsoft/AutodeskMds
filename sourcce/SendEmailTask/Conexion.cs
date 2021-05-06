using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SendEmailTask
{
    public class Conexion
    {
        SqlCommand _sqlCommand = null;
        SmtpClient _client = GetSMTPClient();
        List<CorreosAEnviar> _correosList = new List<CorreosAEnviar>();
        string Message;
        string _stringConection = ConfigurationManager.ConnectionStrings["AffidavitModelsConnection"].ConnectionString;
        
        public Conexion()
        {

        }


        public List<CorreosAEnviar> Consulta()
        {
            using (SqlConnection _dbConn = new SqlConnection(_stringConection))
            {

                _sqlCommand = _dbConn.CreateCommand();

                try
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "NS_spCorreosAEnviar_Select";


                    DataTable _dataTable = new DataTable();
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);

                    _dbConn.Open();
                    _sqlDataAdapter.Fill(_dataTable);

                    foreach (DataRow rsd in _dataTable.Rows)
                    {
                        int idNumber = Convert.ToInt32(rsd["Id"]);
                        string toEmail = rsd["ToEmail"].ToString();
                        string fromEmail = rsd["FromEmail"].ToString();
                        string subjectEmail = rsd["SubjectEmail"].ToString();
                        string bodyEmail = rsd["BodyEmail"].ToString();


                        _correosList.Add(new CorreosAEnviar()
                        {
                            Id = idNumber,
                            ToEmail = toEmail,
                            FromEmail = fromEmail,
                            SubjectEmail = subjectEmail,
                            BodyEmail = bodyEmail,
                        });

                    }
                    return _correosList;
                }
                catch (SqlException sqlEx)
                {
                    Message = sqlEx.Message;
                    return null;
                }
                catch (Exception ex)
                {
                    Message = ex.Message;
                    return null;
                }
                finally
                {
                    if (_sqlCommand != null)
                    {
                        _dbConn.Close();
                        _dbConn.Dispose();

                    }
                }
            }

        }

        public void DeleteEmail(int IdEmail, CorreosAEnviar item)
        {
            using (SqlConnection _dbConn = new SqlConnection(_stringConection))
            {
                SqlTransaction transaction = null;
                _sqlCommand = _dbConn.CreateCommand();

                try
                {
                    _sqlCommand.CommandType = CommandType.StoredProcedure;
                    _sqlCommand.CommandText = "NS_spCorreosAEnviar_Delete";

                    SqlParameter p = new SqlParameter("@IdEmail", SqlDbType.Int);

                    _sqlCommand.Parameters.Add("@IdEmail", SqlDbType.Int).Value = IdEmail;

                    DataTable _dataTable = new DataTable();
                    SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);

                    _dbConn.Open();
                    transaction = _dbConn.BeginTransaction();
                    _sqlCommand.Transaction = transaction;
                    _sqlDataAdapter.Fill(_dataTable);

                    foreach (DataRow rsd in _dataTable.Rows)
                    {
                        int idNumber = Convert.ToInt32(rsd["Id"]);
                        string toEmail = rsd["ToEmail"].ToString();
                        string fromEmail = rsd["FromEmail"].ToString();
                        string subjectEmail = rsd["SubjectEmail"].ToString();
                        string bodyEmail = rsd["BodyEmail"].ToString();

                        _correosList.Add(new CorreosAEnviar()
                        {
                            Id = idNumber,
                            ToEmail = toEmail,
                            FromEmail = fromEmail,
                            SubjectEmail = subjectEmail,
                            BodyEmail = bodyEmail,
                        });

                    }

                    MailMessage m = new MailMessage(new MailAddress(item.FromEmail, "SOMSight"), new MailAddress(item.ToEmail));
                    m.Subject = item.SubjectEmail;
                    m.Body = item.BodyEmail;
                    m.IsBodyHtml = true;
                    m.Bcc.Add(new MailAddress(item.FromEmail));

                    _client.Send(m);
                }
                catch (SqlException sqlEx)
                {
                    Message = sqlEx.Message + " --- " + sqlEx.InnerException;
                    transaction.Rollback();
                }
                catch (Exception ex)
                {
                    Message = ex.Message + " --- " + ex.InnerException;
                    transaction.Rollback();
                }
                finally
                {
                    if (_sqlCommand != null)
                    {
                        _dbConn.Close();
                        _dbConn.Dispose();

                    }
                }
            }
        }

        public static SmtpClient GetSMTPClient()
        {
            var _SMTPPort = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
            var _SMTPServer = ConfigurationManager.AppSettings["SMTPServer"].ToString();
            var _SMTPUser = ConfigurationManager.AppSettings["SMTPUser"].ToString();
            var _SMTPSecure = ConfigurationManager.AppSettings["SMTPSecure"].ToString();

            SmtpClient smtp = new SmtpClient(_SMTPServer);
            smtp.Port = _SMTPPort;
            smtp.EnableSsl = true;

            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(_SMTPUser, _SMTPSecure);
            return smtp;
        }
    }
}
