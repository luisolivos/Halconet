using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Windows.Forms;

using System.IO;
using System.Text.RegularExpressions;

namespace Cobranza
{
    public class SendMail
    {
        string _correoHalconet = "halconet@pj.com.mx";

        public bool Enviar(string _mailJefa, string _mailCliente, string _pdf, string _xml)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            string[] _correos = _mailCliente.Split(new Char[] {';'});
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }
            
            msg.From = new MailAddress(_mailJefa, "Facturación Electrónica " + _mailJefa.Substring(12,3).ToUpper());
            msg.Subject = "Factura electrónica - Distribuidora PJ";
            msg.Body = "<b>Distribuidora P.J. SA de C.V.</b><br><br>" +
                       "Estimado cliente, anexo encontrará documentos fiscales emitidos a su nombre por <b>Distribuidora PJ SA de CV.</b> <br><br>" +
                       "No es necesario confirmar de recibido, en caso de que esta información la requiera en otra cuenta de correo favor de hacérnoslo saber por este medio. </br><br><br>" +
                       "<b>Saludos cordiales.<\b><br><br>" +
                       "<small>HalcoNET " + ClasesSGUV.Propiedades.Version + "</small>";
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            Attachment pdf = new Attachment(_pdf);
            msg.Attachments.Add(pdf);

            Attachment xml = new Attachment(_xml);
            msg.Attachments.Add(xml);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_mailJefa, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        //correos de solicitudes de PPC y CEsp
        public bool Enviar(string _file, string _mailDestinatario, string _mailVendedor, string _vendedor, bool _solicitud)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            string[] _correos = _mailDestinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }
            msg.CC.Add(_mailVendedor);
            msg.Bcc.Add(_correoHalconet);

            msg.From = new MailAddress(_mailVendedor, "****SOLICITUD DE PRODUCTO****");
            msg.Subject = "Solicitud de producto - " + _vendedor;
            msg.Body = "<b>" + _vendedor + "</b>";

            msg.IsBodyHtml = true;

            Attachment pdf = new Attachment(_file);
            msg.Attachments.Add(pdf);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_mailVendedor, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }
        
        public bool Enviar(string _file, string _vendedorMail, string _vendedorNombre, string _jefaMail, string _jefaNombre)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(_vendedorMail);
            msg.CC.Add(_jefaMail);

            msg.From = new MailAddress(_jefaMail, _jefaNombre);
            msg.Subject = "ASUNTO";
            msg.Body = "<b>" + _vendedorNombre + "</b></br></br>";

            msg.IsBodyHtml = true;

            Attachment pdf = new Attachment(_file);
            msg.Attachments.Add(pdf);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_jefaMail, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        public bool EnviarNC(string _file, string _destinatario, string _vendedorNombre, string _remitente, int Total)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add(_remitente);

            string[] _correos = _destinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.Bcc.Add(_correoHalconet);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            msg.From = new MailAddress(_remitente, "Envío automatico HalcoNET " + ClasesSGUV.Propiedades.Version);
            msg.Subject = "NC Pendientes vendedor: " + _vendedorNombre;
            msg.Body = @"<b>Hola " + _vendedorNombre + @":</b><br><br>

                                Tienes " + Total + @" notas de crédito que no has arreglado al día de hoy, recuerda que la comisión sobre las facturas se paga cuando estas han sido liberadas al 100% y no presentan saldo alguno. 
                                Te recomendamos realizar de manera correcta el proceso de cargar el Precio Real en Halconet así como hablar con los clientes que suelen pagar menos de lo que acordaron, 
                                ya que esto trae graves problemas Administrativos para la Empresa y para tus comisiones. <br><br>

                                Ten en cuenta que ser Halcón significa ser el mejor, y para ser el mejor debes de hacer las cosas bien desde un inicio, en PJ buscamos y premiamos la excelencia, se parte de ello!<br><br>

                                Te deseamos un excelente día<br><br>

                          <b>Atte</b><br><br>

                          <b>Fuerza Halcón</b><br><br>

                          <small>**Adjunto encontraras un PDF con el delalle.</small>
                                ";

           
            Attachment pdf = new Attachment(_file);
            msg.Attachments.Add(pdf);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_remitente, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        public bool email_bien_escrito(String email)
        {
            
            String expresion;
            bool valor = false;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";

             string[] _correos = email.Split(new Char[] {';'});

             foreach (string item in _correos)
             {
                 if (Regex.IsMatch(item, expresion))
                 {
                     if (Regex.Replace(item, expresion, String.Empty).Length == 0)
                     {
                         valor = true;
                     }
                     else
                     {
                         valor = false;
                         return false;
                     }
                 }
                 else
                 {
                     valor = false;
                     return false;
                 }
             }

             return valor;
        }

        //////////////////////////////////////////
        //Comercializadora PEJ
        public bool EnviarPEJ(string _mailRemitente, string _mailDestinatarios, string _pdf, string _xml, string _pasword)
        {
            bool enviado = false;
            //_mailDestinatarios = "jose.olivos@pj.com.mx;sistemas@pj.com.mx";

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            string[] _correos = _mailDestinatarios.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.From = new MailAddress(_mailRemitente, "Facturación Electrónica");
            msg.Subject = "Factura electrónica - Comercializadora PEJ";
            msg.Body = "<b>Comercializadora PEJ</b><br><br>" +
                       "Estimado cliente, anexo encontrará documentos fiscales emitidos a su nombre por <b>Comercializadora PEJ</b> <br><br>" +
                       "No es necesario confirmar de recibido, en caso de que esta información la requiera en otra cuenta de correo favor de hacérnoslo saber por este medio. </br><br><br>" +
                       "<b>Saludos cordiales.<\b><br><br>" +
                       "<small>HalcoNET " + ClasesSGUV.Propiedades.Version + "</small>";
            msg.Priority = MailPriority.High;
            msg.IsBodyHtml = true;

            Attachment pdf = new Attachment(_pdf);
            msg.Attachments.Add(pdf);

            Attachment xml = new Attachment(_xml);
            msg.Attachments.Add(xml);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_mailRemitente, _pasword);
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        ///***atraduis***///
        public bool Enviar(string _file, string _destinatario, string _remitente, string _mensaje, string subject, bool IsBodyHtml)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string[] _correos = _destinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.Bcc.Add(_correoHalconet);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            

            msg.From = new MailAddress(_correoHalconet, "Notificaciones HalcoNET");
            msg.Subject = subject;
            msg.Body = _mensaje;


            Attachment pdf = new Attachment(_file);
            msg.Attachments.Add(pdf);

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_correoHalconet, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        public bool EnviarAtradius(string _file, string _destinatario, string _mensaje, string subject)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string[] _correos = _destinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.Bcc.Add(_correoHalconet);
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            Attachment pdf = new Attachment(_file);
            msg.Attachments.Add(pdf);

            msg.From = new MailAddress(_correoHalconet, "Notificaciones HalcoNET");
            msg.Subject = subject;
            msg.Body = _mensaje;

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_correoHalconet, "Mailpj11");
            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\r\nInnerException: " + ex.InnerException.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        public bool EnviarEstadoCuenta(string _destinatario, string _mensaje, string subject, string _cc, string JefaCobranza, string correoJefaCobranza, string _PATH)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string[] _correos = _destinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.Bcc.Add(_correoHalconet);

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            if (!string.IsNullOrEmpty(correoJefaCobranza))
                msg.From = new MailAddress(correoJefaCobranza, JefaCobranza);
            else
                msg.From = new MailAddress(_correoHalconet, "Notificaciones HalcoNET");

            msg.Subject = subject;

            

            string _declaracion = @"<p><font face='Arial' size=1.5 Color='#999999'>Distribuidora PJ respeta tu privacidad. Lee nuestra	
		                                <a href='http://pj.com.mx/empresa/declaracion.html'>
				                                declaración de privacidad&nbsp;
		                                </a>	
                                            en línea.	
                                    </font>
                                    </p>";
            
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_mensaje + "<br><br><img src=cid:companylogo>" + _declaracion, null, "text/html");
            if (System.IO.File.Exists("\\\\192.168.2.100\\HalcoNET\\Firmas\\" + JefaCobranza + ".png"))
            {
                LinkedResource logo = new LinkedResource("\\\\192.168.2.100\\HalcoNET\\Firmas\\" + JefaCobranza + ".png");
                logo.ContentId = "companylogo";
                htmlView.LinkedResources.Add(logo);
            }
            else 
            {
                LinkedResource logo = new LinkedResource("\\\\192.168.2.100\\HalcoNET\\Firmas\\Sin_firma.png");
                logo.ContentId = "companylogo";
                htmlView.LinkedResources.Add(logo);
            }

            string[] _correosCC = _cc.Split(new Char[] { ';' });

            foreach (string item in _correosCC)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.CC.Add(item);
            }
            

            //msg.Body = _mensaje;
            msg.AlternateViews.Add(htmlView);

            if (!string.IsNullOrEmpty(_PATH))
            {
                Attachment pdf = new Attachment(_PATH);
                msg.Attachments.Add(pdf);

            }

            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(correoJefaCobranza, "Mailpj11");

            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }

        public bool EnviarVentaPerdida(string _destinatario, string _mensaje, string subject, string _cc)
        {
            bool enviado = false;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string[] _correos = _destinatario.Split(new Char[] { ';' });
            foreach (string item in _correos)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.To.Add(item);
            }

            msg.Bcc.Add(_correoHalconet);

            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;

            msg.From = new MailAddress(_correoHalconet, "Notificaciones HalcoNET");

            msg.Subject = subject;

            string _declaracion = @"<p><font face='Arial' size=1.5 Color='#999999'>Distribuidora PJ respeta tu privacidad. Lee nuestra	
		                                <a href='http://pj.com.mx/empresa/declaracion.html'>
				                                declaración de privacidad&nbsp;
		                                </a>	
                                            en línea.	
                                    </font>
                                    </p>";

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(_mensaje + "<br><br><img src=cid:companylogo>" + _declaracion, null, "text/html");

                LinkedResource logo = new LinkedResource("\\\\192.168.2.100\\HalcoNET\\Firmas\\Sin_firma.png");
                logo.ContentId = "companylogo";
                htmlView.LinkedResources.Add(logo);

            string[] _correosCC = _cc.Split(new Char[] { ';' });

            foreach (string item in _correosCC)
            {
                if (!string.IsNullOrEmpty(item))
                    msg.CC.Add(item);
            }


            //msg.Body = _mensaje;
            msg.AlternateViews.Add(htmlView);


            Console.Write("Enviando....");
            SmtpClient client = new SmtpClient();
            client.Host = "smtp.pj.com.mx";
            client.Port = 1025;
            client.EnableSsl = false;
            client.UseDefaultCredentials = false;

            client.Credentials = new NetworkCredential(_correoHalconet, "Mailpj11");

            try
            {
                client.Send(msg);
                Console.Write("Envío exitoso");
                client.Dispose();
                msg.Dispose();
                enviado = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "HalcoNET", MessageBoxButtons.OK, MessageBoxIcon.Error);
                enviado = false;
            }
            return enviado;
        }
    }
}
